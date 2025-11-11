using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineShake : MonoBehaviour
{
    public static CineShake bleg { get; private set; }

    public CinemachineVirtualCamera cineCam;
    private float shaketimer;

    private void Awake()
    {
        bleg = this; // Assign the static instance
        cineCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        Debug.Log("Shake");
        CinemachineBasicMultiChannelPerlin cineNoise = cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cineNoise.m_AmplitudeGain = intensity; 

        shaketimer = time;
    }

    private void Update()
    {
        if (shaketimer > 0)
        {
            shaketimer -= Time.deltaTime;
            if (shaketimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cineNoise = cineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cineNoise.m_AmplitudeGain = 0f;
            }
        }
    }
}