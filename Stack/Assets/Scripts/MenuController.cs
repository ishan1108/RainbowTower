using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    private const string APPURL = "https://play.google.com/store/apps/details?id=com.tangential.RainbowTower&rdid=com.tangential.RainbowTower";
    public Image button;
    public Sprite musicOn;
    public Sprite musicOff;

    void Start()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            button.sprite = musicOff;
        }
        else
        {
            button.sprite = musicOn;
        }
    }

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
        if(PlayerPrefs.GetInt("Music") == 1)
        {
            button.sprite = musicOff;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            button.sprite = musicOn;
            PlayerPrefs.SetInt("Music", 1);
        }
        AudioListener.volume = (PlayerPrefs.GetInt("Music") == 0) ? 0 : 1;
    }

    public void rateMyApp()
    {
        Application.OpenURL(APPURL);
    }

    public void shareMyApp()
    {
#if UNITY_ANDROID
        //Refernece of AndroidJavaClass class for intent
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        //Refernece of AndroidJavaObject class for intent
        AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
        //call setAction method of the Intent object created
        intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
        //set the type of sharing that is happening
        intentObject.Call<AndroidJavaObject>("setType", "text/plain");
        //add data to be passed to the other activity i.e., the data to be sent
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"),
                                            "Play and build your tower. can you beat my high score " + 
                                            PlayerPrefs.GetInt("HighScore") + "? Try yourself and challenge others");
        intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), APPURL);
        //get the current activity
        AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        //start the activity by sending the intent data
        currentActivity.Call("startActivity", intentObject);
#endif
    }

    public void adRemover()
    {
        RemoveAds.Instance.BuyNonConsumable();
    }
}
