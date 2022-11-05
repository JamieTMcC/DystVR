using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{

    public GameObject CannonBarrel;

    public float speed = 1.0f;
    public float maxVariation = 10.0f;
    
    private float i = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(i > maxVariation || i < -1 * maxVariation){
            speed = speed * -1;
        }
        CannonBarrel.transform.Rotate(speed,0f,0f,Space.Self);
        i = i + speed;
    }

}

