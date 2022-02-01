using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUManager : MonoBehaviour
{
    public static int itemNumber_CPU = 0;
    public static bool isFirstOut = true;
    public static bool isChanceLeft = false;
    public static bool isOnceSkipped = false;
    
    int limitValue, digitsValue;
    bool isJudging = true;
    bool isFirstAttempt = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!CardManager.isYourTurn && !CardManager.isGameEnded)
        {
            //手札の合計値が17より小さいなら
            if(CardManager.sumNumber_n < 17)
            {
                if(CardManager.isAbleToHit)
                {
                    HitManager.isHitPushed = true;
                    HitManager.isHitDown = true;
                    CardManager.isCardsOpened = false;
                }

                if(CardManager.isCardsOpened)
                {
                    JudgeIfUseItemBefore();
                }
            }

            //手札の合計値が17以上なら
            if(CardManager.sumNumber_n >= 17 && CardManager.sumNumber_n < 22)
            {
                if(isFirstOut)
                {
                    if(isJudging)
                    {
                        //もう一度HITするかどうかを判定する
                        JudgeIfUseItemAfter();
                    }
                }
                else
                {
                    //HITできない場合はSTANDする
                    if(!isChanceLeft && !CardManager.isOneMoreHit)
                    {
                        if(!CardManager.isCPUStood)
                        {
                            StandManager.isStandPushed = true;
                            if(CardManager.isAbleToUse)
                            {
                                CardManager.isItemFinished = true;
                            }
                        }
                        else
                        {
                            CardManager.isYourTurn = true;
                        }
                    }

                    //HITできる場合は最後のHITをする
                    if(isChanceLeft && isOnceSkipped){
                        if(CardManager.isAbleToHit)
                        {
                            HitManager.isHitPushed = true;
                            HitManager.isHitDown = true;
                        }

                        if(CardManager.isAbleToUse)
                        {
                            CardManager.isItemFinished = true;
                        }
                    }
                }
            }

            if(isChanceLeft && isFirstAttempt)
            {
                CardManager.isYourTurn = true;
                if(CardManager.isAbleToUse)
                {
                    CardManager.isItemFinished = true;
                }

                isFirstAttempt = false;
            }
        }
    }

    void JudgeIfUseItemBefore()
    {
        //最後に引いたカードがCPUの手札なら
        if((CardManager.numCard-1)%2 != 0 || CardManager.isPlayerStood)
        {
            GameObject targetCard = SetCards.CardList[CardManager.numCard-1];
            int addNumber = targetCard.GetComponent<CardMotion>().SaveCardNumber();
            int suitNumber = targetCard.GetComponent<CardMotion>().SaveSuitNumber();

            //所持しているアイテムを評価する
            if(itemNumber_CPU == 1)
            {
                if(CardManager.sumNumber_n+addNumber > 21 && CardManager.sumNumber_n+addNumber/2 <= 21)
                {
                    GameObject TargetItem = SetItems.ItemCards_CPU;
                    TargetItem.GetComponent<ItemEffects>().onClickAct();
                }
                else
                {
                    if(CardManager.isAbleToUse)
                    {
                        CardManager.isItemFinished = true;
                    }
                }
            }
            else if(itemNumber_CPU == 2)
            {
                //引いたカードがアイテムの適用対象なら
                if(addNumber <= 6)
                {   
                    if(CardManager.sumNumber_n+addNumber*2 > 17 && CardManager.sumNumber_n+addNumber*2 <= 21)
                    {
                        GameObject TargetItem = SetItems.ItemCards_CPU;
                        TargetItem.GetComponent<ItemEffects>().onClickAct();
                    }
                    else
                    {
                        if(CardManager.isAbleToUse)
                        {
                            CardManager.isItemFinished = true;
                        }
                    }
                }
                else
                {
                    if(CardManager.isAbleToUse)
                    {
                        CardManager.isItemFinished = true;
                    }
                }
            }
            else if(itemNumber_CPU == 3)
            {
                if(CardManager.sumNumber_n+addNumber >= 17 && CardManager.sumNumber_n+addNumber <= 21)
                {
                    //すべての場合においてBUSTしないか判定する
                    if(suitNumber == 11 && CardManager.sumNumber_n+addNumber+1 < 22)
                    {
                        GameObject TargetItem = SetItems.ItemCards_CPU;
                        TargetItem.GetComponent<ItemEffects>().onClickAct();
                    }
                    else if(suitNumber == 12 && CardManager.sumNumber_n+addNumber+2 < 22)
                    {
                        GameObject TargetItem = SetItems.ItemCards_CPU;
                        TargetItem.GetComponent<ItemEffects>().onClickAct();
                    }
                    else if(suitNumber == 13 && CardManager.sumNumber_n+addNumber+3 < 22)
                    {
                        GameObject TargetItem = SetItems.ItemCards_CPU;
                        TargetItem.GetComponent<ItemEffects>().onClickAct();
                    }
                    else
                    {
                        if(CardManager.isAbleToUse)
                        {
                            CardManager.isItemFinished = true;
                        }
                    }
                }
                else
                {
                    if(CardManager.isAbleToUse)
                    {
                        CardManager.isItemFinished = true;
                    }
                }
            }
            else if(itemNumber_CPU == 4)
            {
                //アイテムを使用した場合の期待値を算出する
                for(int i=0; i<4; i++)
                {
                    if(CardManager.sumNumber_n+addNumber+(i+10) >= 17)
                    {
                        if(CardManager.sumNumber_n+addNumber+(i+10) <= 21)
                        {
                            digitsValue++;
                        }
                    }
                }

                //期待値が基準値を超えたら
                if(digitsValue >= 3)
                {
                    GameObject TargetItem = SetItems.ItemCards_CPU;
                    TargetItem.GetComponent<ItemEffects>().onClickAct();

                    if(CardManager.isPlayerStood)
                    {
                        CardManager.isOneLastHit = true;
                    }

                    if(CardManager.isAbleToUse)
                    {
                        CardManager.isItemFinished = true;
                    }

                    digitsValue = 0;
                }
                else
                {
                    if(CardManager.isAbleToUse)
                    {
                        CardManager.isItemFinished = true;
                    }

                    digitsValue = 0;
                }
            }
            else
            {
                if(CardManager.isAbleToUse)
                {
                    CardManager.isItemFinished = true;
                }
            }
        }
    }

    void JudgeIfUseItemAfter()
    {
        if(itemNumber_CPU == 0)
        {
            //アイテムを使用した場合の期待値を算出する
            for(int i=0; i<5; i++)
            {
                if(CardManager.sumNumber_n + (i+1) < 22)
                {
                    limitValue++;
                }
            }

            //期待値が基準値を超えたら
            if(limitValue >= 3) 
            {
                GameObject TargetItem = SetItems.ItemCards_CPU;
                TargetItem.GetComponent<ItemEffects>().onClickAct();

                isFirstOut = false;
                if(CardManager.isPlayerStood)
                {
                    if(CardManager.isAbleToHit)
                    {
                        HitManager.isHitPushed = true;
                        HitManager.isHitDown = true;
                    }

                    if(CardManager.isAbleToUse)
                    {
                        CardManager.isItemFinished = true;
                    }

                    CardManager.isOneMoreHit = true;
                }
                else
                {
                    isChanceLeft = true;
                }
            }
            else
            {
                isJudging = false;
                StandManager.isStandPushed = true;

                if(CardManager.isAbleToUse)
                {
                    CardManager.isItemFinished = true;
                }
            }
        }
        else
        {
            isJudging = false;
            StandManager.isStandPushed = true;

            if(CardManager.isAbleToUse)
            {
                CardManager.isItemFinished = true;
            }
        }
    }
}