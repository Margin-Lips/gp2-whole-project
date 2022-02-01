using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandManager : MonoBehaviour
{
    public static bool isStandPushed = false;
    public static bool isStandDown = false;

    float time;
    float interval = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PushButtonUpDown();
    }

    void PushButtonUpDown()
    {
        //STANDボタンが押されたら沈む
        if(isStandPushed && isStandDown && CardManager.isYourTurn)
        {
            transform.localPosition = new Vector3(0.0f, 0.035f, 0.0f);
        }
        
        if(!isStandPushed && isStandDown)
        {
            TimeCount();
        }

        //一定時間後にボタンの沈みが戻る
        if(!isStandPushed && !isStandDown)
        {
            transform.localPosition = new Vector3(0.0f, 0.06f, 0.0f);
        }
    }

    void TimeCount()
    {
        time += Time.deltaTime;
        if(time > interval)
        {
            isStandDown = false;
            time = 0;
        }
    }

    public void onClickStand()
    {
        if(!isStandPushed && !isStandDown && CardManager.isYourTurn) 
        {
            if(CardManager.isCardsOpened && !CardManager.isPlayerStood && !CardManager.isGameEnded)
            {
                isStandPushed = true;
                isStandDown = true;
                CardManager.isCalcFinished = false;

                //ミュートされていなかったらSEを流す
                if(!SoundsManager.isSoundMuted)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
