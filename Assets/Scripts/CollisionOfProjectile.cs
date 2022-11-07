using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CollisionOfProjectile : MonoBehaviour
{

    public int score;
    public GameObject Cannon;
    public TMP_Text Score;
    public AudioSource GoalSound;



    void OnCollisionEnter(Collision col){
        

        switch(col.gameObject.tag){
            case "RightHand":
            case "LeftHand":
            score++;
            break;
            case "Wall":
            break;
            case "Goal":
            GoalSound.Play(0);
            score--;
            break;
            Score.text = "Score: " + score.ToString();
        }
        Destroy(gameObject, 6);



    }

}
