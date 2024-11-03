using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> enemyList;
    // Start is called before the first frame update
    void Start()
    {
        EnemySpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemySpawn()
    {
        GameObject enemySelect = enemyList[SelectRandomEnemy()];
        Instantiate(enemySelect, transform.position, enemySelect.transform.rotation);
    }

    private int SelectRandomEnemy() {
        return Random.Range(0, enemyList.Count);
    }
}
