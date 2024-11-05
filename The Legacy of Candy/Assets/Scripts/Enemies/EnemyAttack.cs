using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform hitTransform;

    public float hitRadius;

    public float hitDelayTime;
    public float timeNextHit;

    public float forceHit;
    public float forceAction;

    private EnemyMovement enemyMovementScript;

    public AudioClip attackAudioClip;

    //For Animations
    [SerializeField]private float timeToAttack;
    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyMovementScript = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeNextHit > 0)
        {
            timeNextHit -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitTransform.position, hitRadius);
    }

    public void HitPlayer()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(hitTransform.position, hitRadius);

        foreach (Collider2D collision in objects)
        {
            if (collision.CompareTag("Player") && timeNextHit <= 0)
            {
                enemyAnimator.SetBool("isWalking", false);
                enemyAnimator.SetBool("isStill", false);
                enemyAnimator.SetBool("isJumping", false);
                enemyAnimator.SetBool("isAttacking", true);

                StartCoroutine("NotHit");

                timeNextHit = hitDelayTime;

                

                collision.transform.GetComponent<PlayerLife>().TakeDamage();

                Vector2 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
                collision.transform.GetComponent<Rigidbody2D>().AddForce(awayFromPlayer * forceHit, ForceMode2D.Impulse);
            }
        }
    }

    public void BombAction()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(hitTransform.position, hitRadius);

        foreach (Collider2D collision in objects)
        {
            if (collision.CompareTag("Bombs") && timeNextHit <= 0)
            {
                enemyAnimator.SetBool("isWalking", false);
                enemyAnimator.SetBool("isStill", false);
                enemyAnimator.SetBool("isJumping", false);
                enemyAnimator.SetBool("isAttacking", true);

                StartCoroutine("NotHit");

                timeNextHit = hitDelayTime;

                Vector2 awayFromBomb = (collision.gameObject.transform.position - transform.position);
                collision.transform.GetComponent<Rigidbody2D>().AddForce(awayFromBomb * forceAction, ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator NotHit()
    {
        yield return new WaitForSeconds(timeToAttack);
        AudioManager.instance.PlaySFX(attackAudioClip);
        yield return new WaitForSeconds(0.6f);
        enemyMovementScript.isSeePlayer = false;
        enemyMovementScript.isSeeBomb = false;
        enemyMovementScript.action = 1;

        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isWalking", true);
    }
}
