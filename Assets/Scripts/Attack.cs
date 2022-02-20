using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    //[Header("�ӵ�, ������")]

    [SerializeField] [Range(0f, 10f)] private float speed = 5;
    //[SerializeField] [Range(0f, 10f)] private float radius = 1;


    public Transform target;
    Rigidbody2D rigid;
    Vector3 offset;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        offset = transform.position - target.position;

    }
    void Update()
    {
        transform.position = target.position + offset;
        transform.RotateAround(target.position, target.forward, 20 * speed * Time.deltaTime);
        offset = transform.position - target.position;
        Debug.Log(transform.rotation);
    }

}
