/*
Handles the firing of the projectiles when the start button is used
Also handles the increase in size of the colliders if assistmode is enabled
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class FireBall : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPoint;
    public int numberOfTargets = 5,numberOfSets = 6;
    public float fireSpeed = 10;
    public int rateOfFire = 3;
    public int timeDelay = 5;


    private AudioSource audioData;
    private GameObject buttons;
    private BoxCollider BlockerL,BlockerR;
    public GameObject PaddleL,PaddleR;
    public bool AssistMode,DebugMode;
    private TMP_Text DebugText,timerText;
    private ProjectileLogger logger;

    void Start(){
        logger = GameObject.Find("XR Origin").GetComponent<ProjectileLogger>();
        buttons = GameObject.Find("Buttons");
        DebugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        timerText = GameObject.Find("timerText").GetComponent<TMP_Text>();
        timerText.text = "";
        audioData = GetComponent<AudioSource>();
        BlockerL = GameObject.FindWithTag("Left Hand").GetComponent<BoxCollider>();
        BlockerR = GameObject.FindWithTag("Right Hand").GetComponent<BoxCollider>();
    }





    public void main(bool AssistMode, bool DebugMode){
        StartCoroutine(FireBalls());
    }

    IEnumerator FireBalls(){
        logger.startLogging = true;
        BlockerL.isTrigger = false;
        BlockerR.isTrigger = false;
        float scaleChange = 1.5f/numberOfSets;
        Vector3 OriginalSize = BlockerL.size;
        logger.originalColliderSize = BlockerL.size.x.ToString() + " " + BlockerL.size.y.ToString() + " " + BlockerL.size.z.ToString();

        //waits 3 seconds before firing
        yield return new WaitForSeconds(3);

        for(int j = 0; j<numberOfSets;j++){
            logger.setNumber = j;
            for(int i = 0; i<numberOfTargets;i++){
                    logger.projectileNumber = i;
                    Fire();
                    yield return new WaitForSeconds(rateOfFire);
                }
                ModeActions(scaleChange,OriginalSize);

            int t = timeDelay;
            while(t > 0 && j != numberOfSets-1){
                timerText.text = "Time till restart: " + t.ToString() + "s\n";
                yield return new WaitForSeconds(1);
                t--;
            }
            timerText.text = "";
            if(j == numberOfSets-1){
                timerText.text = "Test Complete";
                yield return new WaitForSeconds(2);
            }


        }
        //resets buttons and smoke after 5 seconds;
        yield return new WaitForSeconds(1);
        BlockerL.isTrigger = true;
        BlockerR.isTrigger = true;
        ResetScene(OriginalSize);
    }


    void ModeActions(float scaleChange,Vector3 OriginalSize){
        Debug.Log("Mode Actions");
        Debug.Log("AssistMode:" + AssistMode);
        Debug.Log("DebugMode:" + DebugMode);
        if(AssistMode){
            Debug.Log("Assist Mode is On");
            BlockerL.size += new Vector3(OriginalSize.x*scaleChange,OriginalSize.y*scaleChange,OriginalSize.z*scaleChange);
            BlockerR.size =  BlockerL.size;
            logger.colliderSize = BlockerL.size.x.ToString() + " " + BlockerL.size.y.ToString() + " " + BlockerL.size.z.ToString();
            Debug.Log(BlockerL.size.ToString());
            if(DebugMode){
                DebugText.text += "Hand Blocker size: " + BlockerL.size.ToString() + "\n";
                PaddleL.gameObject.transform.localScale = BlockerL.size;
                PaddleR.gameObject.transform.localScale = BlockerL.size;
            }
        }
    }

    void Fire(){
        logger.projectileFiredTime = Time.time.ToString();
        GameObject spawnedProjectile = Instantiate(projectile);
        spawnedProjectile.transform.position = spawnPoint.position;
        spawnedProjectile.GetComponent<Rigidbody>().velocity  = spawnPoint.forward * fireSpeed;
        audioData.Play(0);
    }




    void ResetScene(Vector3 OriginalSize){
        logger.stopLogging = true;
        logger.startLogging = false;
        BlockerL.size = OriginalSize;
        BlockerR.size = OriginalSize;
        buttons.SetActive(true);
        timerText.text = "";
    }
}
