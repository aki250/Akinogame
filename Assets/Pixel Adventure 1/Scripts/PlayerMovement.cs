using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;

    private float lastAttackTime = 0f; // 上一次攻击的时间
    private float attackRate = 0.5f; // 攻击频率（可以根据需要调整）

    [SerializeField] private LayerMask JumpableGround; // 可跳跃地面的层级掩码

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private int jumpCount = 0; // 记录跳跃次数
    private int maxJumps = 1;  // 最大跳跃次数

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");    // 水平坐标
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);   // 水平速度

        // 检测跳跃输入，如果未达到最大跳跃次数则可以跳跃
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++; // 跳跃后增加跳跃次数
        }

        // 检测攻击输入以及攻击冷却时间
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackRate && EquipManager.instance.currentEquip != null)
        {
            lastAttackTime = Time.time;
            anim.SetTrigger("Attack");
        }

        UpdateAnimationState();
    }

    // 更新角色动画状态
    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        // 检测垂直速度以判断跳跃和下落状态
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        else if (IsGrounded())
        {
            jumpCount = 0; // 如果角色着地，重置跳跃次数
        }

        anim.SetInteger("state", (int)state);
    }

    // 检测角色是否在地面上
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
}
