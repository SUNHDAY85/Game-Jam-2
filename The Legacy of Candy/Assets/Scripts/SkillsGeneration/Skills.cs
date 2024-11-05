using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public string nameClass;

    public List<string> skills;
    public string enemyName;

    public int indexSkillOne;
    public int indexSkillTwo;
    public int playerIndex;
    public int life = 3;

    public TextMeshProUGUI skillOneText;
    public TextMeshProUGUI skillTwoText;

    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        AssingSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssingSkill()
    {
        indexSkillOne = SelectSkillsManager();
        indexSkillTwo = SelectSkills();
        playerIndex = SpawnSelectPlayer();
        skillOneText.text = skills[indexSkillOne];
        skillTwoText.text = skills[indexSkillTwo];
        ShowChanges();
    }

    public int SelectSkillsManager()
    {
        int index = Random.Range(0, 2);
        if (index == 0)
        {
            index = GameManager.instance.index1;
        }
        else
        {
            index = GameManager.instance.index2;
        }
        return index;
    }

    public int SelectSkills()
    {
        int index = Random.Range(0, skills.Count);
        return index;
    }

    public int SpawnSelectPlayer()
    {
        int index;
        if (enemyName == "BombGuy")
        {
            index = 0;
        }
        else
        {
            index = 1;
        }
        return index;
    }

    public void ShowChanges()
    {
        switch (indexSkillOne)
        {
            default:
                
                break;
            case 1:
                transform.localScale *= 1.5f;
                break;
            case 2:
                transform.localScale *= 0.6f;
                break;
            case 3:
                
                break;
            case 4:
                
                break;
            case 5:
                life++;
                break;
            case 6:
                life--;
                break;
        }

        switch (indexSkillTwo)
        {
            default:
                
                break;
            case 1:
                transform.localScale *= 1.5f;
                break;
            case 2:
                transform.localScale *= 0.6f;
                break;
            case 3:
                
                break;
            case 4:
                
                break;
            case 5:
                life++;
                break;
            case 6:
                life--;
                break;
        }
    }

    private void OnMouseDown()
    {
        GameManager.instance.index1 = indexSkillOne;
        GameManager.instance.index2 = indexSkillTwo;
        GameManager.instance.playerIndex = playerIndex;
        GameManager.instance.ResetLevel();
        GameManager.instance.SelectScene();
        GameManager.instance.ActivateLifes(life);
    }
}
