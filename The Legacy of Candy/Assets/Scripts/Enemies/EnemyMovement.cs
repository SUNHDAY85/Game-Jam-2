using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Declarations
    public float speedMovemen;

    private bool isJump;
    private bool isMovingLeft;

    private int changeDirection = 1;
    private int action = 0;

    private Rigidbody2D enemyRigidbody2D;
    private SpriteRenderer enemySpriteRender;

    public AudioClip jumpAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        enemySpriteRender = GetComponent<SpriteRenderer>();

        InvokeRepeating("ChangeAction",0,10);
    }

    // Update is called once per frame
    void Update()
    {
        SelectAction();
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

    private void SelectAction()
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
        if (!isJump)
        {
            transform.Translate(Vector3.right * speedMovemen * changeDirection * Time.deltaTime);
        }
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

        enemySpriteRender.flipX = true;
    }
}
