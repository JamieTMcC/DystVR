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
    public TMP_Text ScoreText,DebugText;
    public GameObject AssistButton,DebugButton;
    private FireTarget script;
    public Material VisibleMaterial,InvisibleMaterial;
    public int score = 0;
    private bool AssistMode,DebugMode;
    private string path;
    private string time;

    void Start(){

        path = Application.persistentDataPath + "/experimentdata/";
        int iteration;
        using(StreamReader readtext = new StreamReader(path + "iteration.txt")){
            iteration = Int32.Parse(readtext.ReadLine());
        }
        iteration++;
        using(StreamWriter writetext = new StreamWriter(path + "iteration.txt")){
            writetext.WriteLine(iteration.ToString());
        }

        path += iteration.ToString() + ".txt";

        using(StreamWriter writetext = new StreamWriter(path)){
            writetext.WriteLine("---------- New File ----------");
        }
    }

    public void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Right Hand" ^ collision.gameObject.tag == "Left Hand"){
            return;
        }


        if(collision.gameObject.tag == "StartButton"){

            script = Cannon.GetComponent<FireTarget>();
            script.main(AssistMode, DebugMode);
            collision.gameObject.transform.parent.gameObject.SetActive(false);
            AssistButton.SetActive(false);
            DebugButton.SetActive(false);
            return;
        }

        if(collision.gameObject.tag == "AssistButton" ^ collision.gameObject.tag == "DebugButton"){
            var ButtonRenderer = collision.gameObject.transform.GetComponent<Renderer>();
            
            switch(collision.gameObject.tag){
            case "AssistButton":
                AssistMode = !AssistMode;
                if(AssistMode){
                    ButtonRenderer.material.color =  Color.green;
                }else{
                    ButtonRenderer.material.color =  Color.red;
                }
                break;
            case "DebugButton":
                DebugMode = !DebugMode;
                if(DebugMode){
                    this.gameObject.transform.GetComponent<Renderer>().material = VisibleMaterial; 
                    ButtonRenderer.material.color =  Color.green;
                    DebugText.text += "Debug:\n";
                }else{
                    this.gameObject.transform.GetComponent<Renderer>().material = InvisibleMaterial;
                    ButtonRenderer.material.color =  Color.red;
                    DebugText.text = "";
                }
                break;
            }
            return;
        }

        





        if(collision.gameObject.tag != "Untagged"){
            score++;
            ScoreText.text = "Score: " + score.ToString();
            if(DebugMode){
                DebugText.text += DateTime.Now.ToString("h:mm:ss") + " -- " + collision.gameObject.tag + "\n";
            }
            Audiodata.Play(0);
            using(StreamWriter writetext = new StreamWriter(path, true))
            {
               writetext.WriteLine(DateTime.Now.ToString("h:mm:ss") + " -- " + collision.gameObject.tag);
            }

            Destroy(collision.gameObject.transform.parent.gameObject);
        }

    }
}