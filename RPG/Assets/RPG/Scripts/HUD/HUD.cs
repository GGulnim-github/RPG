using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward
            , _cameraTransform.rotation * Vector3.up);
    }
}
