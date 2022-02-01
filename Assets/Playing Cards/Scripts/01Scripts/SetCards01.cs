using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCards01 : MonoBehaviour
{
    //生成するプレハブ格納用
    public GameObject PlayingCard;

    //マテリアル格納用
    public Material[] MaterialSet = new Material[13];
    public Material[] AnotherSet = new Material[13];

    //プレハブを格納するリスト
    public static List<GameObject> CardList = new List<GameObject>();

    public static bool isLimitActivated = false;
    public static bool isLimitCameBack = true;
    public static bool isDirectlyLimited = false;

    public static bool isDigitsActivated = false;
    public static bool isDigitsCameBack = true;
    public static bool isDirectlyDigits = false;

    public static bool isDigitsColorChanged = true;
    bool isLimitColorChanged = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //HITボタンが押されたら
        if(HitManager01.isHitPushed)
        {
            if(CardManager01.numCard < 1000)
            {
                CardManager01.isCardsOpened = false;

                //アイテム効果：LIMITorDIGITS
                if(isLimitActivated && isLimitCameBack || isDirectlyLimited)
                {
                    //マテリアルをランダムで設定する(0~4)
                    CardManager01.numCardMaterial = Random.Range(0, 5);

                    if(isLimitActivated)
                    {
                        isLimitActivated = false;
                        isLimitCameBack = false;
                    }
                    if(isDirectlyLimited)
                    {
                        isDirectlyLimited = false;
                    }
                    if(isLimitColorChanged)
                    {
                        isLimitColorChanged = false;
                    }

                    if(!isDigitsCameBack && !CardManager01.isFirstHit)
                    {
                        isDigitsCameBack = true;
                    }
                }
                else if(isDigitsActivated && isDigitsCameBack || isDirectlyDigits)
                {
                    //マテリアルをランダムで設定する(9~12)
                    CardManager01.numCardMaterial = Random.Range(9, 13);

                    if(isDigitsActivated)
                    {
                        isDigitsActivated = false;
                        isDigitsCameBack = false;
                    }
                    if(isDirectlyDigits)
                    {
                        isDirectlyDigits = false;
                    }
                    if(isDigitsColorChanged)
                    {
                        isDigitsColorChanged = false;
                    }

                    if(!isLimitCameBack && !CardManager01.isFirstHit)
                    {
                        isLimitCameBack = true;
                    }
                }
                else
                {
                    //マテリアルをランダムで設定する(0～12)
                    if(CardManager01.isYourTurn){
                        CardManager01.numCardMaterial = Random.Range(0, 13);
                    }
                    else
                    {
                        CardManager01.numCardMaterial = Random.Range(0, 13);
                    }

                    if(!isLimitCameBack && !CardManager01.isFirstHit)
                    {
                        isLimitCameBack = true;
                    }
                    if(!isDigitsCameBack && !CardManager01.isFirstHit)
                    {
                        isDigitsCameBack = true;
                    }
                }

                //生成するオブジェクトの位置を設定する
                Vector3 pos = new Vector3(-1.3f, 1.3f, 0);
                //設定した位置にプレハブを生成する
                GameObject PlayingCards = Instantiate(PlayingCard, pos, Quaternion.Euler(0, 0, 180.0f));
                //プレハブのマテリアルを設定する
                if(!isLimitColorChanged)
                {        
                    PlayingCards.GetComponent<MeshRenderer>().material = AnotherSet[CardManager01.numCardMaterial];
                    isLimitColorChanged = true;
                }
                else if(!isDigitsColorChanged)
                {
                    PlayingCards.GetComponent<MeshRenderer>().material = AnotherSet[CardManager01.numCardMaterial];
                }
                else
                {
                    PlayingCards.GetComponent<MeshRenderer>().material = MaterialSet[CardManager01.numCardMaterial];
                }
                //カードの数字を記憶する
                if(!isDigitsColorChanged)
                {
                    PlayingCards.GetComponent<CardMotion01>().SaveCardNumber();
                    isDigitsColorChanged = true;
                }
                else
                {
                    PlayingCards.GetComponent<CardMotion01>().SaveCardNumber();
                }
                //カードの絵柄を記憶する
                PlayingCards.GetComponent<CardMotion01>().SaveSuitNumber();
                //プレハブをリストに追加する
                CardList.Add(PlayingCards);
                //カード数のカウントを増やす
                CardManager01.numCard++;

                if(CardManager01.numCard >= 2)
                {
                    for(int i=CardManager01.numDeletedCard; i<CardManager01.numCard-1; i++)
                    {
                        CardList[i].GetComponent<CardMotion01>().ShiftCardPosition();
                    }
                }
            }

            HitManager01.isHitPushed = false;
        }    
    }
}
