using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Transform view;
    public CharacterController controller;
    public bool move = false;
    float speed = 0.5f;
    public float moveSpeed = 5.0f;


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            move = true;
            this.controller.SimpleMove(this.view.forward * this.speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move = true;
            this.controller.SimpleMove(this.view.forward * -this.speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            move = true;
            this.controller.SimpleMove(this.view.right * this.speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            move = true;
            this.controller.SimpleMove(this.view.right * -this.speed);
        }
        else move = false;

        float mouseX = Input.GetAxis("Mouse X") * moveSpeed;

        float mouseY = Input.GetAxis("Mouse Y") * moveSpeed;

        // 鼠标在Y轴上的移动号转为摄像机的上下运动，即是绕着X轴反向旋转

        Camera.main.transform.localRotation = Camera.main.transform.localRotation * Quaternion.Euler(-mouseY, mouseX, 0);
        //transform.localRotation = transform.localRotation * Quaternion.Euler(0, mouseX, 0);
    }
}
