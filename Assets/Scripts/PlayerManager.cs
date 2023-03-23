using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private Animator animator;
    private Rigidbody rb;
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private float rotateSpeed = 1.0f;
    [SerializeField] private float jumpHeight = 5.0f;


    // Start is called before the first frame update
    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        if (playerInput != Vector2.zero) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);

        if (playerInputActions.Player.Jump.IsPressed()) rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.y);
          animator.SetBool("isJumping", playerInputActions.Player.Jump.IsPressed());

        // Original codemonkey style movement
        //Vector3 moveDirection = new Vector3(playerInput.x, 0.0f, playerInput.y);
        //transform.position +=  moveDirection * moveSpeed * Time.deltaTime;
        //if(playerInput != Vector2.zero)
        //    transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime*moveSpeed);

        // Trying to implement standard movement
        transform.position +=  transform.forward * playerInput.y * moveSpeed * Time.deltaTime;
        //if(playerInput != Vector2.zero)
        if(playerInput.x != 0.0f)
            transform.forward = Vector3.Slerp(transform.forward, transform.right * playerInput.x, Time.deltaTime * rotateSpeed);
    }
}
