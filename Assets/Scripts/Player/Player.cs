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
    public float dashDuration;
    private float dashTime;
    private bool isDash, isDashCool, isFireBallCool;
    public float fullHp;
    public float attackDamage;
    private float currentHp;
    bool isDamaged;
    bool isKnockBack;

    public GameObject ballObj;
    public GameObject hpBar;
    // Start is called before the first frame update

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerSpeed = 5.0f;
        currentHp = fullHp;
        isKnockBack = isDamaged = isDash = isDashCool = isFireBallCool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isKnockBack)
        {
            Move();

            Dash();

            Render();
        }

        FireBallNearestMonster();

        if(currentHp<=0)
        {
            gameObject.SetActive(false);
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDashCool)
        {
            isDash = true;
            StartCoroutine(DashCoolDown());
        }

        if (dashTime < 0)
        {
            playerSpeed = 5.0f;
            if (isDash)
            {
                dashTime = dashDuration;
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
        hpBar.transform.localScale = new Vector3((float)currentHp / (float)fullHp, 0.15f, 1.0f);
    }

    void FireBallNearestMonster()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !isFireBallCool)
        {
            StartCoroutine(FireBallCoolDown());
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Monster");

            if (objects.Length > 0)
            {
                float shortDis = Vector3.Distance(transform.position, objects[0].transform.position);
                GameObject Monster = objects[0];

                foreach (GameObject obj in objects)
                {
                    float dist = Vector3.Distance(transform.position, obj.transform.position);

                    if (dist < shortDis)
                    {
                        shortDis = dist;
                        Monster = obj;
                    }
                }

                Vector2 attackVec = new Vector2(Monster.transform.position.x - transform.position.x,
                    Monster.transform.position.y - transform.position.y);

                attackVec.Normalize();

                GameObject ball = Instantiate(ballObj, transform.position, transform.rotation);
                Rigidbody2D ballRigid = ball.GetComponent<Rigidbody2D>();
                ballRigid.AddForce(attackVec * 20, ForceMode2D.Impulse);
            }
        }
    }

    public void Damaged(float damage, Transform target)
    {
        if (!isDamaged)
        { 
            currentHp -= damage;
            Debug.Log("player hp : " + currentHp + "/" + fullHp);
            StartCoroutine(DamagedCoolDown());
            StartCoroutine(KnockBackCoolDown());
            Vector2 reactVec = new Vector2(gameObject.transform.position.x - target.position.x,
                gameObject.transform.position.y - target.position.y);
            reactVec.Normalize();
            rigid.velocity = reactVec * 10;
        }

    }

    IEnumerator DashCoolDown()
    {
        isDashCool = true;
        yield return new WaitForSeconds(1.0f);
        isDashCool = false;
    }
    IEnumerator FireBallCoolDown()
    {
        isFireBallCool = true;
        yield return new WaitForSeconds(0.5f);
        isFireBallCool = false;
    }
    IEnumerator DamagedCoolDown()
    {
        isDamaged = true;
        yield return new WaitForSeconds(1.0f);
        isDamaged = false;  
    }
    IEnumerator KnockBackCoolDown()
    {
        spriteRenderer.color = Color.red;
        isKnockBack = true;
        yield return new WaitForSeconds(0.3f);
        isKnockBack = false;
        rigid.velocity = Vector2.zero;
        spriteRenderer.color = Color.white;
    }
}

