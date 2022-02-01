using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects01 : MonoBehaviour
{
    [SerializeField] private AudioSource turnCard;
    [SerializeField] private AudioSource itemEffect;

    Vector3 pos, pos_pre, pos_cur_1, pos_cur_2, angl;

    int itemNumber;
    int itemOrder;
    bool isMoving = false;
    bool isTurning = false;
    bool isThisSelected = false;
    bool isThisUsed = false;
    bool isActivated = false;
    bool isCPUsCard = false;
    bool isFirstSound = true;

    // Start is called before the first frame update
    void Start()
    {
        //初期位置を設定する
        pos_pre = transform.position;

        //目標位置を設定する
        pos_cur_1.Set(1.13f, 1.3f, -0.7f);
        pos_cur_2.Set(1.3f, 1.3f, -0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        //選択されたアイテム以外を消去する
        if(CardManager01.isItemSelected)
        {
            if(isThisSelected)
            {
                if(isMoving)
                {
                    MoveCard();
                }
            }
            else
            {
                if(!isCPUsCard)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        if(isTurning)
        {
            if(!SoundsManager.isSoundMuted && isFirstSound)
            {
                turnCard.Play();
                isFirstSound = false;
            }

            TurnCard();
        }

        if(CardManager01.isItemSelected && isActivated)
        {
            if(!SoundsManager.isSoundMuted)
            {
                itemEffect.Play();
            }

            ActivateEffect();
            isActivated = false;
            isThisUsed = true;
        }

        if(SceneChanger.isIfMenuScene)
        {
            Destroy(this.gameObject);
        }
    }

    //アイテムの番号を記憶する
    public void SaveItemNumber()
    {
        itemNumber = CardManager01.numItemMaterial;
    }

    //CPUのアイテムかどうか判定する
    public void JudgeIsCPUsCard()
    {
        isCPUsCard = true;
    }

    //アイテムが選択された順番を記憶する
    public void SaveSelectedOrder()
    {
        itemOrder = CardManager01.numSelectedItems;
    }

    //アイテムが使用済みかどうか判定する
    public bool SaveUsedState()
    {
        return isThisUsed;
    }

    //アイテムの効果を発動する
    public void ActivateEffect()
    {
        if(itemNumber == 0)
        {
            //アイテム効果：LIMIT
            if(CardManager01.isFirstHit)
            {   
                SetCards01.isDirectlyLimited = true;
            }
            else
            {
                SetCards01.isLimitCameBack = false;
                SetCards01.isLimitActivated = true;
            }

            if(!CardManager01.isYourTurn && CardManager01.isAbleToUse)
            {
                CardManager01.isItemFinished = true;
            }
        }
        else if(itemNumber == 1)
        {
            //アイテム効果：HALF
            GameObject targetCard = SetCards01.CardList[CardManager01.numCard-1];
            targetCard.GetComponent<CardMotion01>().DevideCardNumber();

            if(!CardManager01.isYourTurn && CardManager01.isAbleToUse)
            {
                CardManager01.isItemFinished = true;
            }
        }
        else if(itemNumber == 2)
        {
            //アイテム効果：DOUBLE
            GameObject targetCard = SetCards01.CardList[CardManager01.numCard-1];
            targetCard.GetComponent<CardMotion01>().MultiplyCardNumber();

            if(!CardManager01.isYourTurn && CardManager01.isAbleToUse)
            {
                CardManager01.isItemFinished = true;
            }
        }
        else if(itemNumber == 3)
        {
            //アイテム効果：ORIGIN
            GameObject targetCard = SetCards01.CardList[CardManager01.numCard-1];
            targetCard.GetComponent<CardMotion01>().ChangeSuitNumber();
            
            if(!CardManager01.isYourTurn && CardManager01.isAbleToUse)
            {
                CardManager01.isItemFinished = true;
            }
        }
        else
        {
            //アイテム効果：DIGITS
            if(CardManager01.isFirstHit)
            {   
                SetCards01.isDirectlyDigits = true;
            }
            else
            {
                SetCards01.isDigitsCameBack = false;
                SetCards01.isDigitsActivated = true;
            }

            if(!CardManager01.isYourTurn && CardManager01.isAbleToUse)
            {
                CardManager01.isItemFinished = true;
            }
        }

        GetComponent<Renderer>().material.color = new Color32(100, 100, 100, 255);
    }

    void MoveCard()
    {
        float r = (Time.time - CardManager01.start) / 1.2f;
        if(itemOrder == 1)
        {
            pos = r*pos_cur_1 + (1.0f-r)*pos_pre;
        }
        if(itemOrder == 2)
        {
            pos = r*pos_cur_2 + (1.0f-r)*pos_pre;
        }

        if(r > 1.0f)
        {
            if(itemOrder == 1)
            {
                pos = new Vector3(1.13f, 1.3f, -0.7f);
            }
            if(itemOrder == 2)
            {
                pos = new Vector3(1.3f, 1.3f, -0.7f);
            }
            isMoving = false;
        }

        transform.position = pos;
    }

    void TurnCard()
    {
        angl = transform.eulerAngles;

        angl.z -= 4.0f;
        if(angl.z <= 0)
        {
            angl.z = 0;
            if(itemOrder == 1)
            {
                SetItems01.itemNumber_p1 = itemNumber;
            }
            if(itemOrder == 2)
            {
                SetItems01.itemNumber_p2 = itemNumber;
            }

            isMoving = true;
            isTurning = false;

            if(CardManager01.numSelectedItems >= 2)
            {
                CardManager01.start = Time.time;

                CardManager01.isItemSelected = true;
                CardManager01.isGameStarted = true;
            }
        }

        transform.eulerAngles = angl;
    }

    public void onClickAct()
    {
        //アイテムを選択する
        if(!isTurning && !CardManager01.isItemSelected)
        {
            isTurning = true;
            isThisSelected = true;

            CardManager01.numSelectedItems++;
            SaveSelectedOrder();
        }

        //P：アイテム発動
        if(CardManager01.isYourTurn && CardManager01.isAbleToUse && !isThisUsed)
        {
            isActivated = true;
        }

        //C：アイテム発動
        if(!CardManager01.isYourTurn && isCPUsCard && !isThisUsed)
        {
            isActivated = true;
        }
    }
}