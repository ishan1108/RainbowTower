using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private const string APPURL = "get url here";

    public void onRetryClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void onMenuClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void MusicSwitch()
    {

    }

    public void rateMyApp()
    {
        Application.OpenURL(APPURL);
    }
}
