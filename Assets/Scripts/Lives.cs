using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public  GameManager game;
    public GameObject heartimage1;
    public GameObject heartimage2;
    public GameObject heartimage3;

    // Update is called once per frame
    void Update()
    {  
        if(game.lives > 2) {
            heartimage3.SetActive(true);
        }
        if(game.lives == 2){
            heartimage3.SetActive(false);
            heartimage2.SetActive(true);
            heartimage1.SetActive(true);
        }
        if(game.lives == 1){
            heartimage2.SetActive(false);
            heartimage1.SetActive(true);
        }
        if(game.lives < 1){
            heartimage1.SetActive(false);
        }
    }
}
