using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Manager : MonoBehaviour
{
    public AudioSource Audio;
    public Slider slider;

    public void Audio_DaXiao(float daXiao) {
        Audio.volume = daXiao;
    }
    void Start()
    {
        
    }

    void Update()
    {
        Audio_DaXiao(slider.value);
    }
}
