using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialScript : MonoBehaviour
{
    public TMP_Text tutorialText;
    public float speedup = 1.0f;
    public LookingAtTarget lookingAtTarget;
    public GameObject target;
    List<Vector3> Positions = new List<Vector3>();    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateTutorial());
        tutorialText.text = "Hello";
        Positions.Add(new Vector3(25,1,0));
        Positions.Add(new Vector3(25,4,0));
        Positions.Add(new Vector3(25,1,10));
        Positions.Add(new Vector3(25,4,10));
    }

    IEnumerator UpdateTutorial(){
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "This is a tutorial";
            yield return new WaitForSeconds(3*speedup);
            tutorialText.text = "Try looking around at the targets"; 

            foreach(Vector3 i in Positions){
                yield return new WaitForSeconds(0.25f*speedup);
                Instantiate(target);
                target.transform.position = i;
            }
            int x = 0;
            while(x < 3){                            
                yield return new WaitForSeconds(1*speedup);
                x = lookingAtTarget.getX();
                tutorialText.text = "You have looked at " + x + " targets";
            }
            tutorialText.text = "Well Done";



    }
}
