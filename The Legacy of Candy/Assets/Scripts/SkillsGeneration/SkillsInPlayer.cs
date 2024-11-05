using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInPlayer : MonoBehaviour
{
    public string nameClass;

    public List<string> skills;

    public int indexSkillOne;
    public int indexSkillTwo;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        indexSkillOne = GameManager.instance.index1;
        indexSkillTwo = GameManager.instance.index2;
        playerControllerScript = gameObject.GetComponent<PlayerController>();
        ShowChanges();
    }

    // Update is called once per frame
    void Update()
    {

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
                playerControllerScript.velocidadDeMovimiento += 100;
                break;
            case 4:
                playerControllerScript.velocidadDeMovimiento -= 50;
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
                playerControllerScript.velocidadDeMovimiento += 100;
                break;
            case 4:
                playerControllerScript.velocidadDeMovimiento -= 50;
                break;
        }
    }
}
