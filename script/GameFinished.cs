using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinished : MonoBehaviour
{
    public CreateEnemy m_CreateEnemy;
    public GameObject m_FinalCanvas;
    public GameObject m_GunTarget;


    void Update()
    {
        //If the game is over
        if (EnemyDeath.m_GameFinished && m_CreateEnemy.m_ListOfEnemy.Count == 0)
        {
            StartCoroutine(LoadMenuScene());
        }
    }

    public IEnumerator LoadMenuScene()
    {
        yield return new WaitForSeconds(3f);
        m_FinalCanvas.SetActive(true);
        m_GunTarget.SetActive(false);
        Gun.m_AllowedFire = false;
        yield return new WaitForSeconds(3f);
    }
}
