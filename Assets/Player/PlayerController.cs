using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerMovement movement;
    PlayerCamera camera;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        camera = GetComponentInChildren<PlayerCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        camera.PitchInput = Input.GetAxisRaw("Mouse Y");
        camera.YawInput = Input.GetAxisRaw("Mouse X");
        Vector3 forward = Camera.main.transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 right = Camera.main.transform.right * Input.GetAxisRaw("Horizontal");
        movement.moveDirection = forward + right;
        if (Input.GetKeyDown(KeyCode.Space)) movement.Jump();
    }
}
