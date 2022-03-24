using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBat : Monster
{
    Monster m_Monster;

    // Start is called before the first frame update
    void Start()
    {
        m_Monster = GetComponent<Monster>();
        m_Monster.init(3.0f, 3, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        m_Monster.Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_Monster.GetDamaged(collision, 3.0f, 0.4f);
    }
}
