using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager01 : MonoBehaviour
{
    public static int numLeftTurn = 15;
    public static int numCard = 0;
    public static int numDeletedCard = 0;
    public static int numCardMaterial = 0;
    public static int numItemMaterial = 0;
    public static int sumNumber_p = 0;
    public static int sumNumber_n = 0;
    public static int subNumber = 0;
    public static int winnerCase = 0;
    public static int bustCase = 0;
    public static int numSelectedItems = 0;
    public static float start = 0;
    
    public static bool isItemSelected = false;
    public static bool isItemFinished = false;
    public static bool isCardsOpened = true;
    public static bool isYourTurn = true;
    public static bool isFirstHit = true;
    public static bool isAbleToHit = true;
    public static bool isAbleToUse = false;
    public static bool isGameStarted = false;
    public static bool isGameEnded = false;

    int addNumber = 0;
    bool isNumberAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalcurateSumNumber();

        //15ターンが終了したら勝敗判定を行う
        if(numLeftTurn == 0)
        {
            CompareSumNumbers();
        }
    }

    void CalcurateSumNumber()
    {
        //HITしたらアイテムを使用可能にする
        if(HitManager01.isHitPushed)
        {
            isAbleToHit = false;
            isAbleToUse = true;
        }

        //スペースが押されるとアイテムのフェーズが終わる
        if(!HitManager01.isHitDown && isAbleToUse && isYourTurn && Input.GetKeyDown(KeyCode.Space))
        {
            isItemFinished = true;
        }

        //アイテムのフェーズ終了後に手札の計算をする
        if(isItemFinished)
        {
            isAbleToUse = false;
            isNumberAdded = true;
        }

        if(!HitManager01.isHitDown && isCardsOpened && isNumberAdded)
        {
            GameObject targetCard = SetCards01.CardList[numCard-1];
            addNumber = targetCard.GetComponent<CardMotion01>().SaveCardNumber();
            subNumber += addNumber;

            //手札の合計値に加算する
            if(isYourTurn)
            {
                sumNumber_p += addNumber;
            }
            else
            {
                sumNumber_n += addNumber;
            }
            
            //101ジャストの場合はゲームが終了する
            if(sumNumber_p == 101)
            {
                winnerCase = 1;
                isGameEnded = true;
            }
            if(sumNumber_n == 101)
            {
                winnerCase = 2;
                isGameEnded = true;
            }

            //各種状態を初期化する
            isItemFinished = false;
            isNumberAdded = false;
            isAbleToHit = true;

            if(CPUManager01.isCPUsItemUsed)
            {
                CPUManager01.isCPUsItemUsed = false;
            }

            //2回めのHITなら101から合計値を引く/ターンを終了する
            if(isFirstHit)
            {
                isFirstHit = false;
            }
            else
            {
                if(sumNumber_p > 101)
                {
                    sumNumber_p -= subNumber;
                    bustCase = 1;
                }
                if(sumNumber_n > 101)
                {
                    sumNumber_n -= subNumber;
                    bustCase = 2;
                }
                TextManager01.subSavedNumber = subNumber;
                subNumber = 0;

                if(isYourTurn || sumNumber_n < 101)
                {
                    isYourTurn = !isYourTurn;
                    isFirstHit = true;
                }

                if(isYourTurn && numLeftTurn > 0)
                {
                    numLeftTurn--;
                }
            }
        }
    }

    void CompareSumNumbers()
    {
        if(sumNumber_p > sumNumber_n)
        {
            winnerCase = 3;
        }
        else if(sumNumber_p < sumNumber_n)
        {
            winnerCase = 4;
        }
        else
        {
            winnerCase = 0;
        }

        isGameEnded = true;
    }
}