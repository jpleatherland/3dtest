using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public GameObject playerModel;
    public float moveSpeed;
    public float jumpForce;
    public float rotationSpeed;
    public float gravityScale = 5f;

    public Animator playerAnimator;

    private Vector3 moveDirection;

    private Camera theCamera;


    // Start is called before the first frame update
    void Start()
    {
        theCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal")); 
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;

        if (Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }

        moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

        //transform.position = transform.position + (moveDirection * Time.deltaTime * moveSpeed);
        characterController.Move(moveDirection * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, theCamera.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = newRotation;
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }

        playerAnimator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        playerAnimator.SetBool("isJumping", !characterController.isGrounded);
    }
}
