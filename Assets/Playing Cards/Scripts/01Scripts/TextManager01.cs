using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager01 : MonoBehaviour
{
    public static int subSavedNumber = 0;

    public GameObject leftObject;
    public GameObject turnObject;
    public GameObject playerObject;
    public GameObject CPUObject;
    public GameObject WinnerObject;
    public GameObject ReturnObject;

    int preNumber, curNumber;
    float time = 0;
    float interval = 2.5f;
    bool isFirstSave = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text LeftText = leftObject.GetComponent<Text>();
        Text TurnText = turnObject.GetComponent<Text>();
        Text PlayerText = playerObject.GetComponent<Text>();
        Text CPUText = CPUObject.GetComponent<Text>();
        Text WinnerText = WinnerObject.GetComponent<Text>();
        Text ReturnText = ReturnObject.GetComponent<Text>();

        //ターン表示を入れ替える
        LeftText.text = "▫️" + CardManager01.numLeftTurn.ToString("D2") + " TURN(S)";

        if(CardManager01.isYourTurn)
        {
            TurnText.text = "▫️YOUR TURN";
        }
        else
        {
            TurnText.text = "▫️CPUs TURN";
        }

        //手札の合計値を入れ替える
        PlayerText.text = "▫️Player：" + (101-CardManager01.sumNumber_p);
        CPUText.text = "▫️CPU    ：" + (101-CardManager01.sumNumber_n);

        //勝敗結果を入れ替える
        if(CardManager01.isGameEnded)
        {
            if(CardManager01.winnerCase == 1)
            {
                WinnerText.text = "- YOU WIN -";
            }
            else if(CardManager01.winnerCase == 2)
            {
                WinnerText.text = "- CPU WIN -";
            }
            else if(CardManager01.winnerCase == 3)
            {
                WinnerText.text = "- YOU WIN[w/"+(101-CardManager01.sumNumber_p)+"] -";
            }
            else if(CardManager01.winnerCase == 4)
            {
                WinnerText.text = "- CPU WIN[w/"+(101-CardManager01.sumNumber_n)+"] -";
            }
            else
            {
                WinnerText.text = "- DRAW -";
            }

            ReturnText.text = "PRESS 'ENTER' TO RETURN TO MENU";
        }

        //バストした際に表示する
        if(CardManager01.bustCase != 0)
        {
            if(CardManager01.bustCase == 1)
            {
                if(isFirstSave)
                {
                    preNumber = 101 - (CardManager01.sumNumber_p+subSavedNumber);
                    curNumber = 101 - CardManager01.sumNumber_p;
                    isFirstSave = false;
                }
                WinnerText.text = "- YOU BUST["+preNumber+"→"+curNumber+"] -";
            }
            if(CardManager01.bustCase == 2)
            {
                if(isFirstSave)
                {
                    preNumber = 101 - (CardManager01.sumNumber_n+subSavedNumber);
                    curNumber = 101 - CardManager01.sumNumber_n;
                    isFirstSave = false;
                }
                WinnerText.text = "- CPU BUST["+preNumber+"→"+curNumber+"] -";
            }

            //一定時間後に非表示にする
            time += Time.deltaTime;
            if(time > interval || CardManager01.isGameEnded)
            {
                WinnerText.text = "";

                time = 0;
                subSavedNumber = 0;
                isFirstSave = true;
                CardManager01.bustCase = 0;
            }
        }
    }
}
