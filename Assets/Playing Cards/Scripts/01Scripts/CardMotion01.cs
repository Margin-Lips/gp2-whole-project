using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMotion01 : MonoBehaviour
{    
    //マテリアル格納用
    public Material[] ChangedMaterialSet = new Material[13];

    Vector3 pos, pos_sv, pos_pre, pos_cur, angl;
    int cardNumber, suitNumber;
    float start, delta;
    bool isFirstSound = true;
    bool isFirstSet = true;
    bool isFirstSave = true;
    bool isMoving = true;
    bool isTurning = false;
    bool isCardShifted = true;

    // Start is called before the first frame update
    void Start()
    {
        //初期位置を設定する
        pos_pre.Set(-1.3f, 1.28f, 0);

        //目標位置を設定する
        if(CardManager01.isYourTurn)
        {
            pos_cur.Set(-0.986f, 1.3f, -0.7f);
        }
        else
        {
            pos_cur.Set(-0.986f, 1.3f, 0.71f);
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

        if(!isCardShifted)
        {
            ShiftCard();
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
            if(CardManager01.numCardMaterial <= 9 || !SetCards01.isDigitsColorChanged)
            {
                cardNumber = CardManager01.numCardMaterial + 1;
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
        suitNumber = CardManager01.numCardMaterial + 1;
        return suitNumber;
    }

    //HITしたら場にあるカードを横にずらす
    public void ShiftCardPosition()
    {
        if((CardManager01.isYourTurn && pos.z < -0.4f) || (!CardManager01.isYourTurn && pos.z > 0.4f))
        {
            isCardShifted = false;
        }
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
    }

    //カードを両者のカード置き場に配布する
    void MoveCard()
    {
        float r = (Time.time - start) / 0.8f;
        pos = r*pos_cur + (1.0f-r)*pos_pre;

        if(r > 1.0f)
        {
            if(CardManager01.isYourTurn)
            {
                pos = new Vector3(-0.986f, 1.3f, -0.7f);
            }
            else
            {
                pos = new Vector3(-0.986f, 1.3f, 0.71f);
                isTurning = true;
            }

            isMoving = false;
        }

        transform.position = pos;
    }

    //HITしたら場にあるカードを横にずらす
    void ShiftCard()
    {
        pos = transform.position;

        if(isFirstSave)
        {
            pos_sv = pos;
            isFirstSave = false;
        }

        delta += 0.007f;
        pos.x += 0.007f;

        if(delta >= 0.2f)
        {
            delta = 0;
            pos.x = pos_sv.x + 0.2f;
            isFirstSave = true;
            isCardShifted = true;
        }

        if(pos.x > 0.82f)
        {
            Destroy(this.gameObject);
            CardManager01.numDeletedCard++;
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
            CardManager01.isCardsOpened = true;
        }

        transform.eulerAngles = angl;
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