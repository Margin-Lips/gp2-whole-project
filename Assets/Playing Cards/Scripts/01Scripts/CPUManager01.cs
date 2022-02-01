using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUManager01 : MonoBehaviour
{
    public static int itemNumber_c1 = 0;
    public static int itemNumber_c2 = 0;
    public static bool isCPUsItemUsed = false;
    
    int limitValue, digitsValue, notValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!CardManager01.isYourTurn && !CardManager01.isGameEnded)
        {
            //手札の合計が101より小さいなら
            if(CardManager01.sumNumber_n < 101)
            {
                if(CardManager01.isAbleToHit)
                {
                    HitManager01.isHitPushed = true;
                    HitManager01.isHitDown = true;
                    CardManager01.isCardsOpened = false;
                }

                if(CardManager01.isCardsOpened)
                {
                    notValue = 0;
                    JudgeIfUseItem();
                }
            }
            else
            {
                //BUST確定ならアイテムを温存して流す
                if(CardManager01.isAbleToHit)
                {
                    HitManager01.isHitPushed = true;
                    HitManager01.isHitDown = true;
                    CardManager01.isCardsOpened = false;
                }

                if(CardManager01.isAbleToUse)
                {
                    CardManager01.isItemFinished = true;
                }
            }
        }
    }

    void JudgeIfUseItem()
    {
        if(!CardManager01.isYourTurn)
        {
            GameObject targetCard = SetCards01.CardList[CardManager01.numCard-1];
            GameObject TargetItem_1 = SetItems01.ItemList[0];
            GameObject TargetItem_2 = SetItems01.ItemList[1];
            
            int preNumber = CardManager01.sumNumber_n;
            int addNumber = targetCard.GetComponent<CardMotion01>().SaveCardNumber();
            int suitNumber = targetCard.GetComponent<CardMotion01>().SaveSuitNumber();

            //所持しているアイテムを評価する
            //アイテム使用の優先順位：4→3→2→0→1 (序盤→終盤)
            if((itemNumber_c1 == 4 || itemNumber_c2 == 4) && !isCPUsItemUsed)
            {
                //アイテムを使用した場合の期待値を算出する
                for(int i=0; i<4; i++)
                {
                    if(preNumber+addNumber+(i+10) <= 101)
                    {
                        digitsValue++;
                    }
                }

                //期待値が基準値を超えたら
                if(digitsValue >= 4)
                {
                    if(itemNumber_c1 == 4 && !TargetItem_1.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_1.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                        
                    }
                    else if(itemNumber_c2 == 4 && !TargetItem_2.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_2.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else
                    {
                        notValue++;
                    }

                    digitsValue = 0;
                }
                else
                {
                    notValue++;
                    digitsValue = 0;
                }
            }
            else
            {
                notValue++;
            }

            if((itemNumber_c1 == 3 || itemNumber_c2 == 3) && !isCPUsItemUsed)
            {
                bool isBetterToUse = false;

                //ターン数に応じた条件で判定する
                if(CardManager01.numLeftTurn <= 3 && !CardManager01.isFirstHit)
                {
                    if(addNumber != suitNumber && preNumber+suitNumber <= 101)
                    {
                        isBetterToUse = true;
                    }
                }
                else
                {
                    if((addNumber != suitNumber && suitNumber >= 12) && preNumber+suitNumber <= 101)
                    {
                        isBetterToUse = true;
                    }
                    if((addNumber != suitNumber && suitNumber == 11) && preNumber+suitNumber == 101)
                    {
                        isBetterToUse = true;
                    }
                }
                
                //ターン数に応じた条件を満たしたら
                if(isBetterToUse)
                {
                    if(itemNumber_c1 == 3 && !TargetItem_1.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_1.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else if(itemNumber_c2 == 3 && !TargetItem_2.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_2.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else
                    {
                        notValue++;
                    }
                }
                else
                {
                    notValue++;
                }
            }
            else
            {
                notValue++;
            }

            if((itemNumber_c1 == 2 || itemNumber_c2 == 2) && !isCPUsItemUsed)
            {
                bool isBetterToUse = false;

                //ターン数に応じた条件で判定する
                if(CardManager01.numLeftTurn <= 3 && !CardManager01.isFirstHit)
                {
                    if(addNumber < 7 && (addNumber >= 5 && preNumber+addNumber*2 <= 101))
                    {
                        isBetterToUse = true;
                    }
                    if(addNumber < 7 && (preNumber+addNumber*2 > 95 && preNumber+addNumber*2 <= 101))
                    {
                        isBetterToUse = true;
                    }
                }
                else
                {
                    if(addNumber < 7 && ((addNumber >= 5 && preNumber+addNumber*2 <= 101) || preNumber+addNumber*2 == 101))
                    {
                        isBetterToUse = true;
                    }
                }

                //ターン数に応じた条件を満たしたら
                if(isBetterToUse)
                {
                    if(itemNumber_c1 == 2 && !TargetItem_1.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_1.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else if(itemNumber_c2 == 2 && !TargetItem_2.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_2.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else
                    {
                        notValue++;
                    }
                }
                else
                {
                    notValue++;
                }
            }
            else
            {
                notValue++;
            }

            if((itemNumber_c1 == 0 || itemNumber_c2 == 0) && !isCPUsItemUsed)
            {
                //アイテムを使用した場合の期待値を算出する
                if(!CardManager01.isFirstHit)
                {
                    for(int i=0; i<5; i++)
                    {
                        if(preNumber+(i+1) > 96 && preNumber+(i+1) <= 101)
                        {
                            limitValue++;
                        }
                    }
                }

                //期待値がターン数に応じた基準値を超えたら
                if((CardManager01.numLeftTurn > 3 && limitValue >= 5) || (CardManager01.numLeftTurn <= 3 && limitValue >= 3))
                {
                    if(itemNumber_c1 == 0 && !TargetItem_1.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_1.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else if(itemNumber_c2 == 0 && !TargetItem_2.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_2.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else
                    {
                        notValue++;
                    }

                    limitValue = 0;
                }
                else
                {
                    notValue++;
                    limitValue = 0;
                }
            }
            else
            {
                notValue++;
            }

            if((itemNumber_c1 == 1 || itemNumber_c2 == 1) && !isCPUsItemUsed)
            {
                bool isBetterToUse = false;

                //ターン数に応じた条件で判定する
                if(CardManager01.numLeftTurn <= 3 && !CardManager01.isFirstHit)
                {
                    if((preNumber+addNumber > 101 && preNumber+addNumber/2 <= 101) || preNumber+addNumber/2 == 91)
                    {
                        isBetterToUse = true;
                    }
                }
                else
                {
                    if(preNumber+addNumber/2 == 101 || preNumber+addNumber/2 == 91)
                    {
                        isBetterToUse = true;
                    }
                }

                //ターン数に応じた条件を満たしたら
                if(isBetterToUse)
                {
                    if(itemNumber_c1 == 1 && !TargetItem_1.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_1.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else if(itemNumber_c2 == 1 && !TargetItem_2.GetComponent<ItemEffects01>().SaveUsedState())
                    {
                        TargetItem_2.GetComponent<ItemEffects01>().onClickAct();
                        isCPUsItemUsed = true;
                    }
                    else
                    {
                        notValue++;
                    }
                }
                else
                {
                    notValue++;
                }
            }
            else
            {
                notValue++;
            }
            
            //いずれのアイテムも使用しないならターン終了
            if(notValue == 5)
            {
                if(CardManager01.isAbleToUse)
                {
                    CardManager01.isItemFinished = true;
                }
            }
        }
    }
}