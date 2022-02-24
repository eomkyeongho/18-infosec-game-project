using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBall")
        {
            Destroy(gameObject);
        }
        /*
        if(collision.tag == "Monster")
        {
            Destroy(gameObject);
        }
        */
    }
}
