using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    public  GameManager game;
    public TMP_Text gameOverText;
    void Update()
    {  
        if (game.lives == 0)
        {
            gameOverText.text = "Game Over";
        }
    }
}
