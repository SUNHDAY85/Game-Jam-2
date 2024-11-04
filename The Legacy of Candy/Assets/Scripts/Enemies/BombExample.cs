using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeAction()
    {
        Debug.Log("hay");
        StartCoroutine("DestroyBomb");
    }

    IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
