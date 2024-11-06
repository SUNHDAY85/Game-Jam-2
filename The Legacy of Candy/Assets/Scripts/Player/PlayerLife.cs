using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.ActivateLifes(life);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        life --;
        GameManager.instance.ChangeLifeIU(life);
        if (life == 0)
        {
            GameManager.instance.ActiveNextGenerationButton();
            Destroy(gameObject);
        }
    }
}
