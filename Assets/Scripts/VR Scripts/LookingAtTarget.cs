using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAtTarget : MonoBehaviour
{
    public int x = 0;


    public void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "OuterRing"){
            x++;
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }

    public int getX(){
        return x;
    }
}
