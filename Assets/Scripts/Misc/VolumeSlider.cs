using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    public AudioMixer mixer;


    private void Start()
    {
        mixer = Resources.Load("MainMixer") as AudioMixer;
        if (index == 0)
        {
            float value;
            bool result = mixer.GetFloat("musicVolume", out value);
            GetComponent<Slider>().value = value;
        }
        else
        {
            float value;
            bool result = mixer.GetFloat("fxVolume", out value);
            GetComponent<Slider>().value = value;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (index == 0)
        {
            mixer.SetFloat("musicVolume", GetComponent<Slider>().value);
        }
        if (index == 1)
        {
            mixer.SetFloat("fxVolume", GetComponent<Slider>().value);
        }

    }
}
