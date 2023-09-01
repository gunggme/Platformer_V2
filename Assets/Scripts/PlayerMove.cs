using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private float moveSpeed;
    public float jumpImpulse;

    private bool isWall;
    
    private Vector2 moveDirection;
    
    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        set
        {
            if (_isFacingRight != value)
            {
                // Flip the local scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1); 
            }
            
            _isFacingRight = value;
        }
    }

    // Component
    private Animator animator;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    { 
        animator.SetBool(AnimationString.moveBool, moveDirection != Vector2.zero);
        if (rigid.Raycast(Vector2.down, Vector2.one, 0.9f))
        {
            animator.SetBool(AnimationString.isGroundBool, true);
        }
        else
        {
            animator.SetBool(AnimationString.isGroundBool, false);
        }
    }

    private void FixedUpdate()
    {
        isWall = rigid.Raycast(moveDirection, new Vector2(1, 2), 0.2f);
        moveSpeed = isWall ? 0f : speed;
        rigid.velocity = new Vector2(moveDirection.x * moveSpeed, rigid.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        Debug.Log("move");
        Vector2 inputVec = value.ReadValue<Vector2>();
        SetFacingDirection(inputVec);
        moveDirection = inputVec;
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (animator.GetBool(AnimationString.isGroundBool) && value.started)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpImpulse);
            animator.SetTrigger(AnimationString.jumpTrigger);
        }
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            animator.SetTrigger(AnimationString.attackTrigger);
        }
    }
    
    private void SetFacingDirection(Vector2 moveInput)
    {

        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight )
        {
            // Face the left
            IsFacingRight = false;
        }
    }
}
