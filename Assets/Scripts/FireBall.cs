using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class FireBall : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    public AudioSource audioData;
    public int numberOfTargets;
    public GameObject flash, smoke;
    public int rateOfFire = 3;
    public GameObject StartButton, AssistButton, DebugButton;
    public GameObject BlockerL,BlockerR;
    private bool AssistMode,DebugMode;
    private Vector3 OriginalSize;
    public TMP_Text DebugText;

    public void main(bool AssistMode, bool DebugMode){
        this.AssistMode = AssistMode;
        this.DebugMode = DebugMode;
        StartCoroutine(FireBalls());
    }

    IEnumerator FireBalls(){
        Vector3 scaleChange = new Vector3(0.03f/numberOfTargets, 0.0f, 0.03f/numberOfTargets);
        OriginalSize = BlockerL.transform.localScale;
        yield return new WaitForSeconds(3);
        smoke.SetActive(true);
        for(int i = 0; i<numberOfTargets;i++){
            GameObject spawnedProjectile = Instantiate(projectile);
            flash.SetActive(true);
            spawnedProjectile.transform.position = spawnPoint.position;
            spawnedProjectile.GetComponent<Rigidbody>().velocity  = spawnPoint.forward * fireSpeed;
            audioData.Play(0);
            yield return new WaitForSeconds(0.2f);
            flash.SetActive(false);
            yield return new WaitForSeconds(rateOfFire);
            if(AssistMode){
                BlockerL.transform.localScale += scaleChange;
                BlockerR.transform.localScale += scaleChange;
                if(DebugMode){
                    DebugText.text += "BlockerCylinderSize: " + BlockerL.transform.localScale.ToString() + "\n";
                }
            }
        }
        yield return new WaitForSeconds(5);
        smoke.SetActive(false);
        StartButton.SetActive(true);
        AssistButton.SetActive(true);
        DebugButton.SetActive(true);
        BlockerL.transform.localScale = OriginalSize;
        BlockerR.transform.localScale = OriginalSize;
    }

    public int getRateofFire(){
        return rateOfFire;
    }
}
