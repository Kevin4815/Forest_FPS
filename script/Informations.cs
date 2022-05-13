using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Informations : MonoBehaviour
{
    public CreateEnemy m_CreateEnnemy;
    public GameObject m_InfoCanvas;
    public TextMeshProUGUI m_InfosText;
    public TextMeshProUGUI m_NbEnnemy;
    public bool m_CanvasOff;

    void Start()
    {
        m_CanvasOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_NbEnnemy.text = "Ennemis : " + m_CreateEnnemy.m_ListOfEnemy.Count;
    }
}
