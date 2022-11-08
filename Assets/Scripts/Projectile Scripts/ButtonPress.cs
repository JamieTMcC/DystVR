using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonPress : MonoBehaviour
{

    private GameObject cannon, FPSCounter, goalZone, buttons;
    public Material visible,invisible;
    public bool debugMode, assistMode;
    public GameObject PaddleR,PaddleL;

    private TMP_Text DebugText;

    void Start(){
        cannon = GameObject.Find("Cannon");
        FPSCounter = GameObject.Find("FPSCounter");
        goalZone = GameObject.Find("Goal");
        buttons = GameObject.Find("Buttons");
        DebugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
    }



    void OnTriggerEnter(Collider col){
        
        var buttonRenderer = col.gameObject.transform.GetComponent<Renderer>();
        switch(col.gameObject.tag){
            case "StartButton":
                FireBall script = cannon.GetComponent<FireBall>();
                buttons.SetActive(false);
                script.main(assistMode,debugMode);
                break;
            case "AssistButton":
                buttonRenderer = col.gameObject.transform.GetComponent<Renderer>();
                assistMode = !assistMode;
                if(assistMode){
                    buttonRenderer.material.color =  Color.green;
                }else{
                    buttonRenderer.material.color =  Color.red;
                }
                break;
            case "DebugButton":
                buttonRenderer = col.gameObject.transform.GetComponent<Renderer>();
                debugMode= !debugMode;
                if(debugMode){
                    //switch button to green
                    buttonRenderer.material.color =  Color.green;


                    FPSCounter.SetActive(true);
                    
                    buttonRenderer = goalZone.gameObject.transform.GetComponent<Renderer>();
                    buttonRenderer.material = visible;
                    
                    PaddleL.SetActive(true);
                    PaddleR.SetActive(true);

                    DebugText.text = "Debug: \n";

                }else{
                    buttonRenderer.material.color =  Color.red;
                    FPSCounter.SetActive(false);
                    buttonRenderer = goalZone.gameObject.transform.GetComponent<Renderer>();
                    buttonRenderer.material = invisible;
                    PaddleL.SetActive(false);
                    PaddleR.SetActive(false);
                    DebugText.text = "";
                }
                break;
        }
    }






}
