using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class BurstFireOnActivate : MonoBehaviour
{

    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 5;
    public float spread = 60;
    AudioSource audioData;
    public AudioClip clip1;
    public AudioClip clip2;
    public bool burstmode = false;
    XRGrabInteractable grabbable;
    public GameObject MuzzleFlash;
    // Start is called before the first frame update


    void Start()
    {
        audioData = GetComponent<AudioSource>();
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(fire);
    }


    public void fire(ActivateEventArgs arg){
        MuzzleFlash.SetActive(true);
        Invoke("MuzzleFlashTimeOut",0.1f);
        if(burstmode){
            burst();
        }else{
            FireBullet();
        }
    }

    public void MuzzleFlashTimeOut(){

        MuzzleFlash.SetActive(false);
    }
    void Update(){
        if(grabbable.isSelected && (Input.GetButtonDown("XRI_Left_PrimaryButton") || Input.GetButtonDown("XRI_Right_PrimaryButton"))){
            burstmode = !burstmode;
            audioData.PlayOneShot(clip2);
        }

    }

    public void burst(){
        float[] randints = {0.0f,0.0f,0.0f};
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
            randints[j] = Random.Range(0,spread);
            }
            BurstFireBullet(randints);
        }
        
    }

    public void BurstFireBullet(float[] rotations){
        Vector3 rotationToAdd = new Vector3(rotations[0]/360, rotations[1]/360, rotations[2]/360);

        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.transform.eulerAngles  = spawnPoint.transform.eulerAngles;
        spawnedBullet.transform.Rotate(rotationToAdd);
        spawnedBullet.GetComponent<Rigidbody>().velocity  = (spawnPoint.forward+rotationToAdd) * fireSpeed;
        audioData.PlayOneShot(clip1);
        Destroy(spawnedBullet, 5);

    }

    public void FireBullet(){
        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        spawnedBullet.GetComponent<Rigidbody>().velocity  = spawnPoint.forward * fireSpeed;
        audioData.Play(0);
        Destroy(spawnedBullet, 5);

    }
}
