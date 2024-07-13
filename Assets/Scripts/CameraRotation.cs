using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] float mouseSenstive;

    Camera mainCamera;

    bool isPicking = false;
    float horizontalAngel;
    float verticalAngel;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();

        //EventManager.Instance.onPickedUpObject.AddListener(OnObjectPickedUp);
       // EventManager.Instance.onObjectDrobed.AddListener(OnObjectDroped);

        verticalAngel = mainCamera.transform.localScale.x;
        horizontalAngel = mainCamera.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(XInput(), YInput());
    }

    float XInput()
    {
        if (isPicking)
        {
            return Input.GetAxis("Horizontal");
        }
        return Input.GetAxis("Mouse X");
    }

    float YInput()
    {
        if (isPicking)
        {
            return Input.GetAxis("Vertical");
        }
        return Input.GetAxis("Mouse Y");
    }

    void Rotate(float xInput, float yInput)
    {
        float turnCam = xInput * mouseSenstive;
        Vector3 currentRutation = mainCamera.transform.eulerAngles; 
        horizontalAngel += turnCam;
        if (horizontalAngel > 360)
            horizontalAngel -= 360;
        if (horizontalAngel < 0)
            horizontalAngel += 360;
        currentRutation.y = horizontalAngel;

        turnCam = -yInput * mouseSenstive;
        verticalAngel = Mathf.Clamp(verticalAngel + turnCam, -89f, 89);
        currentRutation.x = verticalAngel;

        mainCamera.transform.eulerAngles = currentRutation;


    }

    void OnObjectPickedUp(GameObject pickedUpOject)
    {
        isPicking = true;
    }

    void OnObjectDroped(GameObject pickedUpOject)
    {
        isPicking = false;
    }
}
