using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayBGM("bgm4");
    }   
    public void onstartbuttonclick()
    {
        AudioManager.Instance.Play("buttonclick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

    public void exitButton()
    {
        AudioManager.Instance.Play("buttonclick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
