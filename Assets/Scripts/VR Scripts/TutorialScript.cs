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
    public GameObject target, tutorialZone, tableandgun;
    List<Vector3> positions1 = new List<Vector3>();
    List<Vector3> positions2 = new List<Vector3>();
    Quaternion rotation1;
    Quaternion rotation2;
    int x = 0;


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
            tutorialText.text = "This concludes the first portion of the tutorial";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "This next portion of the tutorial will cover shooting";
            yield return new WaitForSeconds(3*speedup);
            Instantiate(tableandgun);
            foreach(Vector3 i in positions1){
                yield return new WaitForSeconds(0.25f*speedup);
                Instantiate(target, i, rotation1);
            }
    }
}
