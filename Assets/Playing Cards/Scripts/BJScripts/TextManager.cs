using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject turnObject;
    public GameObject playerObject;
    public GameObject CPUObject;
    public GameObject WinnerObject;
    public GameObject ReturnObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text TurnText = turnObject.GetComponent<Text>();
        Text PlayerText = playerObject.GetComponent<Text>();
        Text CPUText = CPUObject.GetComponent<Text>();
        Text WinnerText = WinnerObject.GetComponent<Text>();
        Text ReturnText = ReturnObject.GetComponent<Text>();

        //ターン表示を入れ替える
        if(CardManager.isYourTurn)
        {
            TurnText.text = "▫️YOUR TURN";
        }
        else
        {
            TurnText.text = "▫️CPUs TURN";
        }

        //手札の合計値を入れ替える
        PlayerText.text = "▫️Player：" + CardManager.sumNumber_p;
        CPUText.text = "▫️CPU    ：" + CardManager.sumNumber_n;

        //勝敗結果を入れ替える
        if(CardManager.isGameEnded)
        {
            if(CardManager.winnerCase == 1)
            {
                WinnerText.text = "-YOU WIN-";
            }
            else if(CardManager.winnerCase == 2)
            {
                WinnerText.text = "-CPU WIN-";
            }
            else
            {
                WinnerText.text = "-DRAW-";
            }

            ReturnText.text = "PRESS 'ENTER' TO RETURN TO MENU";
        }
    }
}
