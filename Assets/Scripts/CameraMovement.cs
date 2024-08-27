using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 5;

    void OnEnable()
    {
        if(target == null)
            enabled = false;
    }

    void Update()
    {
        if(target)
            transform.position = Vector3.Lerp(transform.position, target.position, speed*Time.deltaTime);
    }
}
