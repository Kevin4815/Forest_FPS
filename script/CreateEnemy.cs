using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateEnemy : MonoBehaviour
{
    public EnemyObject m_Enemy;
    public EnemyObject m_EnemyObject;
    public Transform m_EnemyContainer;
    public Transform m_Target;
    public RectTransform m_Terrain;

    public List<GameObject> m_ListOfEnemy = new List<GameObject>();
    public List<AudioClip> m_ZombieScream = new List<AudioClip>();
    
    int m_RandomScream;
    
    AudioSource m_Audio;
    GameObject m_EnemyOnTheScene;

    void Start()
    {
        //Create a new enemy
        m_EnemyObject = new EnemyObject(m_Enemy.m_PrefabEnemy, m_Enemy.m_Transform, m_Enemy.m_NameEnemy);
    }

    //Instantiate the enemy on the scene
    public void InstanceOfEnemy(EnemyObject enemy, int nbEnemy)
    {
        for (int i = 0; i < nbEnemy; i++)
        {
            //Choose a random scream
            m_RandomScream = Random.Range(0, 2);

            //Place the object in a random position
            float randomPositionWidth = Random.Range(0, m_Terrain.rect.width);
            float randomPositionHeight = Random.Range(0, m_Terrain.rect.height);
            float randomSpeed = Random.Range(5f, 10f);

            m_EnemyOnTheScene = Instantiate(enemy.m_PrefabEnemy, m_EnemyContainer);
            m_EnemyOnTheScene.AddComponent<NavMeshAgent>().destination = m_Target.position;
            m_EnemyOnTheScene.GetComponent<AudioSource>().PlayOneShot(m_ZombieScream[m_RandomScream]);
            m_EnemyOnTheScene.GetComponent<NavMeshAgent>().speed = randomSpeed;
            m_EnemyOnTheScene.name = enemy.m_NameEnemy + i;

            //m_EnemyOnTheScene.transform.position = new Vector3(randomPositionWidth, 0, randomPositionHeight);

            m_EnemyContainer.transform.position = new Vector3(randomPositionWidth, 0, randomPositionHeight);

            m_ListOfEnemy.Add(m_EnemyOnTheScene);
        }

        EnemyDeath.m_NewWave = true;
    }
}
