using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeath : MonoBehaviour
{
    public PlayerHealth m_PlayerHealth;
    public Gun m_Gun;
    public EnemyAttack m_EnemyAttack;
    public CreateEnemy m_CreateEnemy;
    public Informations m_GameInfos;
    public static bool m_NewWave = true;
    public static int m_NbWaves = 0;
    public static bool m_GameFinished = false;

    void Update()
    {
        //If the number of shoot on the enemy is superior to 5, the enemy die
        if (m_Gun.m_NbShoot >= 5)
        {
            try
            {
                //Find the index of the killed enemy //Disable his NavMeshAgent //Removes it from de list
                int index = m_EnemyAttack.m_CreateEnemy.m_ListOfEnemy.FindIndex(a => a.name == m_Gun.m_Hit.collider.name);
                m_EnemyAttack.m_CreateEnemy.m_ListOfEnemy[index].GetComponent<NavMeshAgent>().enabled = false;
                m_EnemyAttack.m_CreateEnemy.m_ListOfEnemy[index].GetComponent<AudioSource>().enabled = false;
                m_EnemyAttack.m_CreateEnemy.m_ListOfEnemy.Remove(m_EnemyAttack.m_CreateEnemy.m_ListOfEnemy[index]);
                m_CreateEnemy.m_EnemyContainer.DetachChildren();
            }
            catch (Exception e)
            {
                //Index out of range exception
                Debug.Log(e);
            }
           
            //Find and active his animator
            m_EnemyAttack.m_Anim = m_Gun.m_Hit.collider.GetComponent<Animator>();
            m_EnemyAttack.m_Anim.SetBool("Death", true);
            m_Gun.m_NbShoot = 0;
        }

        NewWave();
    }

    //Create a new wave of enemy when all enemies are dead
    public void NewWave()
    {
        if (m_CreateEnemy.m_ListOfEnemy.Count == 0 && m_PlayerHealth.m_Bar.fillAmount > 0)
        {
            if (m_NewWave && m_NbWaves < 2)
            {
                StartCoroutine(AddEnemy());
                StartCoroutine(CanvasInformations());
                m_NewWave = false;
            }
            else if(m_NbWaves >= 2)
            {
                StartCoroutine(GameOver());
                m_NewWave = false;
            }
        }
    }

    //Create a new list of enemies
    public IEnumerator AddEnemy()
    {
        yield return new WaitForSeconds(4f);
        m_CreateEnemy.InstanceOfEnemy(m_CreateEnemy.m_EnemyObject, 10);
        m_NbWaves++;
        yield return new WaitForSeconds(2f);
    }

    //Display the waves informations
    public IEnumerator CanvasInformations()
    {
        yield return new WaitForSeconds(4f);
        m_GameInfos.m_InfoCanvas.SetActive(true);
        
        if (m_NbWaves == 1)
        {
            m_GameInfos.m_InfosText.text = "La première vague arrive !";
        }
        else if(m_NbWaves == 2)
        {
            m_GameInfos.m_InfosText.text = "La deuxième vague arrive !";
        }

        yield return new WaitForSeconds(4f);
        m_GameInfos.m_InfoCanvas.SetActive(false);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        m_GameFinished = true;
    }
}
