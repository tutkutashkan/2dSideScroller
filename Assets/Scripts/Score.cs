using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public  GameManager game;

    public TMP_Text scoreText;


    // Update is called once per frame
    void Update()
    {
        scoreText.text = game.score.ToString();
    }
}
