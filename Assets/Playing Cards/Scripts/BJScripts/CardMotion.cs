using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMotion : MonoBehaviour
{
    public static int pos_card = 0;
    
    //マテリアル格納用
    public Material[] ChangedMaterialSet = new Material[13];

    Vector3 pos, pos_pre, pos_cur, angl;
    int cardNumber, suitNumber;
    float start = 0;
    bool isFirstSound = true;
    bool isFirstSet = true;
    bool isMoving = true;
    bool isTurning = false;

    // Start is called before the first frame update
    void Start()
    {
        //初期位置を設定する
        pos_pre.Set(-1.3f, 1.28f, 0);

        //目標位置を設定する
        if(CardManager.isYourTurn)
        {
            if(!CardManager.isCPUStood)
            {
                pos_cur.Set((pos_card-6.5f)/6 + 0.1f, 1.3f, -0.7f);
            }
            else
            {
                pos_cur.Set((pos_card-6.5f)/6 - 0.065f, 1.3f, -0.7f);
            }
        }
        else
        {
            pos_cur.Set((pos_card-6.5f)/6 - 0.07f, 1.3f, 0.71f);
        }

        start = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            MoveCard();
        }

        if(isTurning)
        {
            if(!SoundsManager.isSoundMuted && isFirstSound)
            {
                GetComponent<AudioSource>().Play();
                isFirstSound = false;
            }

            TurnCard();
        }

        if(StandManager.isStandPushed)
        {
            DecideHand();
        }

        if(SceneChanger.isIfMenuScene)
        {
            Destroy(this.gameObject);
        }
    }

    //カードの数字を記憶する/返す
    public int SaveCardNumber()
    {
        if(isFirstSet)
        {
            if(CardManager.numCardMaterial <= 9 || !SetCards.isDigitsColorChanged)
            {
                cardNumber = CardManager.numCardMaterial + 1;
            }
            else
            {
                cardNumber = 10;
            }
            
            isFirstSet = false;
        }

        return cardNumber;
    }

    //カードの絵柄を記憶する/返す
    public int SaveSuitNumber()
    {
        suitNumber = CardManager.numCardMaterial + 1;
        return suitNumber;
    }

    //アイテム効果：HALF
    public void DevideCardNumber()
    {
        if(cardNumber != 1)
        {
            cardNumber = cardNumber / 2;
            GetComponent<MeshRenderer>().material = ChangedMaterialSet[cardNumber-1];
        }
        else
        {
            GetComponent<MeshRenderer>().material = ChangedMaterialSet[0];
        }
    }

    //アイテム効果：DOUBLE
    public void MultiplyCardNumber()
    {
        if(cardNumber <= 6)
        {
            cardNumber = cardNumber * 2;
            GetComponent<MeshRenderer>().material = ChangedMaterialSet[cardNumber-1];
        }
    }

    //アイテム効果：ORIGIN
    public void ChangeSuitNumber()
    {
        if(cardNumber != suitNumber)
        {
            cardNumber = suitNumber;
            GetComponent<MeshRenderer>().material = ChangedMaterialSet[cardNumber-1];
        }

        if(CardManager.isAbleToUse)
        {
            CardManager.isItemFinished = true;
        }
    }

    //カードを両者のカード置き場に配布する
    void MoveCard()
    {
        float r = (Time.time - start) / 0.8f;
        pos = r*pos_cur + (1.0f-r)*pos_pre;

        if(r > 1.0f)
        {
            if(CardManager.isYourTurn)
            {
                if(!CardManager.isCPUStood)
                {
                    pos = new Vector3((pos_card-6.5f)/6 + 0.1f, 1.3f, -0.7f);
                }
                else
                {
                    pos = new Vector3((pos_card-6.5f)/6 - 0.065f, 1.3f, -0.7f);
                }
            }
            else
            {
                pos = new Vector3((pos_card-6.5f)/6 - 0.07f, 1.3f, 0.71f);
                isTurning = true;
            }

            isMoving = false;
        }

        transform.position = pos;
    }

    //C：配布されたカードを展開する
    void TurnCard()
    {
        angl = transform.eulerAngles;

        angl.z -= 4.0f;
        if(angl.z <= 0)
        {
            angl.z = 0;
            isTurning = false;
            CardManager.isCardsOpened = true;

            if(CardManager.isOneMoreHit && CardManager.isAbleToUse)
            {
                CardManager.isItemFinished = true;
            }
        }

        transform.eulerAngles = angl;
    }

    //STANDしたら手札カードを前に出す
    void DecideHand()
    {
        if(CardManager.isYourTurn && pos.z < -0.4f)
        {
            pos.z += 0.01f;

            if(pos.z > -0.4f)
            {
                pos.z = -0.4f;
                CardManager.isPlayerStood = true;

                StandManager.isStandPushed = false;
            }

            transform.position = pos;
        }

        if(!CardManager.isYourTurn && pos.z > 0.4f)
        {
            pos.z -= 0.01f;

            if(pos.z < 0.4f)
            {
                pos.z = 0.4f;
                CardManager.isCPUStood = true;

                StandManager.isStandPushed = false;
                CPUManager.isFirstOut = false;
            }

            transform.position = pos;
        }
    }

    //P：クリックされたらカードを展開する
    public void onClickAct()
    {
        if(!isTurning && !isMoving)
        {
            isTurning = true;
        }
    }
}
