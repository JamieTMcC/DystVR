using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGunPosition : MonoBehaviour
{

    private Vector3 startPosition;
    public float speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
            transform.position = Vector3.MoveTowards(transform.position,startPosition,speed);
        
    }
}
