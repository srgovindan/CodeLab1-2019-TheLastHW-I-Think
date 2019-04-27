using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 targetPoint;
    public float speed;

    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float maxRayDistance = 2000f;
            Debug.DrawRay(myRay.origin,myRay.direction*maxRayDistance,Color.yellow);
            RaycastHit rayHit = new RaycastHit();
            
            if (Physics.Raycast(myRay, out rayHit,maxRayDistance))
            {
                targetPoint = new Vector3(rayHit.point.x,0f,rayHit.point.z);
            }
            else
            {
                targetPoint = transform.position;
            }
            
            //move towards target
            transform.position = Vector3.Slerp(transform.position, targetPoint, Time.deltaTime * speed);

        }     
        //right click could set a flag or something to modify the movement
    }
}
