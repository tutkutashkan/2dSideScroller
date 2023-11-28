using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class coincollect : MonoBehaviour
{
    public  GameManager game;

    public TMP_Text scoreText;


    // Update is called once per frame
    void Update()
    {
        scoreText.text = "X " + game.coins.ToString();
    }
}
