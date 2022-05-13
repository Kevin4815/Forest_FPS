using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyObject : MonoBehaviour
{
    public GameObject m_PrefabEnemy;
    public Transform m_Transform;
    public string m_NameEnemy;

    //Create the enemy object
    public EnemyObject(GameObject prefabEnemy, Transform transform, string name)
    {
        m_PrefabEnemy = prefabEnemy;
        m_Transform = transform;
        m_NameEnemy = name;
    }
}
