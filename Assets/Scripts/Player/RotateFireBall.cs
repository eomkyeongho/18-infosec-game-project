using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFireBall : MonoBehaviour
{

    //[Header("속도, 반지름")]

    [SerializeField] [Range(0f, 10f)] private float speed = 5;
    //[SerializeField] [Range(0f, 10f)] private float radius = 1;


    public Transform target;
    Rigidbody2D rigid;
    Vector3 offset;
    private float coolTime = 3.0f;
    private float duTime = 5.0f;
    public float angle;
    public float rotateSpeed;
    public Player player;
    private float damage;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("cool", duTime);
        angle *= 0.5f * Mathf.PI;
        damage = player.attackDamage;
    }
    void Update()
    {
        angle += rotateSpeed * Time.deltaTime % (2 * Mathf.PI);
        transform.position = new Vector2(target.position.x + 2.5f * Mathf.Sin(angle), target.position.y + 2.5f * Mathf.Cos(angle));
    }
    void cool()
    {
        if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
            Invoke("cool", coolTime);
        }
        else
        {
            gameObject.SetActive(true);
            Invoke("cool", duTime);
        }
    }
}
