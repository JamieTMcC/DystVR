using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using TMPro;


public class CollisionOfProjectile : MonoBehaviour
{

    private int score;
    private TMP_Text scoreText;
    private AudioSource audioSource;
    public AudioClip wallSound, goalSound, handSound;

    private string path;



    void Start(){
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        
        path = Application.persistentDataPath + "/experimentdata/ProjectileGame/";
        
        //Reads a number from a file and increments then writes to number each new user         
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

    void OnCollisionEnter(Collision col){

        switch(col.gameObject.tag){
            case "RightHand":
            case "LeftHand":
            using(StreamWriter writetext = new StreamWriter(path, true))
            {writetext.WriteLine(DateTime.Now.ToString("h:mm:ss") + " -- " + "Deflected: " + col.gameObject.tag);}
            score++;
            audioSource.PlayOneShot(handSound);
            break;


            case "Wall":
            case "Untagged":
            audioSource.PlayOneShot(wallSound);
            break;
            
            
            case "Goal":
            using(StreamWriter writetext = new StreamWriter(path, true))
            {writetext.WriteLine(DateTime.Now.ToString("h:mm:ss") + " -- " + "Not Deflected: " + col.gameObject.tag);}
            score--;
            audioSource.PlayOneShot(goalSound);
            break;
        }
        
        using(StreamWriter writetext = new StreamWriter(path, true))
            {writetext.WriteLine(DateTime.Now.ToString("h:mm:ss") + " -- " + col.gameObject.tag);}
        scoreText.text = "Score: " + score.ToString();
        
        Destroy(gameObject, 6);



    }

}
