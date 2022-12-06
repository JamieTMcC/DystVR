/*
Handles collisions between the projectile fired from the cannon and the hands/walls/floor/goal
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

    private ScoreManager sm;
    private PathGenerator pg;
    
    private bool gotInGoal = false;
    public bool tutorial = false;


    void Start(){
        audioSource = GetComponent<AudioSource>();
        if(!tutorial){
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        pg = GameObject.Find("XR Origin").GetComponent<PathGenerator>();
        sm = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
        using(StreamWriter writetext = new StreamWriter(pg.getPath(), true))
        {writetext.WriteLine("New Projectile Created : " + gameObject.GetInstanceID().ToString());}
        Invoke("DestroyAndCheckScore", 6.0f);
        }
    }

    void DestroyAndCheckScore(){
        if(gotInGoal){
            sm.score--;
        }else{
            sm.score++;
        }
        sm.ScoreDisplay();
        Destroy(gameObject);
    }



    void OnCollisionEnter(Collision col){
        Debug.Log(col.gameObject.tag);

        switch(col.gameObject.tag){
            case "Right Hand":
            case "Left Hand":
            if(!tutorial){
            using(StreamWriter writetext = new StreamWriter(pg.getPath(), true))
            {writetext.WriteLine(gameObject.GetInstanceID().ToString() + " -- " + "Deflected: " + col.gameObject.tag);}
            }
            audioSource.PlayOneShot(handSound);
            break;

            case "Goal":
            Debug.Log("Goal");
            gotInGoal = true;
            using(StreamWriter writetext = new StreamWriter(pg.getPath(), true))
            {writetext.WriteLine(gameObject.GetInstanceID().ToString() + " -- " + "Not Deflected: " + col.gameObject.tag);}
            audioSource.PlayOneShot(goalSound);
            break;


            case "Wall":
            case "Untagged":
            audioSource.PlayOneShot(wallSound);
            break;
            
            

        }

    }

}
