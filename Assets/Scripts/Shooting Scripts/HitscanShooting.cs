/*
Handles the visual affects and collider activation of the gun when trigger is pulled
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class HitscanShooting : MonoBehaviour
{
    private AudioSource audioData;
    public AudioClip clip1;
    private XRGrabInteractable grabbable;
    private GameObject aimCollider,trueAimCollider;
    public ParticleSystem muzzleFlash;
    public SimpleShoot simpleShoot;
    public GameObject bullet;
    public Transform barrelLocation;
    private GameObject firedBullet;
    private bool firable;
    private ShootLogger logger;
    // Start is called before the first frame update


    void Start()
    {
        audioData = GetComponent<AudioSource>();
        grabbable = GetComponent<XRGrabInteractable>();
        aimCollider = GameObject.FindWithTag("AimCylinder");
        trueAimCollider = GameObject.FindWithTag("TrueAimCylinder");
        aimCollider.SetActive(false);
        trueAimCollider.SetActive(false);
        grabbable.activated.AddListener(Fire);
        firable = true;
        logger = GameObject.Find("XR Origin").GetComponent<ShootLogger>();
    }


    public void Fire(ActivateEventArgs arg){
        if(firable){
        firedBullet = Instantiate(bullet, barrelLocation.position, barrelLocation.rotation);
        firedBullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * 3000);
        simpleShoot.fire();
        audioData.PlayOneShot(clip1);
        muzzleFlash.Play();
        logger.aimCylinderActive = true;
        aimCollider.SetActive(true);
        trueAimCollider.SetActive(true);
        firable = false;
        Invoke("TurnOffCollider",0.02f);
        Invoke("MakeFireable",0.5f);
        }
    }

    public void TurnOffCollider(){
        aimCollider.SetActive(false);
        trueAimCollider.SetActive(false);
        logger.aimCylinderActive = false;
    }

    public void MakeFireable(){
        Destroy(firedBullet);
        firable = true;
    }
}
