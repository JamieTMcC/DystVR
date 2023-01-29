/*
simply resets the gun on the table if the user drops it
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGunPosition : MonoBehaviour
{

    private Vector3 startPosition;
    private bool startReseting = false;
    public float speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartReset", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if(startReseting)
        transform.position = Vector3.MoveTowards(transform.position,startPosition,speed);   
    }

    void StartReset(){
        startPosition = transform.position;
        startReseting = true;
    }
}
