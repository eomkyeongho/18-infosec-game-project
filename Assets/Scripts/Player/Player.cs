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
    bool isStop;

    public GameObject ballObj;
    public GameObject hpBar;
    public GameObject particle;
    // Start is called before the first frame update

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerSpeed = 5.0f;
        currentHp = fullHp;
        isStop = isDamaged = isDash = isDashCool = isFireBallCool = false;
        particle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStop)
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

        if(transform.position.x <= -12.64)
        {
            transform.position = new Vector2(-12.64f, transform.position.y);
        }
        else if (transform.position.x >= 73.95)
        {
            transform.position = new Vector2(73.95f, transform.position.y);
        }

        if (transform.position.y <= -32.1f)
        {
            transform.position = new Vector3(transform.position.x, -32.1f, 0);
        }
        else if (transform.position.y >= 32.1f)
        {
            transform.position = new Vector3(transform.position.x, 32.1f, 0);
        }
    }

    void Render()
    {
        if (rigid.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rigid.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
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
        if (!isFireBallCool)
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

                float angle = Mathf.Atan2(attackVec.y, attackVec.x) * 57.2958f - 45.0f;

                GameObject ball = Instantiate(ballObj, new Vector3(transform.position.x + attackVec.x, transform.position.y + attackVec.y, transform.position.z), Quaternion.Euler(0.0f, 0.0f, angle));
                Rigidbody2D ballRigid = ball.GetComponent<Rigidbody2D>();
                ballRigid.AddForce(attackVec * 20, ForceMode2D.Impulse);
            }
        }
    }

    public void Damaged(float damage)
    {
        if (!isDamaged)
        { 
            currentHp -= damage;
            Debug.Log("player hp : " + currentHp + "/" + fullHp);
            StartCoroutine(DamagedCoolDown());
        }
    }

    IEnumerator DashCoolDown()
    {
        isDashCool = true;
        particle.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        particle.SetActive(false);
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
        spriteRenderer.color = Color.red;
        isStop = true;
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);
        isStop = false;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.7f);
        isDamaged = false;  
    }
}

