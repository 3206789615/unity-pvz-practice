using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartgameController : MonoBehaviour
{
    public void onstartbuttonclick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //AudioManager.Instance.PlayBGM("bgm4");
    }
}
