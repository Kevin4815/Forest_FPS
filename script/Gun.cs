using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class Gun : MonoBehaviour
{
    public float m_ShootRate = 0.1f;
    private float m_NextFire;
    public int m_NbShoot;
    public GameObject m_Camera;
    public GameObject m_CameraTest;
    public GameObject m_GunTarget;

    private Ray m_Ray;
    public RaycastHit m_Hit;

    public AudioClip m_FireSound;
    AudioSource m_Audio;
    public Animator m_AnimZoomShoot;
    public Animator m_AnimShoot;
    public Animator m_AnimFlame;
    public Animator m_AnimFlameZoom;
    public bool m_ZoomShootAnimation;
    public static bool m_AllowedFire = true;
    public FirstPersonController m_FPS;

    void Start()
    {
        m_ZoomShootAnimation = false;
        m_Audio = GetComponent<AudioSource>();
        m_CameraTest.SetActive(false);
    }

    void Update()
    {
        //If the player is running
        if (m_FPS.m_Vertical > 0 && Input.GetKey(KeyCode.Keypad0))
        {
            if(EnemyDeath.m_GameFinished == false)
            {
                m_AllowedFire = false;
                m_GunTarget.GetComponent<Image>().enabled = false;
                m_FPS.m_HeadBob.VerticalBobRange = 0.17f;
                m_FPS.m_HeadBob.HorizontalBobRange = 0.17f;
                m_AnimShoot.SetBool("Run", true);
            }
        }
        else
        {
            if(EnemyDeath.m_GameFinished == false)
            {
                m_AllowedFire = true;
                m_GunTarget.GetComponent<Image>().enabled = true;
                m_FPS.m_HeadBob.VerticalBobRange = 0.10f;
                m_AnimShoot.SetBool("Run", false);
            }
        }

        //On click to the left mouse button
        if (Input.GetButton("Fire1") && m_AllowedFire)
        {
            //Time between each fire
            if (Time.time > m_NextFire)
            {
                m_NextFire = Time.time + m_ShootRate;
                m_Audio.PlayOneShot(m_FireSound);

                //Shoot without zoom
                m_AnimShoot.SetBool("Shoot", true);
                m_AnimFlame.SetBool("Flame", true);
                m_AnimShoot.SetBool("Idle", false);

                //The ray cast for the gun at the center of the screen
                Vector2 ScreenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
                m_Ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);

                if (Physics.Raycast(m_Ray, out m_Hit, Camera.main.farClipPlane))
                {
                    //If the player shoot in a enemy
                    if (m_Hit.transform.tag == "Enemy")
                    {
                        m_NbShoot++;
                    }
                }
            }
        }
        else
        {
           m_AnimShoot.SetBool("Shoot", false);
           m_AnimFlame.SetBool("Flame", false);
            m_AnimShoot.SetBool("Idle", true);
        }

        GunTargetZoom();
        FireInZoom();
    }

    //Hide the normal camera and diplay the camera who zoom on the gun
    void GunTargetZoom()
    {
        if (Input.GetButton("Fire2") && m_AllowedFire)
        {
            m_ZoomShootAnimation = true;
            m_Camera.SetActive(false);
            m_CameraTest.SetActive(true);
            m_GunTarget.SetActive(false);
        }
        else
        {
            m_ZoomShootAnimation = false;
            m_Camera.SetActive(true);
            m_CameraTest.SetActive(false);
            m_GunTarget.SetActive(true);
        }
    }

    //Shoot with zoom
    void FireInZoom()
    {
        if (m_ZoomShootAnimation == true && Input.GetButton("Fire1") && m_AllowedFire)
        {
            m_AnimZoomShoot.SetBool("Shoot", true);
            m_AnimFlameZoom.SetBool("Flame", true);
            m_AnimZoomShoot.SetBool("Idle", false);
        }
        else if (!Input.GetButton("Fire1"))
        {
            m_AnimZoomShoot.SetBool("Shoot", false);
            m_AnimFlameZoom.SetBool("Flame", false);
            m_AnimZoomShoot.SetBool("Idle", true);
        }
    }
}
