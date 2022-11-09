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
    public GameObject target;
    public Transform spawnPoint;
    public int numberOfTargets = 10;
    public float fireSpeed = 20;
    public int rateOfFire = 3;


    private AudioSource audioData;
    private GameObject flash, smoke;
    private GameObject Buttons;
    
    private GameObject AimCylinder;
    private bool AssistMode,DebugMode;
    private Vector3 OriginalSize;
    private TMP_Text DebugText;

    void Start(){
        Buttons = GameObject.Find("Buttons");
        DebugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        audioData = GetComponent<AudioSource>();
        AimCylinder = GameObject.Find("pistol/Cylinder");
        flash = GameObject.FindWithTag("CannonFlash");
        smoke = GameObject.FindWithTag("Smoke");
        flash.SetActive(false);
        smoke.SetActive(false);
    }



    public void main(bool AssistMode, bool DebugMode){
        this.AssistMode = AssistMode;
        this.DebugMode = DebugMode;
        StartCoroutine(FireTargets());
    }

    IEnumerator FireTargets(){
        Vector3 scaleChange = new Vector3(0.04f/numberOfTargets, 0.0f, 0.04f/numberOfTargets);
        OriginalSize = AimCylinder.transform.localScale;
        if(DebugMode){
            DebugText.text += "Default AimCylinderSize: " + OriginalSize.ToString() + "\n";
            DebugText.text += "numberOfTargets: " + numberOfTargets.ToString() + "\n";
            DebugText.text += "fireSpeed" + fireSpeed.ToString() + "\n";
            DebugText.text += "rateofFire" + rateOfFire.ToString() + "\n";
        }
        yield return new WaitForSeconds(3);
        smoke.SetActive(true);
        for(int i = 0; i<numberOfTargets;i++){
            GameObject spawnedTarget = Instantiate(target);
            flash.SetActive(true);
            spawnedTarget.transform.position = spawnPoint.position;
            spawnedTarget.GetComponent<Rigidbody>().velocity  = spawnPoint.forward * fireSpeed;
            audioData.Play(0);
            Destroy(spawnedTarget, 2 * rateOfFire);
            yield return new WaitForSeconds(0.2f);
            flash.SetActive(false);
            yield return new WaitForSeconds(rateOfFire);
            if(AssistMode){
                AimCylinder.transform.localScale += scaleChange;
                if(DebugMode){
                    DebugText.text += "AimCylinderSize: " + AimCylinder.transform.localScale.ToString() + "\n";
                }
            }
        }
        yield return new WaitForSeconds(5);
        smoke.SetActive(false);
        Buttons.SetActive(true);
        AimCylinder.transform.localScale = OriginalSize;
    }
}
