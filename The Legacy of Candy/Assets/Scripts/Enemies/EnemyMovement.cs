using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Declarations
    public float speedMovemen;

    [SerializeField] private bool isJump;
    [SerializeField] private bool isMovingLeft;
    [SerializeField] public bool isLookingLeft;
    [SerializeField]private bool isFollowingPlayer;
    public bool isSeePlayer;
    public bool isSeeBomb;

    public int action = 0;
    [SerializeField] private int changeDirection = 1;

    public Rigidbody2D enemyRigidbody2D;
    public SpriteRenderer enemySpriteRender;

    public AudioClip jumpAudioClip;

    //For the hit player
    public Transform seeTransform;

    public float seeRadius;

    private EnemyAttack enemyAttackScript;

    //For Animations
    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        enemySpriteRender = GetComponent<SpriteRenderer>();
        enemyAttackScript = GetComponent<EnemyAttack>();
        enemyAnimator = GetComponent<Animator>();

        isMovingLeft = isLookingLeft;

        StartFlipSpriteX();
        InvokeRepeating("ChangeAction",0,10);
    }

    // Update is called once per frame
    void Update()
    {
        PerformAction();
        SeePlayer();
    }

    private void StartFlipSpriteX()
    {
        if (isLookingLeft)
        {

            enemySpriteRender.flipX = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);

            changeDirection *= -1;
        }
    }

    private void SeePlayer()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(seeTransform.position, seeRadius);

        foreach (Collider2D collision in objects)
        {
            if (collision.CompareTag("Player"))
            {
                isSeePlayer = true;
                enemyAttackScript.HitPlayer();
            }
            if (collision.CompareTag("Bombs"))
            {
                isSeeBomb = true;
                enemyAttackScript.BombAction();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(seeTransform.position, seeRadius);
    }

    private void ChangeAction()
    {
        isSeePlayer = false;
        isSeeBomb = false;

        int probability = Random.Range(0, 100);
        if(probability < 80)
        {
            action = 1;
        }
        else if (probability < 90)
        {
            action = 2;
        }
        else
        {
            action = 0;
        }
    }

    private void PerformAction()
    {
        

        switch (action)
        {
            default:
                StayStill();
                break;
            case 1:
                Movement();
                break;
            case 2:
                JumpUp();
                break;
        }
    }

    private void StayStill()
    {
        if (!isJump && !isFollowingPlayer && !isSeePlayer && !isSeeBomb)
        {
            enemyAnimator.SetBool("isWalking", false);
            enemyAnimator.SetBool("isStill", true);
        }   
    }

    private void Movement()
    {
        if (!isJump && !isFollowingPlayer && !isSeePlayer && !isSeeBomb)
        {
            enemyAnimator.SetBool("isStill", false);
            enemyAnimator.SetBool("isWalking", true);
            transform.Translate(Vector3.right * speedMovemen * Time.deltaTime);
        }
    }

    private void JumpPrecipice()
    {
        enemyAnimator.SetBool("isWalking", false);
        enemyAnimator.SetBool("isStill", false);
        enemyAnimator.SetBool("isJumping", true);

        isJump = true;
        AudioManager.instance.PlaySFX(jumpAudioClip);

        enemyRigidbody2D.AddForce(new Vector2(5 * changeDirection, 5), ForceMode2D.Impulse);
    }

    private void JumpUp()
    {
        
        if (!isJump)
        {
            enemyAnimator.SetBool("isWalking", false);
            enemyAnimator.SetBool("isStill", false);
            enemyAnimator.SetBool("isJumping", true);

            isJump = true;

            AudioManager.instance.PlaySFX(jumpAudioClip);

            enemyRigidbody2D.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            enemyAnimator.SetBool("isJumping", false);

            isJump = false;
        }
        else if (collision.collider.CompareTag("Walls"))
        {
            ChangeDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn") && !isJump)
        {
            Decision();
        }
        else if (collision.CompareTag("Walls"))
        {
            ChangeDirection();
        }

    }

    private void Decision()
    {
        int decision = Random.Range(0,2);

        if (decision == 0)
        {
            ChangeDirection();
        }

        else
        {
            JumpPrecipice();
        }
    }

    private void ChangeDirection()
    {
        changeDirection *= -1;

        if (isMovingLeft)
        {
            isMovingLeft = !isMovingLeft;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            isMovingLeft = !isMovingLeft;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
