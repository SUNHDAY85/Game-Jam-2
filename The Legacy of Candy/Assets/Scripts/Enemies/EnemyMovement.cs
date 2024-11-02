using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Declarations
    public float speedMovemen;

    private bool isJump;
    private bool isMovingLeft;
    public bool isLookingLeft;
    private bool isFollowingPlayer;

    private int changeDirection = 1;
    private int action = 0;

    public Rigidbody2D enemyRigidbody2D;
    public SpriteRenderer enemySpriteRender;

    public AudioClip jumpAudioClip;

    //For the hit player

    public Transform hitTransform;

    public float hitRadius;

    public float hitDelayTime;
    public float timeNextHit;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        enemySpriteRender = GetComponent<SpriteRenderer>();

        StartFlipSpriteX();
        InvokeRepeating("ChangeAction",0,10);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeNextHit > 0)
        {
            timeNextHit -= Time.deltaTime;
        }

        PerformAction();
        HitPlayer();
    }

    private void StartFlipSpriteX()
    {
        if (isLookingLeft)
        {
            changeDirection *= -1;
        }
    }

    private void ChangeAction()
    {
        if( Random.Range(0, 100) < 80)
        {
            action = 1;
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
        }
    }

    private void StayStill()
    {

    }

    private void Movement()
    {
        if (!isJump && !isFollowingPlayer)
        {
            transform.Translate(Vector3.right * speedMovemen * changeDirection * Time.deltaTime);
        }
    }

    private void HitPlayer()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(hitTransform.position, hitRadius);

        foreach (Collider2D collision in objects)
        {
            if (collision.CompareTag("Player") && timeNextHit <= 0)
            {
                timeNextHit = hitDelayTime;
                collision.transform.GetComponent<PlayerExample>().TakeDamage();
                Debug.Log("Lo golpeo");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitTransform.position, hitRadius);
    }

    private void Jump()
    {
        isJump = true;
        AudioManager.instance.PlaySFX(jumpAudioClip);

        enemyRigidbody2D.AddForce(new Vector2(5 * changeDirection , 5), ForceMode2D.Impulse);
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
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
            Jump();
        }
    }

    private void ChangeDirection()
    {
        changeDirection *= -1;

        enemySpriteRender.flipX = !enemySpriteRender.flipX;
    }
}
