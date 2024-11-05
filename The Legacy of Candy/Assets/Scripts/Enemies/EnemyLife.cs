using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private int life;

     [SerializeField] private GameObject[] candy; // Lista de dulces 

    private Animator enemyAnimator;

    private EnemyMovement enemyMovementScript;
    private BossCucumber enemyBossMovementScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        if (gameObject.name == "BossCucumber")
        {
            enemyBossMovementScript = GetComponent<BossCucumber>();
        }
        else
        {
            enemyMovementScript = GetComponent<EnemyMovement>();
        }
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
            int randomIndex = Random.Range(0, 3); // Random entre 0 y 2
            Instantiate(candy[randomIndex], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        enemyAnimator.SetBool("isStill", false);
        enemyAnimator.SetBool("isJumping", false);
        enemyAnimator.SetBool("isAttacking", false);
        enemyAnimator.SetBool("isHit", false);
        if (gameObject.name == "BossCucumber")
        {
            enemyBossMovementScript.action = 1;
        }
        else
        {
            enemyMovementScript.action = 1;
        }
        enemyAnimator.SetBool("isWalking", true);
    }
}
