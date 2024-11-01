using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    public GameObject options;
    void Start()
    {
        
    }
     void Update()
     {

     }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void optionsOn()
    {
        options.SetActive(true);
    }
    public void optionsOff()
    {
        options.SetActive(false);
    }


    public void Credits()
    {
        
    }

    public void Controls ()
    {
       
    }

}
