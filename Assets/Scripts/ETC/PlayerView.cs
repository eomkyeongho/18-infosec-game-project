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
    }
}