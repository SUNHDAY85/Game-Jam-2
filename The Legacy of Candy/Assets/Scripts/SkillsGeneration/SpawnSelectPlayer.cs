using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSelectPlayer : MonoBehaviour
{
    public List<GameObject> spawnSelectPlayers;
    public List<GameObject> players;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayers()
    {
        int indexSpawn = 0;
        foreach (GameObject spawn in spawnSelectPlayers)
        {
            int indexPlayers = Random.Range(0, players.Count);
            Instantiate(players[indexPlayers], spawnSelectPlayers[indexSpawn].transform.position, players[indexPlayers].transform.rotation);
            indexSpawn++;
        }     
    }
}
