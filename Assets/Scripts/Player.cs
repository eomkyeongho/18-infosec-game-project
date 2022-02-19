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

    public GameObject ballObj;
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
        isDash = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Dash();

        Render();

        Attack();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "monster")
            gameObject.SetActive(false);
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDash = true;
        }

        if (dashTime <= 0)
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
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector2(h * playerSpeed, v * playerSpeed);
    }

    void Render()
    {
        if (rigid.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rigid.velocity.x > 0)
        {
            spriteRenderer.flipX = true;
        }

        if (rigid.velocity.x == 0.0f && rigid.velocity.y == 0.0f)
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
    }

    void Attack()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("monster");

            if (objects.Length > 0)
            {
                float shortDis = Vector3.Distance(transform.position, objects[0].transform.position);
                GameObject monster = objects[0];

                foreach (GameObject obj in objects)
                {
                    float dist = Vector3.Distance(transform.position, obj.transform.position);

                    if (dist < shortDis)
                    {
                        shortDis = dist;
                        monster = obj;
                    }
                }

                Debug.Log(monster.gameObject.name);

                Vector2 attackVec = new Vector2(monster.transform.position.x - transform.position.x,
                    monster.transform.position.y - transform.position.y);

                GameObject ball = Instantiate(ballObj, transform.position, transform.rotation);
                Rigidbody2D ballRigid = ball.GetComponent<Rigidbody2D>();
                ballRigid.AddForce(attackVec * 5, ForceMode2D.Impulse);
            }
        }
    }
}

