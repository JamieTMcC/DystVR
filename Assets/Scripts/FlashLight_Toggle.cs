using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashLight_Toggle : MonoBehaviour
{
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(ToggleLight);
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleLight(ActivateEventArgs arg)
    {
        List<GameObject> Lights = new List<GameObject>();
        Lights.Add(Light1);
        Lights.Add(Light2);
        Lights.Add(Light3);
        Console.WriteLine(Lights);
        if(Light1.activeInHierarchy){
            foreach (GameObject i in Lights){
                i.SetActive(false);
                };
        }else{
            foreach (GameObject i in Lights){
                i.SetActive(true);
                };
        };
        audioData.Play(0);
    }
}   

