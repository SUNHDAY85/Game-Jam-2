using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Declarations
    public static  int points=0;

    public int index1 = 0;
    public int index2 = 0;
    public int playerIndex = 0;

    [SerializeField] private List<string> levels;

    //Create instance
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {    
            
    }

    public void AssingSkill()
    {
        ChangeScene("SelectPlayer");
    }

    public void SelectScene()
    {
        int indexLevel = Random.Range(0, levels.Count);
        string nameScene = levels[indexLevel];
        ChangeScene(nameScene);
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
