/*
Handles collisions between the projectile fired from the cannon and the hands/walls/floor/goal
*/
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

    private ScoreManager sm;
    private PathGenerator pg;
    
    private bool gotInGoal = false;


    void Start(){
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        audioSource = GetComponent<AudioSource>();
        pg = GameObject.Find("XR Origin").GetComponent<PathGenerator>();
        sm = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
        path = pg.getPath();
        using(StreamWriter writetext = new StreamWriter(path, true))
        {writetext.WriteLine("New Projectile Created : " + gameObject.GetInstanceID().ToString());}
        StartCoroutine(DestroyAndCheckScore(6.0f));
    }

    IEnumerator DestroyAndCheckScore(float time){
        yield return new WaitForSeconds(time);
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
            using(StreamWriter writetext = new StreamWriter(path, true))
            {writetext.WriteLine(gameObject.GetInstanceID().ToString() + " -- " + "Deflected: " + col.gameObject.tag);}
            audioSource.PlayOneShot(handSound);
            break;

            case "Goal":
            Debug.Log("Goal");
            gotInGoal = true;
            using(StreamWriter writetext = new StreamWriter(path, true))
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
