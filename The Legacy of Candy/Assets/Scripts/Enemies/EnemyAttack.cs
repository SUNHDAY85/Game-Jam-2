using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    public Transform hitTransform;

    public float hitRadius;

    public float hitDelayTime;
    public float timeNextHit;

    // Start is called before the first frame update
    void Start()
    {
        
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
                timeNextHit = hitDelayTime;
                collision.transform.GetComponent<PlayerExample>().TakeDamage();
                Debug.Log("Lo golpeo");
            }
        }
    }
}
