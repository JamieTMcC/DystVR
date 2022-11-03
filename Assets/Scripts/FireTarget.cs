using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireTarget : MonoBehaviour
{
    public GameObject target;
    public Transform spawnPoint;
    public float fireSpeed = 20;
    public AudioSource audioData;
    public int numberOfTargets;
    public GameObject flash;
    public GameObject smoke;
    public int rateOfFire = 3;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        StartCoroutine(FireTargets());
    }

    IEnumerator FireTargets(){
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
        }
        yield return new WaitForSeconds(5);
        Destroy(smoke);
    }
}
