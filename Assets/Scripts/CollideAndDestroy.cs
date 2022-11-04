using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System;
public class CollideAndDestroy : MonoBehaviour
{

    public AudioSource Audiodata;

    private string time;
    void Start(){
        using(StreamWriter writetext = new StreamWriter("write.txt")){
            writetext.WriteLine("---------- New File ----------");
        }
    }

    public void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag != "Untagged"){
            Audiodata.Play(0);
            using(StreamWriter writetext = new StreamWriter("write.txt", true))
                {
                writetext.WriteLine(DateTime.Now.ToString("h:mm:ss") + " -- " + collision.gameObject.tag);
                }

            Debug.Log(collision.gameObject.transform.parent.gameObject.tag);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }

    }
}