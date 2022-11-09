/*
Calculates the frames per second every second
and displays if the object is active
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private TMP_Text FPSDisplay;

    void Start(){
        FPSDisplay = GetComponent<TMP_Text>();
        StartCoroutine(UpdateFPS());
    }


    IEnumerator UpdateFPS(){
        while (true){
            yield return new WaitForSeconds(1);
            FPSDisplay.text = "FPS: " + (int) (1f / Time.unscaledDeltaTime);;
        }
    }
}
