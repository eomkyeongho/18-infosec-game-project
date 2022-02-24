using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject monster;
    public GameObject[] monsterList;
    SpriteRenderer monsterColor;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Invoke("GenerateRandomMonster", 3);
    }

    void GenerateRandomMonster()
    {
        Quaternion randomRotate = Quaternion.Euler(0f, 0f, Random.Range(-180f, 180f));
        Vector3 distance = new Vector3(7.5f,7.5f,0f);
        Vector3 destination = randomRotate * distance + player.transform.position;
        int randomMonster = Random.Range(0, 3);

        monster = Instantiate(monsterList[randomMonster], destination, monsterList[randomMonster].transform.rotation);
        monsterColor = monster.GetComponent<SpriteRenderer>();
        monsterColor.color = Color.white;

        Invoke("GenerateRandomMonster", 3);
    }
}
