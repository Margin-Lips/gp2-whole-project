using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //BJゲームに切り替える
    public void OnClickBJGameButton()
    {
        SceneChanger.isIfMenuScene = false;
        SceneManager.LoadScene("BJ_Scene");
    }

    //01ゲームに切り替える
    public void OnClick01GameButton()
    {
        SceneChanger.isIfMenuScene = false;
        SceneManager.LoadScene("01_Scene");
    }

    //説明画面に切り替える
    public void OnClickInstructButton()
    {
        MenuManager.isInstDisplayed = true;
    }

    //BGMミュートを切り替える
    public void OnClickMusicButton()
    {
        SoundsManager.isMusicMuted = !SoundsManager.isMusicMuted;
    }

    //SEミュートを切り替える
    public void OnClickSoundButton()
    {
        SoundsManager.isSoundMuted = !SoundsManager.isSoundMuted;
    }
}
