using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonMovement : MonoBehaviour
{
    public float speed = 0.05f;
    public float maxVariation = 10.0f;
    public bool horizontal = false;

    private float i = 0.0f;


    void Start(){
        StartCoroutine(Move());

    }


    // Update is called once per frame
    IEnumerator Move()
    {
        while(true){
        if(i > maxVariation || i < -1 * maxVariation){
            speed = speed * -1;
        }
        if(horizontal){
            transform.Rotate(0f,0f,speed,Space.Self);
        }else{
            transform.Rotate(speed,0f,0f,Space.Self);
        }
        i = i + speed;
        yield return new WaitForSeconds(0.02f);
        }
    }

}

