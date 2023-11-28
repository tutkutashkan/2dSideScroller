using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapcontrol : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().MapCollected(this);
            animator.SetBool("Collected",true);
            Invoke(nameof(ObjectFalse),1.0f);
        }
    }
    private void ObjectFalse()
    {
        gameObject.SetActive(false);
    }
}
