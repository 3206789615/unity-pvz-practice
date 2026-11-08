using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuUI : MonoBehaviour
{
        
    // 返回主菜单
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameMenu"); // 改成你的菜单场景名
    }

    // 音乐音量（Slider调用）
    public void OnBGMVolumeChanged(float value)
    {
        Debug.Log("BGM音量：" + value);
        AudioManager.Instance.SetBGMVolume(value);
    }

    // 音效音量（Slider调用）
    public void OnSFXVolumeChanged(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
}
