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

    public TextMeshProUGUI skillOneText;
    public TextMeshProUGUI skillTwoText;

    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log(index);
        }
        else
        {
            index = GameManager.instance.index2;
            Debug.Log(index);
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
        }
    }

    private void OnMouseDown()
    {
        GameManager.instance.index1 = indexSkillOne;
        GameManager.instance.index2 = indexSkillTwo;
        GameManager.instance.playerIndex = playerIndex;
        GameManager.instance.SelectScene();
    }
}
