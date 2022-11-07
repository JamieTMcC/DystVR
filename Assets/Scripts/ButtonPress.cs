using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{

    public GameObject Cannon;
    public bool DebugMode, AssistMode;


    void OnTriggerEnter(Collider col){
        
        var ButtonRenderer = col.gameObject.transform.GetComponent<Renderer>();
        switch(col.gameObject.tag){
            case "StartButton":
                FireBall script = Cannon.GetComponent<FireBall>();
                col.gameObject.transform.parent.gameObject.SetActive(false);
                script.main(AssistMode,DebugMode);
                break;
            case "AssistButton":
                ButtonRenderer = col.gameObject.transform.GetComponent<Renderer>();
                AssistMode = !AssistMode;
                if(AssistMode){
                    ButtonRenderer.material.color =  Color.green;
                }else{
                    ButtonRenderer.material.color =  Color.red;
                }
                break;
            case "DebugButton":
                ButtonRenderer = col.gameObject.transform.GetComponent<Renderer>();
                DebugMode= !DebugMode;
                if(DebugMode){
                    ButtonRenderer.material.color =  Color.green;
                }else{
                    ButtonRenderer.material.color =  Color.red;
                }
                break;
        }
    }






}
