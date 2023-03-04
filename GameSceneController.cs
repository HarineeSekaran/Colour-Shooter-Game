using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneController : MonoBehaviour
{
    public TextMeshProUGUI gameText;
    public GameObject enemyPrefab;
    public float spawningDistance;
    public GameObject player,weapon;
    
   private float gameTimer = 60f;
   private float timeColorChanged = 60f;

   private int score;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            SpawnEnemy();
        }

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        gameText.text = "Time:  " + Mathf.Floor(gameTimer);
        gameText.text += "\nScore: " + score;

        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0 || Time.timeScale == 0)
        {
            gameTimer = 0;
            gameText.text = "Game Over! Score: " + score;
            gameText.text += "\nPress R to restart";
        }

        if (timeColorChanged - gameTimer >= 10f) //Colour changes "Every 10 Seconds"!!
        {
            if (weapon.GetComponent<Renderer>().material.GetColor("_Color") == Color.blue)
            {
                weapon.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
            else
            {
                weapon.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }

            timeColorChanged = gameTimer;
        }  

        
    }

    void SpawnEnemy( )
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);

        GameObject enemyObject = Instantiate(enemyPrefab);
        enemyObject.transform.SetParent(this.transform);
        enemyObject.transform.position = new Vector3
            (
            Mathf.Cos(randomAngle) * spawningDistance,
            2,
            Mathf.Sin(randomAngle) * spawningDistance
            );

        EnemyController enemy = enemyObject.GetComponent<EnemyController>();
        enemy.onEnemyDestroyed = () =>
        {
            OnEnemyDestroyed();
        };
        enemy.onWrongEnemy = () =>
        {
            OnWrongEnemy();
        };
    }

    void OnEnemyDestroyed ()
    {
        Debug.Log("Enemy Destroyed");
        SpawnEnemy();//Everytime an enemy gets destroyes, new enemy spawns.

        score += 100;
    }

    void OnWrongEnemy ()
    {
        Debug.Log("Wrong Enemy!");
        Time.timeScale = 0;
        player.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
    }
}
