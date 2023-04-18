using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doubleJumpSpeed;

    private Rigidbody2D m_rigidbody;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private BoxCollider2D m_feet;
    private bool isGround;
    private bool canDoubleJump;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_feet = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
        Jump();
        CheckGrounded();
        SwitchAnimation();
        //Attack();
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(m_rigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (m_rigidbody.velocity.x > 0.1f)
            {
                //两种不同的改变朝向的方式
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                //m_spriteRenderer.flipX = false;
            }

            if (m_rigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                //m_spriteRenderer.flipX = true;
            }
        }
    }

    private void CheckGrounded()
    {
        isGround = m_feet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        Debug.Log(isGround);
    }

    private void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, m_rigidbody.velocity.y);
        m_rigidbody.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(m_rigidbody.velocity.x) > Mathf.Epsilon;
        m_animator.SetBool("Run", playerHasXAxisSpeed);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                m_animator.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                m_rigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    m_animator.SetBool("DoubleJump", true) ;
                    Vector2 doubleJumpVel = new Vector2(0.0f, doubleJumpSpeed);
                    m_rigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }

/*    private void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            m_animator.SetTrigger("Attack");
        }
    }*/

    private void SwitchAnimation()
    {
        m_animator.SetBool("Idle", false);
        if (m_animator.GetBool("Jump"))
        {
            if (m_rigidbody.velocity.y < 0.0f)
            {
                m_animator.SetBool("Jump", false);
                m_animator.SetBool("Fall", true);
            }

        }
        else if (isGround)
        {
            m_animator.SetBool("Fall", false);
            m_animator.SetBool("Idle", true) ;
        }

        if (m_animator.GetBool("DoubleJump"))
        {
            if (m_rigidbody.velocity.y < 0.0f)
            {
                m_animator.SetBool("DoubleJump", false);
                m_animator.SetBool("DoubleFall", true);
            }

        }
        else if (isGround)
        {
            m_animator.SetBool("DoubleFall", false);
            m_animator.SetBool("Idle", true);
        }

    }
}
