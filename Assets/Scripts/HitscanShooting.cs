using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class HitscanShooting : MonoBehaviour
{
    AudioSource audioData;
    public AudioClip clip1;
    XRGrabInteractable grabbable;
    public GameObject muzzleFlash;
    public GameObject aimCollider;
    // Start is called before the first frame update


    void Start()
    {
        audioData = GetComponent<AudioSource>();
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(Fire);
    }


    public void Fire(ActivateEventArgs arg){
        audioData.PlayOneShot(clip1);
        muzzleFlash.SetActive(true);
        aimCollider.SetActive(true);
        Invoke("MuzzleFlashTimeOut",0.1f);
        Invoke("TurnOffCollider",0.02f);
    }

    public void MuzzleFlashTimeOut(){
        muzzleFlash.SetActive(false);
    }

    public void TurnOffCollider(){
        aimCollider.SetActive(false);
    }
}
