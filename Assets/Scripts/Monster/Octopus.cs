using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : Monster
{
    Monster m_Monster;

    [Header("추격 속도")]
    [SerializeField] [Range(1f, 4f)] float moveSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Monster = GetComponent<Monster>();
        m_Monster.init(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        m_Monster.Move(moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Ball2")
        {
            m_Monster.KnowBackAwayFromPlayer(collision, 5.0f, 0.4f);
        }
    }
}
