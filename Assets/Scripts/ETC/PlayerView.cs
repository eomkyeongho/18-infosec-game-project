using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    void Update()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        Vector2 moveVector = new Vector2(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime);
        this.transform.Translate(moveVector);

        if(transform.position.x<=-5f)
        {
            transform.position = new Vector3(-5f, transform.position.y, -10);
        }
        else if (transform.position.x >= 75f)
        {
            transform.position = new Vector3(75.0f, transform.position.y, -10);
        }

        if (transform.position.y <= -30.0f)
        {
            transform.position = new Vector3(transform.position.x, -30.0f, - 10);
        }
        else if (transform.position.y >= 30.0f)
        {
            transform.position = new Vector3(transform.position.x, 30.0f, -10);
        }
    }
}
