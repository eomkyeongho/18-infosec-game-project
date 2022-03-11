using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject monster;
    public GameObject[] monsterList;
    SpriteRenderer monsterColor;
    GameObject player;
    public Text scriptText;
    public Text textTimer;
    float delay;
    public int enemyCount = 0;
    public float gameTime;
    int minTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        delay = 3.0f;
        scriptText.text = enemyCount.ToString();
        Invoke("GenerateRandomMonster", delay);
    }

    void Update()
    {
        delay -= Time.deltaTime * 0.03f;
        if (delay <= 1) delay = 1;
        //Debug.Log(delay + "초당 몬스터 1마리씩 소한 중");
        scriptText.text = enemyCount.ToString();
        gameTime += Time.deltaTime;
        if(Mathf.Round(gameTime) == 60)
        {
            minTime += 1;
            gameTime = 0;
        }
        textTimer.text = minTime + " : " + Mathf.Round(gameTime);
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

        Invoke("GenerateRandomMonster", delay);
    }
    public void CountKillMonster()
    {
        enemyCount++;
    }
}
