using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 6f;
    public float jumpSpeed = 7f;

    public float gravity = 20f;
    public Vector2 verticalSpeed = Vector2.zero;

    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float lookXLimit = 45f;

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Jump();
        Rotate();
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
            verticalSpeed.y = -2f;
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                verticalSpeed.y = jumpSpeed;
            }
        }
        else
        {
            verticalSpeed.y -= gravity * Time.deltaTime;
        }
    }

    private void Rotate()
    {
        Vector2 mouseAxis = Mouse.current.delta.ReadValue();

        _rotationX -= mouseAxis.y * mouseSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);

        cameraHolder.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        characterController.transform.Rotate(0, mouseAxis.x * mouseSensitivity, 0);
    }

    private void Move()
    {
        float horizontalMove = Keyboard.current.aKey.isPressed ? -1 : Keyboard.current.dKey.isPressed ? 1 : 0;
        float verticalMove = Keyboard.current.wKey.isPressed ? 1 : Keyboard.current.sKey.isPressed ? -1 : 0;

        Vector3 move = new Vector3(horizontalMove, 0, verticalMove);
        _moveDirection = transform.TransformDirection(move.normalized) * speed;
        
        Vector3 finalMove = _moveDirection + new Vector3(0, verticalSpeed.y, 0);
        characterController.Move(finalMove * Time.deltaTime);
    }
}