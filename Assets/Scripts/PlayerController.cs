using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float attackCooldown = 0.1f;
    [SerializeField] private float jumpCooldown = 0.1f;
    [SerializeField] private float jumpMultiplier = 0.1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask jumpLayer;
    [SerializeField] private LayerMask[] layers;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private Sword swordPrefab;
    [SerializeField] private Transform swordSpawn;

    private Rigidbody2D rb;
    public Animator animator;
    private float horizontalInput;
    private bool isJumping = false;
    private bool isAttacking = false;
    private bool attackanim = false;
    private float attackTimer = 0f;
    private float jumpTimer = 0f;
    private bool isGrounded = true;
    private int livesagainstTrap = 3;
    public int level;
    public Transform checkPoint;
    private float direction = 1;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, jumpLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
            jumpTimer = jumpCooldown;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            //if(jumpTimer > 0)
            //{
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                //jumpTimer -= Time.deltaTime;
            //} else
            //{
                //isJumping = false;
            }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            rb.AddForce(Vector2.down * rb.velocity.y *(1 - jumpMultiplier), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            Shoot();
            isAttacking = true;
            attackanim = true;
            attackTimer = attackCooldown;
        }

        // Handle attack cooldown
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
            }
            if (attackTimer <= 0.2f)
            {
                attackanim = false;
            }
        }
        // Handle Jumping
        if (isJumping)
        {   jumpTimer -= Time.deltaTime;
            if(jumpTimer <= 0f)
            {
                isJumping = false;
            }
        }
        // Check if the player is grounded
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, jumpLayer);

        // Handle animations
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("Attack", attackanim);
        // if the player is hit with traps 3 times it dies 
        if(livesagainstTrap == 0){
            toDeath();
        }
    }


    private void FixedUpdate()
    {
        // Read player input 
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // Move the player horizontally
        Vector2 movement = new Vector2(horizontalInput * movementSpeed, rb.velocity.y);
        rb.velocity = movement;

        // Flip the player sprite based on movement direction
        if (horizontalInput > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            direction =1f;
        }
        else if (horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            direction =-1f;
            
        }

        // Handle jumping
        //if (isJumping)
        //{
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //isJumping = false;
        //}

    }
    // Handle throwing sword
    private void Shoot()
    {
        Sword sword = Instantiate(swordPrefab, swordSpawn.position, swordSpawn.rotation);
        sword.Project(direction);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet"){
            livesagainstTrap -= 1;
        }
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Spike" || collision.gameObject.tag == "UnderMap")
        {
            toDeath();
        }
        if(collision.gameObject.tag == "Finished")
        {
            Debug.Log("finish");
            level+=1;
            FindObjectOfType<GameManager>().loadingNextLevel();
        }
    }
    private void toDeath(){
        animator.SetBool("Dead", true);
        gameObject.layer = LayerMask.NameToLayer("Respawn");
        livesagainstTrap = 3;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;

        // set the player object to false in 1s
        Invoke(nameof(ObjectFalse),1.0f);

        FindObjectOfType<GameManager>().Death();
        //FindObjectOfType<AudioManager>().Play("PlayerDeath");
    }
    private void ObjectFalse()
    {
        gameObject.SetActive(false);
    }
}
