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
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        offset = transform.position - target.position;
        Invoke("cool", duTime);

    }
    void Update()
    {
        transform.position = target.position + offset;
        transform.RotateAround(target.position, target.forward, 20 * speed * Time.deltaTime);
        offset = transform.position - target.position;
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
