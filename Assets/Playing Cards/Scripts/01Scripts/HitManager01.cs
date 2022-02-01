using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager01 : MonoBehaviour
{
    public static bool isHitPushed = false;
    public static bool isHitDown = false;

    int interval = 1;
    float time;

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
        //HITボタンが押されたら沈む
        if(isHitPushed && isHitDown && CardManager01.isYourTurn)
        {
            transform.localPosition = new Vector3(0.0f, 0.035f, 0.0f);
        }
        
        if(!isHitPushed && isHitDown)
        {
            TimeCount();
        }

        //一定時間後にボタンの沈みが戻る
        if(!isHitPushed && !isHitDown)
        {
            transform.localPosition = new Vector3(0.0f, 0.06f, 0.0f);
        }
    }

    void TimeCount()
    {
        time += Time.deltaTime;
        if(time > interval)
        {
            isHitDown = false;
            time = 0;
        }
    }

    public void onClickHit()
    {
        if(!isHitPushed && !isHitDown && CardManager01.isYourTurn && CardManager01.isAbleToHit) 
        {
            if(CardManager01.isCardsOpened && !CardManager01.isGameEnded)
            {
                isHitPushed = true;
                isHitDown = true;

                //ミュートされていなかったらSEを流す
                if(!SoundsManager.isSoundMuted)
                {
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
