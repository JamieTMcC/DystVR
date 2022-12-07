using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TutorialScript : MonoBehaviour
{
    public TMP_Text tutorialText;
    public float waitTime = 3.0f;
    public LookingAtTarget lookingAtTarget;
    public AtTargetZone atTargetZone;
    public GameObject target, tutorialZone, tableandgun, tableAndBalls, continueButton, resetButton;
    CollideAndDestroy collideAndDestroy;
    Vector3[] positions1 = new Vector3[4];
    Vector3[] positions2 = new Vector3[4];
    Quaternion rotation1;
    Quaternion rotation2;
    float spawnTime;
    public int x = 0;


    // Start is called before the first frame update
    void Start()
    {   
        spawnTime = waitTime/12f;
        positions1[0] = new Vector3(25,1,0);
        positions1[1] = new Vector3(25,4,0);
        positions1[2] = new Vector3(25,1,9);
        positions1[3] = new Vector3(25,4,9);
        int i = 0;
        foreach(Vector3 pos in positions1){
            positions2[i] = new Vector3(5, pos.y, pos.z);
            i++;
        }
        rotation1 = Quaternion.Euler(0, 15, 0);
        rotation2 = Quaternion.Euler(0, -15, 180);
        StartCoroutine(StartTutorial());
    }

    IEnumerator StartTutorial(){
        
            yield return UpdateTutorial("This is a tutorial");
            yield return UpdateTutorial("Try looking around at the targets");

            //generate 1st set of targets
            foreach(Vector3 i in positions1){
                yield return new WaitForSeconds(spawnTime);
                Instantiate(target, i, rotation1);
            }
            
            //check if player has looked at all targets
            while(x < 4){                            
                yield return new WaitForSeconds(spawnTime);
                x = lookingAtTarget.getX();
                tutorialText.text = "You have looked at " + x + " targets";
            }
            
            
            yield return UpdateTutorial("Well Done");
            yield return UpdateTutorial("You can look around using the right analog stick");
            yield return UpdateTutorial("Look at the targets behind you to continue");
            
            //generate 2nd set of targets
            foreach(Vector3 i in positions2){
                yield return new WaitForSeconds(spawnTime);
                Instantiate(target, i, rotation2);
            }

            //check if player has looked at all targets
            while(x < 8){                            
                yield return new WaitForSeconds(spawnTime);
                x = lookingAtTarget.getX();
                tutorialText.text = "You have looked at " + (x-4) + " targets";
            }


            yield return UpdateTutorial("Well Done");
            yield return UpdateTutorial("You can move around using the left analog stick");
            yield return UpdateTutorial("Move to the red zone to continue");
            
            //make a red zone for player to walk to
            Instantiate(tutorialZone, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            while(!atTargetZone.getReached()){
                yield return new WaitForSeconds(spawnTime);
            }

            yield return UpdateTutorial("Well Done");
            yield return UpdateTutorial("The next part of the tutorial will cover shooting");
            yield return UpdateTutorial("Shoot the targets");
            yield return UpdateTutorial("A outer ring shot is worth 1 point");
            yield return UpdateTutorial("A inner ring shot is worth 2 points");
            yield return UpdateTutorial("A bullseye shot is worth 3 points");
            
            
            
            //Shooting Tutorial


            tableandgun = Instantiate(tableandgun);
            collideAndDestroy = GameObject.FindWithTag("AimCylinder").GetComponent<CollideAndDestroy>();
            collideAndDestroy.setContinue(true);
            x = 0;
            bool makeTargets = true;
            
            //checks if the player has shot the continue button
            while(collideAndDestroy.getContinue()){
                if(x != collideAndDestroy.getNoOfHits()){
                    makeTargets = true;
                }
                x = collideAndDestroy.getNoOfHits();
                if(x%4 == 0 && makeTargets){
                    
                    //makes targets
                    foreach(Vector3 i in positions1){
                       yield return new WaitForSeconds(spawnTime);
                        Instantiate(target, i, rotation1);
                    }

                    makeTargets = false;
                    
                    if(x == 4) Instantiate(continueButton).transform.position += new Vector3(-3,0,0);
                    if(x%3 == 0) tutorialText.text = "You can continue by shooting the Continue button";
                }
                yield return new WaitForSeconds(1); 
                tutorialText.text = "Score: " + collideAndDestroy.getScore();
    
            }

            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
            foreach(GameObject t in targets){
                Destroy(t);
            }

            Destroy(tableandgun);

            yield return UpdateTutorial("Well Done");
            yield return UpdateTutorial("The next part of the tutorial will cover the projectile blocking game");
            yield return UpdateTutorial("The aim of the game is to block as many projectiles as possible");
            tutorialText.text = "Push the balls away using your hands";

            GameObject tandb = Instantiate(tableAndBalls);
            
            GameObject LHand = GameObject.FindWithTag("Left Hand");
            GameObject RHand = GameObject.FindWithTag("Right Hand");
            LHand.GetComponent<BoxCollider>().isTrigger = false;
            RHand.GetComponent<BoxCollider>().isTrigger = false;


            GameObject ContButton = Instantiate(continueButton);
            ContButton.transform.position += new Vector3(-4,0,0.5f);
            ContButton.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = true;
            
            
            GameObject ResetButton = Instantiate(resetButton);
            ResetButton.transform.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = true;
            ResetButton.transform.position += new Vector3(-1,0,0.5f);
            
            
            while(!LHand.GetComponent<ButtonPress>().continueGame && !RHand.GetComponent<ButtonPress>().continueGame){
                yield return new WaitForSeconds(spawnTime);
                if(LHand.GetComponent<ButtonPress>().reset || RHand.GetComponent<ButtonPress>().reset){
                    Destroy(tandb);
                    tandb = Instantiate(tableAndBalls);
                    LHand.GetComponent<ButtonPress>().reset = false;
                    RHand.GetComponent<ButtonPress>().reset = false;
                }
            }
            Destroy(tandb);
            
            yield return UpdateTutorial("The proper game will involve balls which move with velocity towards you...");
            yield return UpdateTutorial("and a goal which you must protect");
            yield return UpdateTutorial("Tutorial Complete - Moving to Shooting Game");
            
            SceneManager.LoadScene(sceneName:"PistolGameUnmodified");
    
    }

    IEnumerator UpdateTutorial(string s){
            tutorialText.text = s;
            yield return new WaitForSeconds(waitTime);
    }
}
