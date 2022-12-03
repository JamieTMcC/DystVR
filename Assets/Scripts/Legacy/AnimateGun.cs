using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class AnimateGun : MonoBehaviour
{
    public Animator gunAnimation;
    public void fire(){
        gunAnimation.SetTrigger("Fire");
    }
}
