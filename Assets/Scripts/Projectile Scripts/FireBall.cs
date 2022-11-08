using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class FireBall : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPoint;
    public int numberOfTargets = 10;
    public float fireSpeed = 5;
    public int rateOfFire = 3;


    private AudioSource audioData;
    private GameObject flash, smoke;
    private GameObject buttons;
    private BoxCollider BlockerL,BlockerR;
    public GameObject PaddleL,PaddleR;
    private bool AssistMode,DebugMode;
    private TMP_Text DebugText;

    void Start(){
        buttons = GameObject.Find("Buttons");
        DebugText = GameObject.Find("DebugText").GetComponent<TMP_Text>();
        audioData = GetComponent<AudioSource>();
        BlockerL = GameObject.FindWithTag("Left Hand").GetComponent<BoxCollider>();
        BlockerR = GameObject.FindWithTag("Right Hand").GetComponent<BoxCollider>();
        flash = GameObject.FindWithTag("CannonFlash");
        smoke = GameObject.FindWithTag("Smoke");
        flash.SetActive(false);
        smoke.SetActive(false);
    }





    public void main(bool AssistMode, bool DebugMode){
        this.AssistMode = AssistMode;
        this.DebugMode = DebugMode;
        StartCoroutine(FireBalls());
    }

    IEnumerator FireBalls(){
        BlockerL.isTrigger = false;
        BlockerR.isTrigger = false;
        float scaleChange = 1.5f/numberOfTargets;
        Vector3 OriginalSize = BlockerL.size;
        float x = OriginalSize.x;
        float y = OriginalSize.y;
        float z = OriginalSize.z;
        if(DebugMode){
            DebugText.text += "Default Hand Blocker Radius: " + OriginalSize.ToString() + "\n";
            DebugText.text += "numberOfTargets: " + numberOfTargets.ToString() + "\n";
            DebugText.text += "fireSpeed" + fireSpeed.ToString() + "\n";
            DebugText.text += "rateofFire" + rateOfFire.ToString() + "\n";
        }



        yield return new WaitForSeconds(3);
        smoke.SetActive(true);
        //smoke is active for the full duration of the targets being fired
        for(int i = 0; i<numberOfTargets;i++){
            Fire();
            flash.SetActive(true);
            yield return new WaitForSeconds(0.2f);//makes a flash for a fraction of a second 
            flash.SetActive(false);
            yield return new WaitForSeconds(rateOfFire);
            ModeActions(scaleChange,x,y,z);
        }


        //resets buttons and smoke after 5 seconds;
        yield return new WaitForSeconds(5);
        BlockerL.isTrigger = true;
        BlockerR.isTrigger = true;
        ResetScene(OriginalSize);
    }


    void ModeActions(float scaleChange,float x,float y, float z){
        if(AssistMode){
            BlockerL.size += new Vector3(scaleChange*x,scaleChange*y,scaleChange*z);
            BlockerR.size =  BlockerL.size;
            if(DebugMode){
                DebugText.text += "Hand Blocker size: " + BlockerL.size.ToString() + "\n";
                PaddleL.gameObject.transform.localScale = BlockerL.size;
                PaddleR.gameObject.transform.localScale = BlockerL.size;

            }
        }
    }

    void Fire(){
        GameObject spawnedProjectile = Instantiate(projectile);
        spawnedProjectile.transform.position = spawnPoint.position;
        spawnedProjectile.GetComponent<Rigidbody>().velocity  = spawnPoint.forward * fireSpeed;
        audioData.Play(0);

    }




    void ResetScene(Vector3 OriginalSize){
        smoke.SetActive(false);
        buttons.SetActive(true);
        BlockerL.size = OriginalSize;
        BlockerR.size = OriginalSize;
    }
}
