using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private float playerSpeed;
    public float dashSpeed;
    public float defaultTime;
    private float dashTime;
    private bool isDash;
    private float runSpeed;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        playerSpeed = 5.0f;
        runSpeed = 1.0f;
        isDash = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2(h * playerSpeed * runSpeed, v * playerSpeed * runSpeed);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
        }

        if(Input.GetKey(KeyCode.LeftShift) && dashTime <= 0)
        {
            runSpeed = 1.5f;
        }
        else
        {
            runSpeed = 1.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isDash = true;
        }

        if(dashTime <= 0)
        {
            playerSpeed = 5.0f;
            if (isDash)
            {
                dashTime = defaultTime;
                isDash = false;
            }
        }
        else
        {
            dashTime -= Time.deltaTime;
            playerSpeed = dashSpeed;
        }

        if(rigid.velocity.x == 0.0f && rigid.velocity.y == 0.0f)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
    }
}
