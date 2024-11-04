using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life;

    private Animator enemyAnimator;

    private EnemyMovement enemyMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyMovementScript = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        Debug.Log("uy");
        life --;
        enemyAnimator.SetBool("isWalking", false);
        enemyAnimator.SetBool("isStill", false);
        enemyAnimator.SetBool("isJumping", false);
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isHit", true);
        StartCoroutine("DelaytHit");
        
    }

    IEnumerator DelaytHit()
    {
        yield return new WaitForSeconds(1f);
        if (life <= 0)
        {
            Debug.Log("oe");
            Destroy(gameObject);
        }
        enemyAnimator.SetBool("isStill", false);
        enemyAnimator.SetBool("isJumping", false);
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isHit", false);
        enemyMovementScript.action = 1;

        enemyAnimator.SetBool("isWalking", true);
    }
}
