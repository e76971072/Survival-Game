using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateEnemies : MonoBehaviour
{
    public GameObject Enemy, enemy2, enemy3, powerup, powerup2, powerup3;
    int enemyCount;
    public float xPos;
    public float zPos;
    public int enems = 10;
    int enemiesLeft = 0;
    public Text enemiesAlive;
    int round = 1;
    int powerUps = 0;

    // Start is called before the first frame update
    void Start()
    {
        xPos = Random.Range(10, 14);
        zPos = Random.Range(-3, 21);
        StartCoroutine(spawnEnemy(enems));
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemiesLeft = enemies.Length;

        enemiesAlive.text = "Round " + round + "\nEnemies: " + enemiesLeft + "/" + enems;

        if (enemiesLeft == 0)
        {
            enemyCount = 0;

            enems += 10;

            round++;

            StartCoroutine(spawnEnemy(enems));
        }
    }

    IEnumerator spawnEnemy(int enems)
    {
        if (powerUps == 0)
        {
            Instantiate(powerup, new Vector3(xPos, 1, zPos), Quaternion.identity);

            xPos = Random.Range(-15, -24);
            zPos = Random.Range(-13, -28);

            Instantiate(powerup2, new Vector3(xPos, 1, zPos), Quaternion.identity);

            powerUps = 3;
        }

        if (powerUps == 3)
        {
            xPos = Random.Range(-18, 28);
            zPos = Random.Range(-31, 28);
            Debug.Log("Made Health");
            Instantiate(powerup3, new Vector3(xPos, .5f, zPos), Quaternion.identity);
        }

        while (enemyCount < enems - 2)
        {
            xPos = Random.Range(16, 33);
            zPos = Random.Range(-35, -4);
            Instantiate(Enemy, new Vector3(xPos, 2, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

            enemyCount += 1;
        }

        Instantiate(enemy2, new Vector3(xPos, 2, zPos), Quaternion.identity);
        Instantiate(enemy3, new Vector3(xPos, 2, zPos), Quaternion.identity);
        enemyCount += 2;
    }
}
