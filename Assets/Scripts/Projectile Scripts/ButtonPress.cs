/*
Handles collisions between the buttons and hands
*/ 



using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPress : MonoBehaviour
{

    private GameObject cannon, goalZone, buttons, XROrigin;
    public GameObject FPSCounter;//FPSCounter needs to be disabled at startup and since we run this twice, find can't be used

    private Vector3 startPosition;
    public Material visible,invisible;
    public bool debugMode, assistMode;
    public GameObject PaddleR,PaddleL;
    public ActionBasedContinuousMoveProvider LocoScript;
    private TMP_Text DebugText;

    void Start(){
        cannon = GameObject.Find("Cannon");
        goalZone = GameObject.Find("Goal");
        buttons = GameObject.Find("Buttons");
        DebugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        XROrigin = GameObject.Find("XR Origin");
        startPosition = GameObject.Find("StartPoint").transform.position;
        startPosition.y = XROrigin.transform.position.y;
    }



    void OnTriggerEnter(Collider col){
        
        var buttonRenderer = col.gameObject.transform.GetComponent<Renderer>();
        switch(col.gameObject.tag){
            case "SceneSwitchButton":
                SceneManager.LoadScene(sceneName:"PistolGameUnmodified");
                break;
            case "StartButton":
                FireBall script = cannon.GetComponent<FireBall>();
                buttons.SetActive(false);
                XROrigin.transform.position = startPosition;
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
