/*
Handles the collision between the cylinder which comes from the gun
when the trigger is pressed and the buttons/targets
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System;
using TMPro;
public class CollideAndDestroy : MonoBehaviour
{

    private AudioSource audioData;
    private GameObject cannon;
    public TMP_Text ScoreText,DebugText;
    private GameObject FPSCounter;
    public bool NotTutorial;
    public bool continueGame = true;
    private ShootLogger logger;

    //Button Objects
    private GameObject AssistButton,DebugButton;
    private FireTarget script;

    public Material VisibleMaterial,InvisibleMaterial;
    private int score = 0;
    private int noOfHits = 0;

    //2 Different Modes
    private bool AssistMode,DebugMode;

    void Start(){
        cannon = GameObject.Find("Cannon");
        audioData = GameObject.Find("AudioObject").GetComponent<AudioSource>();
        if (NotTutorial){
            logger = GameObject.Find("XR Origin").GetComponent<ShootLogger>();
            AssistButton = GameObject.FindWithTag("AssistButton");
            DebugButton = GameObject.FindWithTag("DebugButton");
            FPSCounter = GameObject.FindWithTag("FPSCounter");
        }
    }

    public void OnTriggerEnter(Collider collision) {

        //Checks if player shoots own hand
        if(collision.gameObject.tag == "Right Hand" ^ collision.gameObject.tag == "Left Hand"){
            return;
        }


        if(collision.gameObject.tag == "StartButton"){
            
            //using scipt object allows for a call to main when the StartButton has collided
            script = cannon.GetComponent<FireTarget>();
            script.main(AssistMode, DebugMode);

            //sets buttons to be inactive so that they will not change while game is going on
            GameObject.Find("Buttons").SetActive(false);
            //returns are used as multiple collisions could happen between parent and child

            return;
        }

        if(collision.gameObject.tag == "SceneSwitchButton"){
            
                if(SceneManager.GetActiveScene().name == "PistolGameModified"){
                    SceneManager.LoadScene(sceneName:"ProjectileBlockerModified");
                }
                else if(SceneManager.GetActiveScene().name == "PistolGameUnmodified"){
                    SceneManager.LoadScene(sceneName:"ProjectileBlockerUnmodified");
                }
        }

        if(collision.gameObject.tag == "AssistButton" ^ collision.gameObject.tag == "DebugButton"){
            /*ButtonRenderer allows us to alter the material of an object
            so that we can change the colour when it is active or unactive
            */
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
                //toggling debug button will clear it
                DebugMode = !DebugMode;
                if(DebugMode){
                    this.gameObject.transform.GetComponent<Renderer>().material = VisibleMaterial; 
                    ButtonRenderer.material.color =  Color.green;
                    DebugText.text += "Debug:\n";
                    FPSCounter.SetActive(true);
                }else{
                    this.gameObject.transform.GetComponent<Renderer>().material = InvisibleMaterial;
                    ButtonRenderer.material.color =  Color.red;
                    DebugText.text = "";
                    FPSCounter.SetActive(false);
                }
                break;
            }
            return;
        }

        if(collision.gameObject.tag == "ContinueButton"){
            continueGame = false;
            Destroy(collision.gameObject.transform.parent.gameObject);
            return;
        }





        //not untagged is how we describe the different sections of the target
        if(collision.gameObject.tag != "Untagged"){

            score++;
            audioData.Play(0);


            if(NotTutorial){
                ScoreText.text = "Score: " + score.ToString();
                logger.falseshot = collision.gameObject.tag;
                logger.targetHitTime = Time.time.ToString();
            }


            if(collision.gameObject.tag == "OuterRing"){
                noOfHits++;
            }



            //Destroys the target by using the parent
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }


    public int getScore(){
        return score;
    }
    public int getNoOfHits(){
        return noOfHits;
    }

    public bool getContinue(){
        return continueGame;
    }
    public void setContinue(bool value){
        continueGame = value;
    }
}