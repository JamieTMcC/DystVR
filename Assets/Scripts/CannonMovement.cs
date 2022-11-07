using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonMovement : MonoBehaviour
{

    public GameObject CannonBarrel;

    public float speed = 1.0f;
    public float maxVariation = 10.0f;
    public Scene PistolGame;
    public bool horizontal = false;

    private float i = 0.0f;


    void Start(){
        if(SceneManager.GetActiveScene() == PistolGame){
            horizontal = false;
        }else{
            horizontal = true;
        }


    }


    // Update is called once per frame
    void Update()
    {
        if(i > maxVariation || i < -1 * maxVariation){
            speed = speed * -1;
        }
        if(horizontal){
            CannonBarrel.transform.Rotate(0f,0f,speed,Space.Self);
        }else{
            CannonBarrel.transform.Rotate(speed,0f,0f,Space.Self);
        }
        i = i + speed;
    }

}

