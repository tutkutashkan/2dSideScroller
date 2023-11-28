using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    //public PlayerController player;
    public PlayerMovement player;
    public GameObject pauseMenu;
    public GameObject nextlevelpanel;
    public float respawnTime = 2.0f;
    public int lives = 3;
    public int score = 0;
    public int coins = 0;
    public int level;
    public int maps = 0;
    public Transform[] checkpoint;
    public int Point;



    //public ParticleSystem explosion;
    public void EnemyDestroyed(EnemyController enemy)
    {
        //explosion.transform.position = enemy.transform.position;
        //explosion.Play();
        //scoring system
        if (enemy.chasing){
            score += 100;
        } else if (enemy.jumping){
            score += 50;
        } else {
            score += 25;
        }
    }
    public void TrapDestroyed(TrapController trap)
    {
        //explosion.transform.position = trap.transform.position;
        //explosion.Play();
        //scoring system
        score += 200;
    }
    public void MapCollected(mapcontrol map){
        maps += 1;
    }
    public void HealthGain(Health health)
    {
        if(lives != 3){
            lives += 1;
        }
    }
    public void coinCollect(Coin coin)
    {
        coins += 1;
        score += 100;

    }
    public void Death()
    {
        // player's death
        //explosion.transform.position = player.transform.position;
        //explosion.Play();
        lives --;
        if (lives < 1 )
        {
            // if player has more than 1 live respawn in 3s
            Invoke(nameof(gameOver),3.0f);

        } else {
            Invoke(nameof(Respawn),respawnTime);
        }
    }
    private void Respawn()
    {   
        player.cl.enabled = true;
        player.RB.bodyType = RigidbodyType2D.Dynamic;
        player.transform.position = checkpoint[Point].position;
        // changing the layer of the player to respawn which is undestructible and change it back in 3s
        //player.gameObject.SetActive(true);
        player.animator.SetBool("Dead",false);
        Invoke(nameof(AfterRespawn), 1.0f);
    }
    private void AfterRespawn()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    private void gameOver()
    {
        FindObjectOfType<PauseMenu>().Pause();
    }
    public void loadingNextLevel()
    {
        if (level == 3)
        {
            FindObjectOfType<PauseMenu>().Pause();
            FindObjectOfType<PauseMenu>().Finished();
        }
        else
        {
            nextlevelpanel.SetActive(true);
            level +=1;
            Invoke(nameof(NextScene),1.0f);
        }
    }
    public void NextScene()
    {
        nextlevelpanel.SetActive(false);
        SceneManager.LoadScene("Level" + level.ToString());
    }

}
