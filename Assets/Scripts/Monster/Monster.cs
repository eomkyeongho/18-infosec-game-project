using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Player player;
    GameObject playerObj;

    Rigidbody2D rigid;
    Transform target;
    SpriteRenderer sprite;
    BoxCollider2D boxCollider;
    bool isDamaged;
    int attackDamage;
    float moveSpeed;
    float hp;

    public void init(float speed, int damge, float monsterHP)
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        isDamaged = false;
        moveSpeed = speed;
        attackDamage = damge;
        hp = monsterHP;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
    }
    public void Move()
    {
        if(!isDamaged)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (rigid.position.x > target.position.x)
            sprite.flipX = true;
        else if (rigid.position.x < target.position.x)
            sprite.flipX = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            collision.GetComponent<Player>().Damaged(attackDamage);
        }
    }

    void KnowBackPlayer(Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - collision.transform.position;
        StartCoroutine(OnDamage(reactVec, power, durationTime));
        StartCoroutine(OnDamageEffect(0.01f));
    }

    void KnowBackAwayFromMaterial(Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - collision.transform.position;
        StartCoroutine(OnDamage(reactVec, power, durationTime));
        StartCoroutine(OnDamageEffect(0.01f));
    }

    void KnowBackAwayFromMaterial(GameObject obj, Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - collision.transform.position;
        StartCoroutine(OnDamage(obj, reactVec, power, durationTime));
    }

    void KnowBackAwayFromPlayer(Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - target.position;
        StartCoroutine(OnDamage(reactVec, power, durationTime));
        StartCoroutine(OnDamageEffect(0.01f));
    }

    void KnowBackAwayFromPlayer(GameObject obj, Collider2D collision, float power, float durationTime)
    {
        Vector2 reactVec = transform.position - target.position;
        StartCoroutine(OnDamage(obj, reactVec, power, durationTime));
    }

    public void GetDamaged(Collider2D collision, float power, float durationTime, GameObject obj = null)
    {
        if (collision.tag == "Player" || collision.tag == "Monster" || collision.tag == "BorderBall") return;

        hp -= player.attackDamage;
        if(hp<=0)
        {
            Destroy(gameObject);
            GameObject.FindWithTag("GM").GetComponent<GameManager>().CountKillMonster();
        }

        if(obj)
        {
            if (collision.tag == "FireBall")
            {
                KnowBackAwayFromPlayer(obj, collision, power, durationTime);
            }
            else
            {
                KnowBackAwayFromMaterial(obj, collision, power, durationTime);
            }
        }
        else
        {
            if (collision.tag == "FireBall")
            {
                KnowBackAwayFromPlayer(collision, power, durationTime);
            }
            else
            {
                KnowBackAwayFromMaterial(collision, power, durationTime);
            }
        }
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

    public IEnumerator OnDamageEffect(float durationTime)
    {
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
