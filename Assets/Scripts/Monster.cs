using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform target;
    SpriteRenderer sprite;
    BoxCollider2D boxCollider;

    [Header("추격 속도")]
    [SerializeField] [Range(1f, 4f)] float moveSpeed = 3f;

    bool isDamaged;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();
        isDamaged = false;
    }
    void Update()
    {
        if(!isDamaged)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (rigid.position.x > target.position.x)
            sprite.flipX = true;
        else if (rigid.position.x < target.position.x)
            sprite.flipX = false;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball" && rigid)
        {
            Vector2 reactVec = transform.position - collision.transform.position;
            StartCoroutine(OnDamage(reactVec, 5.0f));
        }

        if (collision.tag == "Ball2" && rigid)
        {
            Vector2 reactVec = transform.position - target.position;
            StartCoroutine(OnDamage(reactVec, 10.0f));
            StartCoroutine(OnDamageEffect());
        }
    }
    IEnumerator OnDamage(Vector2 reactVec, float power)
    {
        isDamaged = true;
        boxCollider.isTrigger = true;
        reactVec = reactVec.normalized;
        rigid.velocity = reactVec * power;
        yield return new WaitForSeconds(0.2f);
        rigid.velocity = new Vector2(0,0);
        boxCollider.isTrigger = false;
        isDamaged = false;
    }

    IEnumerator OnDamageEffect()
    {
        float durationTime = 0.01f;

        while (durationTime > 0)
        {
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            sprite.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            durationTime -= Time.deltaTime;
        }
    }
}
