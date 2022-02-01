using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;
    public static bool isMusicMuted = false;
    public static bool isSoundMuted = false;

    AudioSource audioSource;

    bool isFadeInFinished = false;

    //1つだけMusicPlayerを保持する
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeVolume();
    }

    void ChangeVolume()
    {
        //フェードインが完了したらミュートを可能にする
        if(!isFadeInFinished)
        {
            if(audioSource.volume < 0.1f)
            {
                audioSource.volume += 0.001f;
            }
            else
            {
                audioSource.volume = 0.1f;
                isFadeInFinished = true;
            }
        }
        else
        {
            if(isMusicMuted)
            {
                audioSource.volume = 0;
            }
            else
            {
                audioSource.volume = 0.1f;
            }
        }
    }
}
