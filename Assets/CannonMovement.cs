using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{

    public GameObject CannonBarrel;

    public float speed = 1.0f;
    public float maxVariation = 0.0f;
    
    private int i = 0;
    private int incrementor = 1;
    
    void Start()
    {
        if(maxVariation == 0.0f){

            maxVariation = 15 * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(i > maxVariation || i < -1 * maxVariation){
            incrementor = incrementor *-1;
            speed = speed * -1;
        }
        CannonBarrel.transform.Rotate(speed,0f,0f,Space.Self);
        i = i + incrementor;
        Debug.Log(i);
    }

}

