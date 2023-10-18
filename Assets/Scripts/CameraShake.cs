using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool isCameraShake = false;

    public Transform cameraTransform;
    public float shakeIntensity = 0.1f; 

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if(isCameraShake)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
        }      
    }
}
