using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideAndDestroy : MonoBehaviour
{

    public AudioSource Audiodata;

    public void OnTriggerEnter(Collider collision) {
        

        if(collision.gameObject.tag == "Target"){

            Destroy(collision.gameObject);
            Audiodata.Play(0);

        }
    }
}
