using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static int numCard = 0;
    public static int numCardMaterial = 0;
    public static int numItemMaterial = 0;
    public static int sumNumber_p = 0;
    public static int sumNumber_n = 0;
    public static int winnerCase = 0;
    
    public static bool isPlayerStood = false;
    public static bool isCPUStood = false;
    public static bool isItemSelected = false;
    public static bool isItemFinished = false;
    public static bool isCardsOpened = true;
    public static bool isYourTurn = true;
    public static bool isOneMoreHit = false;
    public static bool isOneLastHit = false;
    public static bool isAbleToHit = true;
    public static bool isAbleToUse = false;
    public static bool isCalcFinished = false;
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

        //両者がSTANDしたら勝敗判定を行う
        if(isPlayerStood && isCPUStood && isCalcFinished)
        {
            CompareSumNumbers();
        }
    }

    void CalcurateSumNumber()
    {
        //HITしたらアイテムを使用可能にする
        if(HitManager.isHitPushed)
        {
            isAbleToHit = false;
            isAbleToUse = true;
        }

        //スペースが押されるとアイテムのフェーズが終わる
        if(!HitManager.isHitDown && !StandManager.isStandDown && isAbleToUse)
        {
            if(isYourTurn && Input.GetKeyDown(KeyCode.Space))
            {
                isItemFinished = true;
            }
        }

        //アイテムのフェーズ終了後に手札の計算をする
        if(isItemFinished)
        {
            isAbleToUse = false;
            isNumberAdded = true;
        }

        if(!HitManager.isHitDown && isCardsOpened && isNumberAdded)
        {
            GameObject targetCard = SetCards.CardList[numCard-1];
            addNumber = targetCard.GetComponent<CardMotion>().SaveCardNumber();

            //手札の合計値に加算する
            if(isYourTurn)
            {
                sumNumber_p += addNumber;
            }
            else
            {
                sumNumber_n += addNumber;
            }
            
            //BUSTした場合はゲームが終了する
            if(sumNumber_p > 21)
            {
                winnerCase = 2;
                isGameEnded = true;
            }
            if(sumNumber_n > 21)
            {
                winnerCase = 1;
                isGameEnded = true;
            }

            //各種状態を初期化する
            isItemFinished = false;
            isNumberAdded = false;
            isAbleToHit = true;

            if(isOneMoreHit)
            {
                isOneMoreHit = false;
            }
            
            if(isOneLastHit)
            {
                isOneLastHit = false;
            }

            if(!CPUManager.isOnceSkipped)
            {
                CPUManager.isOnceSkipped = true;
            }

            if(!isYourTurn && CPUManager.isChanceLeft)
            {
                CPUManager.isChanceLeft = false;
            }

            if(isYourTurn && !isCalcFinished)
            {
                isCalcFinished = true;
            }

            //計算終了後にターンを切り替える
            if(isYourTurn || sumNumber_n < 17 || (isCPUStood && !isPlayerStood))
            {
                if(isPlayerStood)
                {
                    isYourTurn = false;
                }
                else
                {
                    isYourTurn = !isYourTurn;
                }
            }
        }
    }

    void CompareSumNumbers()
    {
        //両者がBUSTしていない場合のみ
        if(sumNumber_n < 22 && sumNumber_p < 22)
        {
            if(sumNumber_p > sumNumber_n)
            {
                winnerCase = 1;
            }
            else if(sumNumber_p < sumNumber_n)
            {
                winnerCase = 2;
            }
            else
            {
                winnerCase = 0;
            }
        }

        isGameEnded = true;
    }
}
