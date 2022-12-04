using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialScript : MonoBehaviour
{
    public TMP_Text tutorialText;
    public float speedup = 1.0f;
    public LookingAtTarget lookingAtTarget;
    public AtTargetZone atTargetZone;
    public GameObject target, tutorialZone, tableandgun, continueButton;
    CollideAndDestroy collideAndDestroy;
    List<Vector3> positions1 = new List<Vector3>();
    List<Vector3> positions2 = new List<Vector3>();
    Quaternion rotation1;
    Quaternion rotation2;
    public int x = 0;


    // Start is called before the first frame update
    void Start()
    {
        positions1.Add(new Vector3(25,1,0));
        positions1.Add(new Vector3(25,4,0));
        positions1.Add(new Vector3(25,1,9));
        positions1.Add(new Vector3(25,4,9));
        foreach(Vector3 pos in positions1){
            positions2.Add(new Vector3(5, pos.y, pos.z));
        }
        rotation1 = Quaternion.Euler(0, 15, 0);
        rotation2 = Quaternion.Euler(0, -15, 180);
        StartCoroutine(UpdateTutorial());
        tutorialText.text = "Hello";
    }

    IEnumerator UpdateTutorial(){
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "This is a tutorial";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "Try looking around at the targets"; 

            foreach(Vector3 i in positions1){
                yield return new WaitForSeconds(0.25f*speedup);
                Instantiate(target, i, rotation1);
            }
            
  
            while(x < 4){                            
                yield return new WaitForSeconds(1*speedup);
                x = lookingAtTarget.getX();
                tutorialText.text = "You have looked at " + x + " targets";
            }
            
            
            tutorialText.text = "Well Done";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "You can look around using the right analog stick";
            yield return new WaitForSeconds(2*speedup);
            tutorialText.text = "Look at the targets behind you to continue";
            
            foreach(Vector3 i in positions2){
                yield return new WaitForSeconds(0.25f*speedup);
                Instantiate(target, i, rotation2);
            }
            while(x < 8){                            
                yield return new WaitForSeconds(1*speedup);
                x = lookingAtTarget.getX();
                tutorialText.text = "You have looked at " + (x-4) + " targets";
            }


            tutorialText.text = "Well Done";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "You can move around using the left analog stick";
            yield return new WaitForSeconds(2*speedup);
            tutorialText.text = "Move to the red zone to continue";
            
            Instantiate(tutorialZone, new Vector3(0,0,0), Quaternion.Euler(0,0,0));
            while(!atTargetZone.getReached()){
                yield return new WaitForSeconds(1*speedup);
            }
            tutorialText.text = "Well Done";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "This next portion of the tutorial will cover shooting";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "Shoot the targets";
            yield return new WaitForSeconds(1.5f*speedup);
            tutorialText.text = "A outer ring shot is worth 1 point";
            yield return new WaitForSeconds(1.5f*speedup);
            tutorialText.text = "A inner ring shot is worth 2 points";
            yield return new WaitForSeconds(1.5f*speedup);
            tutorialText.text = "A bullseye shot is worth 3 points";
            yield return new WaitForSeconds(1.5f*speedup);
            
            
            
            //Shooting Tutorial
            tableandgun = Instantiate(tableandgun);
            collideAndDestroy = GameObject.FindWithTag("AimCylinder").GetComponent<CollideAndDestroy>();
            collideAndDestroy.setContinue(true);
            x = 0;
            bool makeTargets = true;
            while(collideAndDestroy.getContinue()){
                if(x != collideAndDestroy.getNoOfHits()){
                    makeTargets = true;
                }
                x = collideAndDestroy.getNoOfHits();
                if(x%4 == 0 && makeTargets){
                    
                    //makes targets
                    foreach(Vector3 i in positions1){
                       yield return new WaitForSeconds(0.25f*speedup);
                        Instantiate(target, i, rotation1);
                    }

                    makeTargets = false;
                    
                    if(x == 4) Instantiate(continueButton).transform.position += new Vector3(-3,0,0);
                    if(x%3 == 0) tutorialText.text = "You can continue by shooting the Continue button";
                }
                yield return new WaitForSeconds(1*speedup); 
                tutorialText.text = "Score: " + collideAndDestroy.getScore();
    
            }

            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
            foreach(GameObject t in targets){
                Destroy(t);
            }
            
            
            
            tutorialText.text = "Well Done";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "The next part of the tutorial will cover the projectile blocking game";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "The aim of the game is to block as many projectiles as possible";
            yield return new WaitForSeconds(3*speedup); 
    }
}
