using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    SpriteRenderer sr;

    [Header("추격 속도")]
    [SerializeField] [Range(1f, 4f)] float moveSpeed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (rb.position.x > target.position.x)
            sr.flipX = true;
        else if (rb.position.x < target.position.x)
            sr.flipX = false;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "monster")
            rb.velocity = new Vector2(0, 0);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            Vector2 reactVec = transform.position - collision.transform.position;
            StartCoroutine(OnDamage(reactVec));
        } 
        else if(collision.tag == "Ball2")
        {
            Vector2 reactVec = transform.position - target.position;
            StartCoroutine(OnDamage(reactVec));
        }
    }
    IEnumerator OnDamage(Vector2 reactVec)
    {
        reactVec = reactVec.normalized;
        rb.AddForce(reactVec * 3, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(0,0);
        


    }
}
