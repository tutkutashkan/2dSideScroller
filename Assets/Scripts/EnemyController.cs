using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State {
        Waiting,
        Roaming,
        ChaseTarget,
        AttackTarget,
        Dead,
        //GoingBackToStart,
    }
    //public Vector3 position1;
    //public Vector3 position2;
    private Vector3 roamPosition;
    private Rigidbody2D rb;
    private Collider2D cl;
    private State state;
    public float speed;
    public Transform target;
    //public float minimumDistance;
    private Animator animator;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask wallMask;
    public bool roaming;
    public bool jumping;
    public bool chasing;
    private float distance;
    private LayerMask mask;
    private float direction = -1f;
    private void Awake() 
    {
        // if the enemy is only roaming it will check distance from walls
        if (roaming){
            state = State.Roaming;
            distance = 1.0f;
            mask = wallMask;
        // if the enemy is chasing the player it starts with the waiting state
        }else{
            state = State.Waiting;
            distance = 10.0f;
            mask = playerMask;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //roamPosition = position1;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cl= GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
        default:

        case State.Roaming:

            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            if (CheckNearby(distance,mask)) 
            {
                // Reached Roam Position so change of direction and sprite
                direction = direction *-1f;
                FlipSprite(distance,mask);
            }
            break;

        case State.Waiting:
            animator.SetBool("Chase",false);
            //float chaseRange = 5.0f;
            if (CheckNearby(distance,mask)) 
            {
                // If enemy sees the player start chasing
                state = State.ChaseTarget;
            }
            break;

        case State.ChaseTarget:
            //Check player side and flip the sprite
            FlipSprite(distance,mask);

            animator.SetBool("Chase",true);

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            float attackRange = 3f;
            if (Vector3.Distance(transform.position, target.position) < attackRange) 
            {
                animator.SetBool("Attack",true);
            }
            else
            {
                animator.SetBool("Attack",false);
            }
            float stopChaseDistance = 10f;
            if (Vector3.Distance(transform.position, target.position) > stopChaseDistance) 
            {
                // Too far, stop chasing
                state = State.Waiting;
            }
            break;

        case State.Dead:
            gameObject.layer = LayerMask.NameToLayer("Non-Interactable"); 
            rb.velocity = Vector3.zero;
            cl.enabled =false;
            animator.SetBool("Dead",true);
            Invoke(nameof(ObjectFalse),1.0f);
            break;
        }
        CheckKilled();
        
    }
    //Flip Sprite
    private void FlipSprite(float distance,LayerMask mask)
    {
        float change = 1f;
        RaycastHit2D raycastright = Physics2D.Raycast(transform.position,Vector2.right, distance, mask);
        RaycastHit2D raycastleft = Physics2D.Raycast(transform.position,Vector2.left, distance, mask);
        if (roaming){
            change *= -1f;
        }
        if (raycastright.collider != null)
        {
            transform.localScale = new Vector3(-1*change, 1f, 1f);
        }
        else if (raycastleft.collider != null)
        {
            transform.localScale = new Vector3(change, 1f, 1f);
        }
    }
    // Check if enemy can see the player
    private bool CheckNearby(float distance, LayerMask mask)
    {
        RaycastHit2D raycastright = Physics2D.Raycast(transform.position,Vector2.right,distance,mask);
        RaycastHit2D raycastleft = Physics2D.Raycast(transform.position,Vector2.left,distance,mask);
        if (roaming){
            if(direction >0){
                return raycastright.collider != null;
            }
            else{
                return raycastleft.collider != null;
            }
        }
        else{
            return (raycastleft.collider != null || raycastright.collider != null);
        }
    }

    // If the player jumps on top of the enemy, enemy gets killed.
    private void CheckKilled()
    {
        RaycastHit2D hit = Physics2D.BoxCast(cl.bounds.center, cl.bounds.size*0.8f, 0f, Vector2.up, 0.3f ,playerMask);
        //Debug.DrawRay(transform.position, Vector2.up, Color.green);
        //Debug.DrawLine(transform.position, transform.position + new Vector3(start,end,0f), Color.green);
        if (hit.collider != null)
        {
            FindObjectOfType<GameManager>().EnemyDestroyed(this);
            state = State.Dead;
        }
    }
    // If enemy is collides with the sword it gets destroyed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            FindObjectOfType<GameManager>().EnemyDestroyed(this);
            state = State.Dead;
        }
    }
    private void ObjectFalse()
    {
        gameObject.SetActive(false);
    }
}
