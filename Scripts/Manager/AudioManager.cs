using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource sfxSource;
    private AudioSource bgmSource; // 背景音乐
    private Dictionary<string, AudioClip> audioDict = new Dictionary<string, AudioClip>();
    void Awake()
    {
        // 单例（防重复）
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 获取AudioSource
        AudioSource[] sources = GetComponents<AudioSource>();
        sfxSource = sources[0];
        bgmSource = sources[1];

        // ⭐ 一次性加载所有音效
        LoadAllAudio();
    }

    void LoadAllAudio()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");

        foreach (var clip in clips)
        {
            audioDict[clip.name] = clip;
        }
    }

    // 播放音效
    public void Play(string name)
    {
        if (audioDict.ContainsKey(name))
        {
           // Debug.Log("播放音效: " + name);
            sfxSource.PlayOneShot(audioDict[name]);
        }
        else
        {
            Debug.LogWarning("找不到音效: " + name);
        }
    }

    public void PlayBGM(string name)
    {
        if (audioDict.ContainsKey(name))
        {
           // Debug.Log("播放音效: " + name);
            bgmSource.clip = audioDict[name];
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("找不到BGM: " + name);
        }
    }

    public void PlayRandom(string baseName, int count)
    {
        int index = Random.Range(1, count + 1);
        string finalName = baseName + index;

        Play(finalName);
    }
    // ⛔ 停止BGM
    public void StopBGM(string name)
    {
        bgmSource.Stop();
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PauseBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
    }

    public void ResumeBGM()
    {
        bgmSource.UnPause();
    }
}
