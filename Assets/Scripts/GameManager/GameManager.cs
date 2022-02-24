using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject monster;
    SpriteRenderer monsterColor;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GenerateRandomMonster", 3);
    }

    void GenerateRandomMonster()
    {
        //int randomRange = Random.Range(0, 4);
        int randomMonster = Random.Range(0, 3);
        Debug.Log(randomMonster);
        switch (randomMonster)
        {
            case 0:
                monster = Instantiate(GameObject.Find("Octopus"), new Vector3(-6, 0, 0), Quaternion.identity);
                monsterColor = monster.GetComponent<SpriteRenderer>();
                monsterColor.color = Color.white;
                break;
            case 1:
                monster = Instantiate(GameObject.Find("BeenMonster"), new Vector3(-6, 0, 0), Quaternion.identity);
                monsterColor = monster.GetComponent<SpriteRenderer>();
                monsterColor.color = Color.white;
                break;
            case 2:
                monster = Instantiate(GameObject.Find("Slime"), new Vector3(-6, 0, 0), Quaternion.Euler(0, 180.0f, 0));
                monsterColor = monster.GetComponent<SpriteRenderer>();
                monsterColor.color = Color.white;
                break;
            default: break;

        }

        Invoke("GenerateRandomMonster", 3);
    }
}
