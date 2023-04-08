using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audio; //sesuaikan dengan nama audio mixer yang dibuat
    
    public void volume(float vol){
        audio.SetFloat("volume", vol); //"volume" nama parameter audio mixer
    }

    public void Display(bool fScreen){
        Screen.fullscreen = fScreen; //perlu toggle fuulscreen
    }

    public void Back(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
