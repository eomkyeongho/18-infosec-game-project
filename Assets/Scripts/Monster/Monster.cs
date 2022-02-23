using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Rigidbody2D rigid;
    Transform target;
    SpriteRenderer sprite;
    BoxCollider2D boxCollider;
    bool isDamaged;
    
    void Start()
    {

    }

    public void init(GameObject obj)
    {
        rigid = obj.GetComponent<Rigidbody2D>();
        sprite = obj.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boxCollider = obj.GetComponent<BoxCollider2D>();
        isDamaged = false;
    }
    public void Move(float moveSpeed)
    {
        if(!isDamaged)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (rigid.position.x > target.position.x)
            sprite.flipX = true;
        else if (rigid.position.x < target.position.x)
            sprite.flipX = false;
    }
    
    public void KnowBackAwayFromMaterial(Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - collision.transform.position;
        StartCoroutine(OnDamage(reactVec, power, durationTime));
    }

    public void KnowBackAwayFromMaterial(GameObject obj, Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - collision.transform.position;
        StartCoroutine(OnDamage(obj, reactVec, power, durationTime));
    }

    public void KnowBackAwayFromPlayer(Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - target.position;
        StartCoroutine(OnDamage(reactVec, power, durationTime));
        StartCoroutine(OnDamageEffect());
    }

    public void KnowBackAwayFromPlayer(GameObject obj, Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - target.position;
        StartCoroutine(OnDamage(obj, reactVec, power, durationTime));
    }

    IEnumerator OnDamage(Vector2 reactVec, float power, float durationTime)
    {
        isDamaged = boxCollider.isTrigger = true;
        reactVec = reactVec.normalized;
        rigid.velocity = reactVec * power;
        yield return new WaitForSeconds(durationTime);
        rigid.velocity = new Vector2(0,0);
        isDamaged = boxCollider.isTrigger = false;
    }

    IEnumerator OnDamage(GameObject obj, Vector2 reactVec, float power, float durationTime)
    {
        obj.GetComponent<Animator>().SetBool("Damaged", true);
        isDamaged = boxCollider.isTrigger = true;
        reactVec = reactVec.normalized;
        rigid.velocity = reactVec * power;
        yield return new WaitForSeconds(durationTime);
        rigid.velocity = new Vector2(0, 0);
        isDamaged = boxCollider.isTrigger = false;
        obj.GetComponent<Animator>().SetBool("Damaged", false);
    }

    public IEnumerator OnDamageEffect()
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
