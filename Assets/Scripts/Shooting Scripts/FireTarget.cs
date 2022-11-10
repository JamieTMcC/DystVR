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
    private GameObject buttons;
    
    private GameObject aimCylinder;
    private bool assistMode,debugMode;
    private Vector3 originalSize;
    private TMP_Text debugText;

    void Start(){
        buttons = GameObject.Find("Buttons");
        debugText = GameObject.Find("debugText").GetComponent<TMP_Text>();
        audioData = GetComponent<AudioSource>();
        aimCylinder = GameObject.Find("pistol/Cylinder");
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
        Vector3 scaleChange = new Vector3(0.04f/numberOfTargets, 0.0f, 0.04f/numberOfTargets);
        originalSize = aimCylinder.transform.localScale;
        if(debugMode){
            debugText.text += "Default aimCylinderSize: " + originalSize.ToString() + "\n";
            debugText.text += "numberOfTargets: " + numberOfTargets.ToString() + "\n";
            debugText.text += "fireSpeed" + fireSpeed.ToString() + "\n";
            debugText.text += "rateofFire" + rateOfFire.ToString() + "\n";
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
            if(assistMode){
                aimCylinder.transform.localScale += scaleChange;
                if(debugMode){
                    debugText.text += "aimCylinderSize: " + aimCylinder.transform.localScale.ToString() + "\n";
                }
            }
        }
        yield return new WaitForSeconds(5);
        smoke.SetActive(false);
        buttons.SetActive(true);
        aimCylinder.transform.localScale = originalSize;
    }
}
