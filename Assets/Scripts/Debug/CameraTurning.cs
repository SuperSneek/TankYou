﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurning : MonoBehaviour
{

    private Transform transform;
    [SerializeField]
    private float sensitivity = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * sensitivity);
    }
}
