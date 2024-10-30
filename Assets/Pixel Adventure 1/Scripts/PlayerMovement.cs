using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private BoxCollider2D coll;

    private float lastAttackTime = 0f; // ��һ�ι�����ʱ��
    private float attackRate = 0.5f; // ����Ƶ�ʣ����Ը�����Ҫ������

    [SerializeField] private LayerMask JumpableGround; // ����Ծ����Ĳ㼶����

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 7f;

    private int jumpCount = 0; // ��¼��Ծ����
    private int maxJumps = 1;  // �����Ծ����

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
        dirX = Input.GetAxisRaw("Horizontal");    // ˮƽ����
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);   // ˮƽ�ٶ�

        // �����Ծ���룬���δ�ﵽ�����Ծ�����������Ծ
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++; // ��Ծ��������Ծ����
        }

        // ��⹥�������Լ�������ȴʱ��
        if (Input.GetMouseButtonDown(0) && Time.time - lastAttackTime > attackRate && EquipManager.instance.currentEquip != null)
        {
            lastAttackTime = Time.time;
            anim.SetTrigger("Attack");
        }

        UpdateAnimationState();
    }

    // ���½�ɫ����״̬
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

        // ��ⴹֱ�ٶ����ж���Ծ������״̬
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
            jumpCount = 0; // �����ɫ�ŵأ�������Ծ����
        }

        anim.SetInteger("state", (int)state);
    }

    // ����ɫ�Ƿ��ڵ�����
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, JumpableGround);
    }
}
