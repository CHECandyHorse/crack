using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class movementScript : NetworkBehaviour{
    public CharacterController controller;

    public static bool isMoving;
    public static bool isGrounded;
    float speed;

    [Header("Values")]
    public float normalSpeed = 4.4f;
    public float sprintingSpeed = 6.8f;
    public float gravity = -12.6f;
    public float jumpHeight = 3f;
    public float FOVchange = .05f;

    [Header("Setup")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Camera _camera;
    public Camera _weaponCam;
    public mouseScript _mouseScript;
    public Material base_;
    public Material base_trans;
    public Animator animator_;

    [Header("Model")]
    public MeshRenderer[] meshRenderers;
    public GameObject head_Hair;

    Vector3 velocity;

    bool isSprint;

    float startFOV;
    float maxFOV;

    void Start(){
        speed = normalSpeed;

        if (!isLocalPlayer){
            _camera.enabled = false;
            _weaponCam.enabled = false;
            _mouseScript.enabled = false;
            _mouseScript.enabled = false;

            foreach (MeshRenderer i in meshRenderers){
                i.material = base_;
                head_Hair.SetActive(true);
            }
        }
        if (isLocalPlayer){
            foreach (MeshRenderer i in meshRenderers){
                i.material = base_trans;
                head_Hair.SetActive(false);
            }
        }

        startFOV = _camera.fieldOfView;
        maxFOV = 105;
    }
 
    void Update(){
        if (!isLocalPlayer)
            return;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && isGrounded){
            if (isGrounded){
                animator_.SetBool("move", true);
            }

            isMoving = true;
        }
        else{
            animator_.SetBool("move", false);

            isMoving = false;
        }

        if (!gunScript.isReloading){
            controller.Move(move * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded && !gunScript.isReloading){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            speed = sprintingSpeed;
            isSprint = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)){
            speed = normalSpeed;
            isSprint = false;
        }

        if (isGrounded){
            animator_.SetBool("jump", false);
        }
        else if (!isGrounded){
            animator_.SetBool("move", false);
            animator_.SetBool("jump", true);
        }

        if (isSprint){
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, maxFOV, FOVchange);
        }
        else if (!isSprint){
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, startFOV, FOVchange);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
