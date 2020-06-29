using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform car;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - car.position;
    }

    // Update is called once per frame
    private void Update()
    {
        var position = car.position;
        transform.position = new Vector3(position.x, position.y, position.z) + _offset;
        //   print("camera position is " + transform.position);
    }
}