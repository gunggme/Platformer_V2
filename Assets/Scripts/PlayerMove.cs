using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private float moveSpeed;
    public float jumpImpulse;

    private bool isWall;
    
    private Vector2 moveDirection;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isWall = rigid.Raycast(moveDirection, new Vector2(1, 2), 0.2f);
        moveSpeed = isWall ? 0f : speed;
        rigid.velocity = new Vector2(moveDirection.x * moveSpeed, rigid.velocity.y);
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVec = value.Get<Vector2>();
        moveDirection = inputVec;
    }

    public void OnJump(InputValue value)
    {
        if (rigid.Raycast(Vector2.down, Vector2.one, 0.9f))
            rigid.velocity = new Vector2(rigid.velocity.x, jumpImpulse);
    }
}
