using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float deadzone = 0.1f;
    [SerializeField] private float smoothRotate = 2000f;
    //private int playerSpeed = 10;
    //private int playerRotation = -5;

    [Header("Animation")]
    public Animations playerAnimation;
    private bool movingBackwards;
    public GameObject GameObj;

    [SerializeField] private bool isController;

    private CharacterController controller;

    [Header("Player Position")]
    public Vector2 movement;
    public Vector3 move;
    public Vector2 knockback;
    private float knockbackInitM;
    public float knockbackDecay = 0.1f;
    private Vector2 aim;

    private Vector3 playerVelocity;

    private PlayerControls playerControls;
    private PlayerInput playerInput;
    
    private Player player;

    [Header("Sounds")]
    public AudioSource GrassFootSteps;
    public float audioPitch = 1;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleRotation();
        HandleMovement();
    }

    void HandleInput()
    {
        movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }
    void HandleMovement()
    {
        //wasd movement
        move = new Vector3(movement.x, 0, movement.y);
        //make movement inversely relate to knockback
        if (knockback.magnitude > 0)
        {
            move = move * (1 - knockback.magnitude / knockbackInitM);
        }
        

        //sets gravity
        playerVelocity.y += gravity * Time.deltaTime;
        // add knockback
        if (knockback.x != 0 || knockback.y != 0)
        {
            playerVelocity.x = knockback.x;
            playerVelocity.z = knockback.y;
        }
        controller.Move(playerVelocity * Time.deltaTime);

        //decay knockback
        if (knockback.magnitude > 0)
        {
            knockback = Vector2.Lerp(knockback, Vector2.zero, knockbackDecay);
        }
        
        //Disables player movement,sounds, and animation after death
        if(player.health > 0){
        //determines if player is moving and plays walking sound
            controller.Move(move * Time.deltaTime * speed);
            if(movement.magnitude > 0) {
                GrassFootSteps.pitch = 1.6f;
                GrassFootSteps.enabled = true;
            }else 
                GrassFootSteps.enabled = false;

            //flips sprite if moving either left or right
            if(!GameObj.GetComponent<Animations>().spriteRenderer.flipX && move.x > 0) {
                GameObj.GetComponent<Animations>().spriteRenderer.flipX = true;
            }else if(GameObj.GetComponent<Animations>().spriteRenderer.flipX && move.x < 0){
                GameObj.GetComponent<Animations>().spriteRenderer.flipX = false;
            }

            //plays animations
            GameObj.GetComponent<Animations>().animation.SetFloat("WalkSpeed", movement.magnitude);
        }else {
            GrassFootSteps.enabled = false;
        }
    }
    void HandleRotation()
    {
        //For fixed rotation direction
        // var movementX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed; 
        // float rotationZ = Input.GetAxis("Horizontal") * playerRotation;
        // transform.Translate(movementX, 0, 0, Space.World);
        // transform.localRotation = Quaternion.Euler(0,0,rotationZ);


        // Uncomment for WASD rotation
        // Vector3 move = new Vector3(movement.x,0,movement.y);
        //  if(move != Vector3.zero) {
        //     Quaternion toRot = Quaternion.LookRotation(move,Vector3.up);
        //     transform.rotation = Quaternion.RotateTowards(transform.rotation,toRot,smoothRotate*Time.deltaTime);
        //  }


        //Uncomment for full mouse rotation
        //using controller
        if (isController)
        {
            if (Mathf.Abs(aim.x) > deadzone || Mathf.Abs(aim.y) > deadzone)
            {
                Vector3 playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
                if (playerDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion rot = Quaternion.LookRotation(playerDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, smoothRotate * Time.deltaTime);
                }
            }
            //keyboard
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayDistance;
/*            if (Input.GetKey("up"))
            {

            }
            if (Input.GetKey("left"))
            {

            }
            if (Input.GetKey("right"))
            {

            }
            if (Input.GetKey("down"))
            {

            }*/

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }

        }
    }

    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectPoint);
    }

    // public void OnDeviceChange(PlayerInput pi)
    // {
    //     isController = pi.currentControlScheme.Equals("Controller") ? true : false;
    // }

    // apply knockback to player using a vector3 direction and a float force
    public void Knockback(Vector2 direction, float force)
    {
        knockback = direction * force;
        knockbackInitM = knockback.magnitude;
    }
}
