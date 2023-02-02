using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float deadzone = 0.1f;
    [SerializeField] private float smoothRotate= 2000f;

    
    
    [SerializeField] private bool isController;

    private CharacterController controller;

    private Vector2 movement;
    private Vector2 aim;

    private Vector3 playerVelocity;

    private PlayerControls playerControls;
    private PlayerInput playerInput;

    private void Awake() {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable() {
        playerControls.Enable();
    }
    private void OnDisable() {
        playerControls.Disable();
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();
    }

    void HandleInput() {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        //aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }
    void HandleMovement() {
        Vector3 move = new Vector3(movement.x,0,movement.y);
        controller.Move(move * Time.deltaTime * speed);

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    void HandleRotation() {
        Vector3 move = new Vector3(movement.x,0,movement.y);
         if(move != Vector3.zero) {
            Quaternion toRot = Quaternion.LookRotation(move,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,toRot,smoothRotate*Time.deltaTime);
         }
        //using controller
        // if(isController) {
        //     if(Mathf.Abs(aim.x) > deadzone || Mathf.Abs(aim.y) > deadzone) {
        //         Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
        //         if(playerDirection.sqrMagnitude > 0.0f) {
        //             Quaternion rot = Quaternion.LookRotation(playerDirection, Vector3.up);
        //             transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, smoothRotate * Time.deltaTime);
        //         }
        //     }
        // //keyboard
        // }else {
        //     Ray ray = Camera.main.ScreenPointToRay(aim);
        //     Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //     float rayDistance;
            
        //     if(groundPlane.Raycast(ray,out rayDistance)) {
        //         Vector3 point = ray.GetPoint(rayDistance);
        //         LookAt(point);
        //     }
        // }
    }

    private void LookAt(Vector3 lookPoint) {
        Vector3 heightCorrectPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectPoint);
    }

    public void OnDeviceChange(PlayerInput pi) {
        isController = pi.currentControlScheme.Equals("Controller") ? true : false;
    }
}
