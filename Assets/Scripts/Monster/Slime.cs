using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster
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
<<<<<<< HEAD
        if(collision.tag== "Ball2" || collision.tag == "Ball")
        {
            m_Monster.KnowBackAwayFromPlayer(gameObject, collision, 2.0f, 0.4f);
        }
=======
        m_Monster.GetDamaged(collision, 5.0f, 0.4f, gameObject);
>>>>>>> aee8a0cabda055fe175c2ab0347820dab6d8da0a
    }
}
