using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class FireTarget : MonoBehaviour
{
    public GameObject target;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    public AudioSource audioData;
    public int numberOfTargets;
    public GameObject flash, smoke;
    public int rateOfFire = 3;
    public GameObject StartButton, AssistButton, DebugButton;
    public GameObject AimCylinder; 
    private bool AssistMode,DebugMode;
    private Vector3 OriginalSize;
    public TMP_Text DebugText;

    public void main(bool AssistMode, bool DebugMode){
        this.AssistMode = AssistMode;
        this.DebugMode = DebugMode;
        StartCoroutine(FireTargets());
    }

    IEnumerator FireTargets(){
        Vector3 scaleChange = new Vector3(0.04f/numberOfTargets, 0.0f, 0.04f/numberOfTargets);
        OriginalSize = AimCylinder.transform.localScale;
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
        StartButton.SetActive(true);
        AssistButton.SetActive(true);
        DebugButton.SetActive(true);
        AimCylinder.transform.localScale = OriginalSize;
    }
}
