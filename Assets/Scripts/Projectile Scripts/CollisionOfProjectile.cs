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
    private ProjectileLogger logger;

    private ScoreManager sm;
    
    private bool gotInGoal = false;
    public bool tutorial = false;


    void Start(){
        audioSource = GetComponent<AudioSource>();        
        logger = GameObject.Find("XR Origin").GetComponent<ProjectileLogger>();

        if(!tutorial){
        scoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        sm = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
        logger.projectileFiredTime = Time.time.ToString();
        Invoke("DestroyAndCheckScore", 6.0f);
        }
    }

    void DestroyAndCheckScore(){
        if(gotInGoal){
            logger.deflected = "FALSE";
            sm.score--;
        }else{
            logger.deflected = "TRUE";
            sm.score++;
        }
        sm.ScoreDisplay();
        Destroy(gameObject);
    }



    void OnCollisionEnter(Collision col){
        Debug.Log(col.gameObject.tag);

        switch(col.gameObject.tag){
            case "Right Hand":
            if(!tutorial){
                logger.rHandCollision = true;
                logger.rCollisionTime = Time.time.ToString();
            }
            audioSource.PlayOneShot(handSound);
            break;

            case "Left Hand":
            if(!tutorial){
                logger.lHandCollision = true;
                logger.lCollisionTime = Time.time.ToString();
            }
            audioSource.PlayOneShot(handSound);
            break;

            case "Goal":
            logger.goalCollision = true;
            logger.goalCollisionTime = Time.time.ToString();
            gotInGoal = true;
            audioSource.PlayOneShot(goalSound);
            break;


            case "Wall":
            case "Untagged":
            logger.wallCollision = true;
            logger.wallCollisionTime = Time.time.ToString();
            audioSource.PlayOneShot(wallSound);
            break;
            
            

        }

    }

}
