using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 originalCamPos;
    public CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin p;

    public float shakeVariation;
    public bool isShaking;
    public float shakeTimer;

    public void ShakeCamera(float duration)
    {
        shakeTimer = duration;
    }

    public void Start()
    {
        originalCamPos = transform.position;
        p = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTimer > 0)
        {
            if (!isShaking)
            {
                isShaking = true;
            }

            //cameraTransform.position = new Vector3(originalCamPos.x + Random.Range(-shakeVariation, shakeVariation), originalCamPos.y + Random.Range(-shakeVariation, shakeVariation), originalCamPos.z);
            
            p.m_AmplitudeGain = shakeVariation;
            shakeTimer -= Time.deltaTime;
        }

        if (shakeTimer <= 0)
        {
            cameraTransform.position = originalCamPos;
            p.m_AmplitudeGain = 0;
        }


        
    }
}
