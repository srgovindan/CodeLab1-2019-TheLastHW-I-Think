using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeFollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
   
    public float speed;
    
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //get unit vector between player and me
        Vector3 direction = Vector3.Normalize(player.transform.position - transform.position);
        //get look direction
        Quaternion quatDirection = Quaternion.LookRotation(direction);
        //get dot product between player and enemy 
        float dotProduct = Vector3.Dot(direction, transform.forward);
        
        //debug draw
        Debug.DrawRay(transform.position, transform.forward * 5, Color.cyan);
        Debug.DrawRay(transform.position, direction * 5, Color.green);
        
        
        //follow player in sight
        if (dotProduct > 0.25)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, quatDirection, Time.deltaTime);
            rb.velocity = transform.forward * speed;
        }
        
        //stop if player is not in sight
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.GM.GameOver();
        }
    }
}
