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
    public int timeDelay = 15;

    private ShootLogger logger;
    private AudioSource audioData;
    private GameObject buttons;

    private bool assistMode,debugMode;
    private Vector3 originalSize;
    private TMP_Text debugText, timerText;

    void Start(){
        buttons = GameObject.Find("Buttons");
        debugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        timerText = GameObject.Find("Timer").GetComponent<TMP_Text>();
        timerText.text = "";
        audioData = GetComponent<AudioSource>();
        logger = GameObject.Find("XR Origin").GetComponent<ShootLogger>();
    }



    public void main(bool assistMode, bool debugMode){
        this.assistMode = assistMode;
        this.debugMode = debugMode;
        StartCoroutine(FireTargets());
    }

    IEnumerator FireTargets(){
        logger.startLogging = true;
        Vector3 scaleChange = new Vector3(0.05f/numberOfSets, 0.0f, 0.05f/numberOfSets);
        originalSize = aimCylinder.transform.localScale;
        logger.defaultAimCylinderSize = originalSize.x.ToString();
        logger.aimCylinderSize = originalSize.x.ToString();
        if(debugMode){
            debugText.text += "Default aimCylinderSize: " + originalSize.ToString() + "\n";
            debugText.text += "numberOfTargets: " + numberOfTargets.ToString() + "\n";
            debugText.text += "fireSpeed" + fireSpeed.ToString() + "\n";
            debugText.text += "rateofFire" + rateOfFire.ToString() + "\n";
        }
        yield return new WaitForSeconds(3);
        for(int j = 0; j<numberOfSets;j++){
            logger.setNumber = j;
            for(int i = 0; i<numberOfTargets;i++){
                logger.targetNumber = i;

                GameObject spawnedTarget = Instantiate(target);

                spawnedTarget.transform.position = spawnPoint.position;
                spawnedTarget.GetComponent<Rigidbody>().velocity  = spawnPoint.forward * fireSpeed;
                logger.targetFiredTime = Time.time.ToString();

                audioData.Play(0);
                Destroy(spawnedTarget, 2 * rateOfFire);
                
                yield return new WaitForSeconds(rateOfFire);
                if(assistMode && debugMode) debugText.text += "aimCylinderSize: " + aimCylinder.transform.localScale.ToString() + "\n";
            }
            if(assistMode) aimCylinder.transform.localScale += scaleChange;
            logger.aimCylinderSize = aimCylinder.transform.localScale.x.ToString();
            
            int t = timeDelay;
            while(t > 0 && j != numberOfSets-1){
                timerText.text = "Time till restart: " + t.ToString() + "s\n";
                yield return new WaitForSeconds(1);
                t--;
            }
            timerText.text = "";

        }
        yield return new WaitForSeconds(1);
        aimCylinder.transform.localScale = originalSize;
        logger.startLogging = false;
        logger.stopLogging = true;
        buttons.SetActive(true);
    }
}
