using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class BurstFireOnActivate : MonoBehaviour
{

    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 5;
    AudioSource audioData;
    // Start is called before the first frame update


    void Start()
    {
        audioData = GetComponent<AudioSource>();
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(burst);
    }

    public void burst(ActivateEventArgs arg){
        int[] randints = {0,0,0};
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
            randints[j] = Random.Range(0,30);
            }
            FireBullet(randints);
        }
        
    }

    public void FireBullet(int[] rotations){
        Vector3 rotationToAdd = new Vector3(rotations[0], rotations[1], rotations[2]);

        GameObject spawnedBullet = Instantiate(bullet);
        spawnedBullet.transform.position = spawnPoint.position;
        Debug.Log(rotations[0]);
        spawnedBullet.transform.eulerAngles  = spawnPoint.transform.eulerAngles;
        spawnedBullet.transform.Rotate(rotationToAdd);
        spawnedBullet.GetComponent<Rigidbody>().velocity  = rotationToAdd * fireSpeed;
        audioData.Play(0);
        Destroy(spawnedBullet, 5);

    }
}
