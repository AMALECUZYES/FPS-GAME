using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletspeed = 70f;
    public float bulletlife = 3;
    public GameObject Bulletimpact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        GetComponent<Rigidbody>().velocity = transform.forward * bulletspeed;
        bulletlife -= Time.deltaTime;
        if (bulletlife <= 0)
        {
            Destroy(gameObject);

        }
    
    
    
    
    
    
    
    
    }
    //this  function detects if the bullet collided with anything  
    private void OnCollisionEnter(Collision collision)
    {
        //transform point = collision.gameObject.transform;
        
        //Instantiate(Bulletimpact, point.position, Quaternion.LookRotation(collision.normal));
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        
        Destroy(gameObject);
        //Instantiate(Bulletimpact, hit.point, Quaternion.LookRotation(hit.normal));



    }






}
