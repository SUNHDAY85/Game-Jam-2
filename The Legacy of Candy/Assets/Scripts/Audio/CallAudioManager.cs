using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallPlaySFX(AudioClip audioClip)
    {
        AudioManager.instance.PlaySFX(audioClip);
    }

    public void CallPlayMusic(AudioClip audioClip)
    {
        AudioManager.instance.PlayMusic(audioClip);
    }
}
