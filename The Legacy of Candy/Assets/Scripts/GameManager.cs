using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Declarations
    public int points = 0;
    public int level = 0;

    public int index1 = 0;
    public int index2 = 0;
    public int playerIndex = 0;

    private bool isPaused;

    [SerializeField] private List<string> levels;
    [SerializeField] private List<GameObject> lifes;

    public GameObject nextGenerationButton;

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
        isPaused = false;
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
        level++;
        if (level < 10)
        {
            int indexLevel = Random.Range(0, levels.Count);
            string nameScene = levels[indexLevel];
            ChangeScene(nameScene);
        }
        else
        {
            ChangeScene("BossScene");
        }
    }

    public void ChangeScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
    public void ResetLevel()
    {
        level = 0;
    }

    //PauseGame
    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        isPaused = !isPaused;
    }

    public void ChangeLifeIU(int indexLife)
    {
        lifes[indexLife].SetActive(false);
    }
    public void ActiveNextGenerationButton()
    {
        nextGenerationButton.SetActive(true);
    }

    public void ActivateLifes(int indexLife)
    {
        int index = 0;
        while (index < 5)
        {
            lifes[index].SetActive(false);
            index++;
        }

        index = 0;
        while (index < indexLife)
        {
            lifes[index].SetActive(true);
            index++;
        }    
    }
}
