using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    private TMP_Text ScoreText;

    void Start(){
        ScoreText = GetComponent<TMP_Text>();
    }

    public void ScoreDisplay(){
        ScoreText.text = "Score: " + score.ToString();
    }
}
