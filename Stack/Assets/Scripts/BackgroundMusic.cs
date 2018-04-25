using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    private AudioSource music;
    private GameObject obj;
	// Use this for initialization
	void Start () {
        music = gameObject.GetComponent<AudioSource>();
        obj = this.gameObject;
        DontDestroyOnLoad(obj);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        PlayerPrefs.SetInt("Music", 1);
	}

    void Update()
    {
        if (PlayerPrefs.GetInt("Music") == 1 && !music.isPlaying)
        {
            music.Play();
        }
    }

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

}
