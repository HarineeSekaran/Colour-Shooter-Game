using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;//To avoid space conflicts 

public class EnemyController : MonoBehaviour
{

    public Action onEnemyDestroyed;
    public Action onWrongEnemy;

    // Start is called before the first frame update
    void Start()// To have random red and blue enemies. 
    {
        int randomNumber = Random.Range(0, 2);

        if (randomNumber == 0)
        {
            this.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        else
        {
            this.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.GetComponent<BulletController> () != null)
        {

            if (c.gameObject.GetComponent<Renderer>().material.GetColor("_Color") ==
                    this.GetComponent<Renderer>().material.GetColor("_Color"))
            {
                GameObject.Destroy(this.gameObject);
                onEnemyDestroyed();//If color of player differs from enemy color, then enemy will be destroyed.

            }
            else
            {
                onWrongEnemy();
            }
        }

        if (c.gameObject.GetComponent<PlayerController>() != null)
        {
            if (c.gameObject.GetComponent<Renderer>().material.GetColor("_Color") ==
                 this.GetComponent<Renderer>().material.GetColor("_Color"))//Collision between enemy and player.
            {
                onWrongEnemy(); //When blue, collect red enemies; when red, collect blue enemies. 
            }
            else
            {
                GameObject.Destroy(this.gameObject);
                onEnemyDestroyed();
            }
        }

       // {
            //onWrongEnemy();//If color of player is similar to enemy color, then its wrong enemy. 
        //}

    }

}
