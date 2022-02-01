using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandManager01 : MonoBehaviour
{
    bool isStandPushed = false;
    bool isStandDown = false;

    float time;
    float interval = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PushButtonUpDown();
        //Debug.Log(isStandPushed);
    }

    void PushButtonUpDown()
    {
        //STANDボタンが押されたら沈む
        if(isStandPushed && isStandDown && CardManager01.isYourTurn)
        {
            transform.localPosition = new Vector3(0.0f, 0.035f, 0.0f);
            isStandPushed = false;
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
        if(!isStandPushed && !isStandDown && CardManager01.isYourTurn) 
        {
            if(CardManager01.isCardsOpened && !CardManager01.isGameEnded)
            {
                isStandPushed = true;
                isStandDown = true;

                //ミュートされていなかったらSEを流す
                if(!SoundsManager.isSoundMuted)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}