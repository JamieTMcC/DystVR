using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System;
using TMPro;
public class CollideAndDestroy : MonoBehaviour
{

    public AudioSource Audiodata;
    public GameObject Cannon;
    public TMP_Text ScoreText;
    private FireTarget script;
    public int score = 0;


    private string time;
    void Start(){
        using(StreamWriter writetext = new StreamWriter("write.txt")){
            writetext.WriteLine("---------- New File ----------");
        }
    }

    public void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "ShootableButton"){
            script = Cannon.GetComponent<FireTarget>();
            script.main();
            collision.gameObject.transform.parent.gameObject.SetActive(false);
            return;
        }






        if(collision.gameObject.tag != "Untagged"){
            score++;
            ScoreText.text = "Score: " + score.ToString();
            Audiodata.Play(0);
            using(StreamWriter writetext = new StreamWriter("write.txt", true))
                {
                writetext.WriteLine(DateTime.Now.ToString("h:mm:ss") + " -- " + collision.gameObject.tag);
                }

            Destroy(collision.gameObject.transform.parent.gameObject);
        }

    }
}