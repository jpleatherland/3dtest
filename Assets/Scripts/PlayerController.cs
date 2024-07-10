using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public CharacterController characterController;
    public GameObject playerModel;
    public float moveSpeed;
    public float jumpForce;
    public float rotationSpeed;
    public float gravityScale = 5f;

    public Animator playerAnimator;

    private Vector3 moveDirection;

    private Camera theCamera;

    public bool isKnocking;
    public float knockbackCounter;
    public Vector2 knockbackForce;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        theCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnocking)
        {
            playerMovement();
        }

        if (isKnocking)
        {
            knockback();
        }

        playerAnimator.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        playerAnimator.SetBool("isJumping", !characterController.isGrounded);
    }

    private void playerMovement()
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection.Normalize();
        moveDirection *= moveSpeed;
        moveDirection.y = yStore;
        if (!characterController.isGrounded)
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        }
        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }

        characterController.Move(moveDirection * Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, theCamera.transform.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = newRotation;
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void triggerKnockback(float knockbackLength, Vector2 knockbackPower)
    {
        isKnocking = true;
        knockbackCounter = knockbackLength;
        knockbackForce = knockbackPower;
        moveDirection.y = knockbackForce.y;
    }

    private void knockback()
    {
        knockbackCounter -= Time.deltaTime;
        if (knockbackCounter <= 0)
        {
            isKnocking = false;
            return;
        }
        float yStore = moveDirection.y;
        moveDirection = playerModel.transform.forward * -knockbackForce.x;
        moveDirection.y = yStore;
        if (!characterController.isGrounded)
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
        }
        
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
