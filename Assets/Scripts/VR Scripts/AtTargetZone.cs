using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtTargetZone : MonoBehaviour
{
    bool reached = false;

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "TutorialZone"){
            reached = true;
            Destroy(other.gameObject);
        }
    }

    public bool getReached(){
        return reached;
    }
}