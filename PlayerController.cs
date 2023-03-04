using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject FirstPerson;
    public GameObject bulletPrefab;
    public float shootingForce;
    public GameObject weapon;
  
    // Start is called before the first frame update
    void Start()
    {
        weapon.GetComponent<Renderer> ().material.SetColor("_Color", Color.red);
        //To create red weapon. 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = this.transform.position + this.transform.forward + this.transform.up * 0.7f;
            bulletObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * shootingForce);
            bulletObject.GetComponent<Renderer>().material.SetColor
                (
                "_Color",
                weapon.GetComponent<Renderer>().material.GetColor("_Color")
                ); //To shoot red bullets 
        }

    }

    //void OnCollisionEnter(Collision c)
    //{
       
        //if (c.gameObject.GetComponent<EnemyController>() != null)
       
        //{
            //Time.timeScale = 0;
            //FirstPerson.GetComponent<StarterAssets.FirstPersonController>().enabled = false;
            
        //}
    //}
}
