using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttack : MonoBehaviour
{
    public CreateEnemy m_CreateEnemy;
    public Transform m_Target;
    public Gun m_Gun;
    public Animator m_Anim;
    public bool m_AttackIsEnd;
    public bool m_GameOver;
    public static float m_Distance;

    private void Start()
    {
        m_GameOver = false;
    }
    //The enemy finish his attack before to run
    private IEnumerator WaitForRun()
    {
        m_AttackIsEnd = true;
        yield return new WaitForSeconds(1f);
        m_AttackIsEnd = false;
    }
    void Update()
    {
        if (m_GameOver == false)
        {
            for (int i = 0; i < m_CreateEnemy.m_ListOfEnemy.Count; i++)
            {
                NavMeshAgent navMeshAgent = m_CreateEnemy.m_ListOfEnemy[i].GetComponent<NavMeshAgent>();
                navMeshAgent.destination = m_Target.position;

                //Calculate the distance between the enemy and the player
                m_Distance = Vector3.Distance(m_Target.transform.position, m_CreateEnemy.m_ListOfEnemy[i].transform.position);
                m_Anim = m_CreateEnemy.m_ListOfEnemy[i].GetComponent<Animator>();

                //If the distance is less than 2f
                if (m_Distance < 2f)
                {
                    navMeshAgent.isStopped = true;
                    m_Anim.SetBool("Attack", true);
                    m_Anim.SetBool("Run", false);
                    StartCoroutine(WaitForRun());
                }
                else if(m_AttackIsEnd == false)
                {
                    navMeshAgent.isStopped = false;
                    m_Anim.SetBool("Attack", false);
                    m_Anim.SetBool("Run", true);
                }
            }
        }
    }
}
