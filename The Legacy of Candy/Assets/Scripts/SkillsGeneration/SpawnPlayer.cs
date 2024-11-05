using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public List<GameObject> players;
    public int indexplayer;
    // Start is called before the first frame update
    void Start()
    {
        indexplayer = GameManager.instance.playerIndex;
        Instantiate(players[indexplayer], transform.position, players[indexplayer].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
