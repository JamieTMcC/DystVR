using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class HitscanShooting : MonoBehaviour
{
    private AudioSource audioData;
    public AudioClip clip1;
    private XRGrabInteractable grabbable;
    private GameObject muzzleFlash, aimCollider;
    // Start is called before the first frame update


    void Start()
    {
        audioData = GetComponent<AudioSource>();
        grabbable = GetComponent<XRGrabInteractable>();
        muzzleFlash = GameObject.FindWithTag("PistolFlash");
        aimCollider = GameObject.FindWithTag("AimCylinder");
        muzzleFlash.SetActive(false);
        aimCollider.SetActive(false);
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
