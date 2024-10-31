using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Declarations
    public float speedMovemen;

    [SerializeField] private bool isJump;
    [SerializeField] private bool isMovingLeft;

    [SerializeField] private int changeDirection = 1;

    private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (!isJump)
        {
            transform.Translate(Vector3.right * speedMovemen * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Finish"))
        {
            isJump = false;
        }
        else if (collision.collider.CompareTag("EditorOnly"))
        {
            isMovingLeft = !isMovingLeft;
            speedMovemen *= -changeDirection;
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
            isMovingLeft = !isMovingLeft;
            speedMovemen *= -changeDirection;
        }

        else
        {
            isJump = true;
            if (!isMovingLeft)
            {
                rigidbody2D.AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(5 * -changeDirection, 5), ForceMode2D.Impulse);
            }
        }
    }
}
