using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject monster;
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
        Debug.Log(randomMonster);
        switch (randomMonster)
        {
            case 0:
                monster = Instantiate(GameObject.Find("Octopus"), destination, Quaternion.identity);
                monsterColor = monster.GetComponent<SpriteRenderer>();
                monsterColor.color = Color.white;
                break;
            case 1:
                monster = Instantiate(GameObject.Find("BeenMonster"), destination, Quaternion.identity);
                monsterColor = monster.GetComponent<SpriteRenderer>();
                monsterColor.color = Color.white;
                break;
            case 2:
                monster = Instantiate(GameObject.Find("Slime"), destination, Quaternion.Euler(0, 180.0f, 0));
                monsterColor = monster.GetComponent<SpriteRenderer>();
                monsterColor.color = Color.white;
                break;
            default: break;

        }

        Invoke("GenerateRandomMonster", 3);
    }
}
