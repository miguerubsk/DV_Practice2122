using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    public float moveSpeed;
    public CharacterController player;
    public float jumpForce, gravityScale, rotateSpeed, knockBackForce, knockBackTime;
    public Animator animator;
    public GameObject characterModel;

    private Vector3 moveDirection;
    public Transform pivot;

    private float knockbackCounter;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (knockbackCounter <= 0) {
            float yStore = moveDirection.y;
            moveDirection = (this.transform.forward * Input.GetAxis("Vertical")) + (this.transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            if (Input.GetButtonDown("Jump") && player.isGrounded) {
                moveDirection.y = jumpForce;
            }

        } else {
            knockbackCounter -= Time.deltaTime;
        }

        if (!player.isGrounded) {
                moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
            }
        

        player.Move(moveDirection * Time.deltaTime);
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            characterModel.transform.rotation = Quaternion.Slerp(characterModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        animator.SetBool("isGrounded", player.isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    public void KnockBack(Vector3 direction) {
        knockbackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }

}
