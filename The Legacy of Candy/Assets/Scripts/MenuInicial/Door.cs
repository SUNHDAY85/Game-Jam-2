using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Declarations
    public Animator animatorDoor;
    public bool isOpen;

    public AudioClip doorCloseAudioClip;
    public AudioClip doorOpenAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        animatorDoor = gameObject.GetComponent<Animator>();
        AudioManager.instance.PlaySFX(doorCloseAudioClip);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.points >= 5)
        {
            animatorDoor.SetBool("isOpen", true);
            isOpen = true;
            AudioManager.instance.PlaySFX(doorOpenAudioClip);
        }
        else
        {
            
        }
    }
}
