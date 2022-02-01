using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffects : MonoBehaviour
{
    [SerializeField] private AudioSource turnCard;
    [SerializeField] private AudioSource itemEffect;

    Vector3 pos, pos_pre, pos_cur, angl;

    int itemNumber;
    float start;
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
        pos_cur.Set(1.3f, 1.3f, -0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        //選択されたアイテム以外を消去する
        if(CardManager.isItemSelected)
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

        if(CardManager.isItemSelected && isActivated)
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
        itemNumber = CardManager.numItemMaterial;
    }

    //CPUのアイテムかどうか判定する
    public void JudgeIsCPUsCard()
    {
        isCPUsCard = true;
    }

    //アイテムの効果を発動する
    public void ActivateEffect()
    {
        if(itemNumber == 0)
        {
            //アイテム効果：LIMIT
            if(CardManager.isOneMoreHit)
            {   
                SetCards.isDirectlyLimited = true;
            }
            else
            {
                SetCards.isLimitCameBack = false;
                SetCards.isLimitActivated = true;
            }
        }
        else if(itemNumber == 1)
        {
            //アイテム効果：HALF
            GameObject targetCard = SetCards.CardList[CardManager.numCard-1];
            targetCard.GetComponent<CardMotion>().DevideCardNumber();
        }
        else if(itemNumber == 2)
        {
            //アイテム効果：DOUBLE
            GameObject targetCard = SetCards.CardList[CardManager.numCard-1];
            targetCard.GetComponent<CardMotion>().MultiplyCardNumber();
        }
        else if(itemNumber == 3)
        {
            //アイテム効果：ORIGIN
            GameObject targetCard = SetCards.CardList[CardManager.numCard-1];
            targetCard.GetComponent<CardMotion>().ChangeSuitNumber();
        }
        else
        {
            //アイテム効果：DIGITS
            if(CardManager.isOneLastHit)
            {   
                SetCards.isDirectlyDigits = true;
            }
            else
            {
                SetCards.isDigitsCameBack = false;
                SetCards.isDigitsActivated = true;
            }
        }

        GetComponent<Renderer>().material.color = new Color32(100, 100, 100, 255);
    }

    void MoveCard()
    {
        float r = (Time.time - start) / 1.2f;
        pos = r*pos_cur + (1.0f-r)*pos_pre;

        if(r > 1.0f)
        {
            pos = new Vector3(1.3f, 1.3f, -0.7f);
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
            start = Time.time;
            SetItems.playerItemNumber = itemNumber;

            isMoving = true;
            isTurning = false;
            CardManager.isItemSelected = true;
            CardManager.isGameStarted = true;
        }

        transform.eulerAngles = angl;
    }

    public void onClickAct()
    {
        //アイテムを選択する
        if(!isTurning && !CardManager.isItemSelected)
        {
            isTurning = true;
            isThisSelected = true;
        }

        //P：アイテム発動
        if(CardManager.isYourTurn && CardManager.isAbleToUse && !isThisUsed)
        {
            isActivated = true;

            if(itemNumber == 0 && CardManager.isCPUStood)
            {
                CardManager.isOneMoreHit = true;
            }
            if(itemNumber == 4 && CardManager.isCPUStood)
            {
                CardManager.isOneLastHit = true;
            }
        }

        //C：アイテム発動
        if(!CardManager.isYourTurn && isCPUsCard && !isThisUsed)
        {
            isActivated = true;
        }
    }
}
