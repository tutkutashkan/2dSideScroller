using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private enum State {
        Waiting,
        ShootingTarget,
        Dead,
    }
    public GameObject totemBullet;
    private float nextShotTime =0f;
    public float timeBetweenShots;
    public float speed;
    public Transform target;
    private State state;
    private Animator animator;
    public Transform bulletSpawn;
    // Start is called before the first frame update

    private void Awake()
    {
        state = State.Waiting;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
        default:

        case State.Waiting:
            animator.SetBool("Shooting",false);
            float attackRange = 10.0f;
            if (Vector3.Distance(transform.position, target.position) < attackRange) {
                // attack
                state = State.ShootingTarget;
            }
            break;
        
        case State.ShootingTarget:
            animator.SetBool("Shooting",true);
            if(Time.time > nextShotTime){
                Instantiate(totemBullet, bulletSpawn.position , bulletSpawn.rotation);
                //FindObjectOfType<AudioManager>().Play("Shooting");
                nextShotTime = Time.time + timeBetweenShots;
            }
            if (Vector3.Distance(transform.position, target.position) > 5.0f) {
                state = State.Waiting;
            }
            break;
        
        case State.Dead:
        gameObject.layer = LayerMask.NameToLayer("Non-Interactable"); 
            animator.SetBool("Dead",true);
            Invoke(nameof(ObjectFalse),1.0f);
            break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            FindObjectOfType<GameManager>().TrapDestroyed(this);
            state = State.Dead;
        }
    }
    private void ObjectFalse()
    {
        gameObject.SetActive(false);
    }
}
