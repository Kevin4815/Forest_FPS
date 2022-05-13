using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    public AudioClip m_Hurt;
    //public AudioClip m_Lose;
    AudioSource m_Audio;
    public Image m_Bar;
    public CreateEnemy m_CreateEnemy;
    public EnemyAttack m_EnemyAttack;
    public Animator m_Anim;
    public FirstPersonController m_FirstPersonController;
    public Animator m_PlayerAnimator;
    public GameObject m_DeadCanvas;

    public List<GameObject> m_ObjectToHide = new List<GameObject>();

    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
    }

    //Check is the player is hurt by a enemy
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "RightForeArm")
        {
            m_Audio.PlayOneShot(m_Hurt);
            m_Bar.fillAmount -= 0.25f;

            //If the player is dead
            if (m_Bar.fillAmount == 0)
            {
                m_EnemyAttack.m_GameOver = true;

                //Find each enemy and stop their attack
                for (int i = 0; i < m_CreateEnemy.m_ListOfEnemy.Count; i++)
                {
                    NavMeshAgent navMeshAgent = m_CreateEnemy.m_ListOfEnemy[i].GetComponent<NavMeshAgent>();
                    navMeshAgent.isStopped = true;

                    StartCoroutine(DeathAnimation( 1f));
                    HideObjectAfterDeath();

                    m_FirstPersonController.enabled = false;

                    //The enemy stop his attack and stop chasing the player
                    m_Anim = m_CreateEnemy.m_ListOfEnemy[i].GetComponent<Animator>();
                    m_Anim.SetBool("Attack", false);
                    m_Anim.SetBool("Run", false);
                    m_Anim.SetBool("Idle", true);
                    m_PlayerAnimator.SetBool("Death", true);
                }
            }
        }
    }

    public void HideObjectAfterDeath()
    {
        for (int i = 0; i < m_ObjectToHide.Count; i++)
        {
            m_ObjectToHide[i].SetActive(false);
        }
    }

    //Player rotation after his death
    IEnumerator DeathAnimation(float duration)
    {
        float startRotation = m_FirstPersonController.transform.rotation.x;
        float endRotation = startRotation - 90f;
        float timeElasped = 0;

        while (timeElasped < duration)
        {
            float xRotation = Mathf.Lerp(startRotation, endRotation, timeElasped / duration);
            transform.eulerAngles = new Vector3(xRotation, m_FirstPersonController.transform.eulerAngles.y, m_FirstPersonController.transform.eulerAngles.z);
            timeElasped += Time.deltaTime;

            yield return null;
        }

        m_DeadCanvas.SetActive(true);
    }
}
