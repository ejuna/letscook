using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{

    public AudioMixer audioMixer;
    public Slider BgmSlider;
    public Slider EffectSlider;


    //BGM 설정
    public void SetBgmVolum()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(BgmSlider.value) * 20);
    }

    //Effect 설정
    public void SetEffectVolum()
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(EffectSlider.value) * 20);
    }
}
