using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour {

    [Header("References")]
    public Camera playerCamera;

    [Header ("General")]
    public float gravityScale = -9.8f;

    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;

    [Header("Jump")]
    public float jumpHeight;

    [Header("Camera")]
    public float cameraSpeed;

    Vector3 moveInput = Vector3.zero;
    CharacterController playerController;
    Vector3 rotationInput = Vector3.zero;
    private float cameraAngle;

    private void Awake() {
        playerController = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called before the first frame update
    void Start() {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        if (UIController.CanDoThings()) {
            Move();
            Look();
        }
        
    }

    private void Move() {
        if (playerController.isGrounded) {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = moveInput.normalized;

            if (Input.GetButton("Sprint")) {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            } else {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }
            

            if (Input.GetButtonDown("Jump")) {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;

        playerController.Move(moveInput * Time.deltaTime);
    }

    private void Look() {
        rotationInput.x = Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * cameraSpeed * Time.deltaTime;
        cameraAngle = cameraAngle + rotationInput.y;
        cameraAngle = Mathf.Clamp(cameraAngle, -70, 70);

        transform.Rotate(Vector3.up * rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraAngle, 0f, 0f);
    }
}
