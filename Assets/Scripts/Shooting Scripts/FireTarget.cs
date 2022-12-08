/*
Handles the firing of the targets when the start button is used
Also handles the increase in size of the colliders if assistmode is enabled
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class FireTarget : MonoBehaviour
{
    public GameObject target,aimCylinder;
    public Transform spawnPoint;
    public int numberOfTargets = 10;
    public int numberOfSets = 5;
    public float fireSpeed = 20;
    public int rateOfFire = 3;


    private AudioSource audioData;
    private GameObject flash, smoke;
    private GameObject buttons;

    private bool assistMode,debugMode;
    private Vector3 originalSize;
    private TMP_Text debugText, timerText;

    void Start(){
        buttons = GameObject.Find("Buttons");
        debugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        timerText = GameObject.Find("Timer").GetComponent<TMP_Text>();
        audioData = GetComponent<AudioSource>();
        flash = GameObject.FindWithTag("CannonFlash");
        smoke = GameObject.FindWithTag("Smoke");
        flash.SetActive(false);
        smoke.SetActive(false);
    }



    public void main(bool assistMode, bool debugMode){
        this.assistMode = assistMode;
        this.debugMode = debugMode;
        StartCoroutine(FireTargets());
    }

    IEnumerator FireTargets(){
        Vector3 scaleChange = new Vector3(0.07f/numberOfSets, 0.0f, 0.07f/numberOfSets);
        originalSize = aimCylinder.transform.localScale;
        if(debugMode){
            debugText.text += "Default aimCylinderSize: " + originalSize.ToString() + "\n";
            debugText.text += "numberOfTargets: " + numberOfTargets.ToString() + "\n";
            debugText.text += "fireSpeed" + fireSpeed.ToString() + "\n";
            debugText.text += "rateofFire" + rateOfFire.ToString() + "\n";
        }
        yield return new WaitForSeconds(3);
        smoke.SetActive(true);
        for(int j = 0; j<numberOfSets;j++){

            for(int i = 0; i<numberOfTargets;i++){
                GameObject spawnedTarget = Instantiate(target);
                flash.SetActive(true);
                Invoke("DeactivateFlash", 0.2f);

                spawnedTarget.transform.position = spawnPoint.position;
                spawnedTarget.GetComponent<Rigidbody>().velocity  = spawnPoint.forward * fireSpeed;
                
                audioData.Play(0);
                Destroy(spawnedTarget, 2 * rateOfFire);
                
                yield return new WaitForSeconds(rateOfFire);
                if(assistMode && debugMode) debugText.text += "aimCylinderSize: " + aimCylinder.transform.localScale.ToString() + "\n";
            }
            if(assistMode) aimCylinder.transform.localScale += scaleChange;
            
            int timetillrestart = 15;
            
            while(timetillrestart > 0){
                timerText.text = "Time till restart: " + timetillrestart.ToString() + "\n";
                yield return new WaitForSeconds(1);
                timetillrestart--;
            }

        }

        yield return new WaitForSeconds(5);
        smoke.SetActive(false);
        buttons.SetActive(true);
        aimCylinder.transform.localScale = originalSize;
    }

    void DeactivateFlash(){
        flash.SetActive(false);
    }
}
