using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static bool isIfMenuScene = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CardManager.isGameEnded)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Menu_Scene");
                isIfMenuScene = true;
                ResetBJSettings();
            }
        }
        if(CardManager01.isGameEnded)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Menu_Scene");
                isIfMenuScene = true;
                Reset01Settings();
            }
        }
    }

    //BJゲーム：終了後に初期化する
    void ResetBJSettings()
    {
        CardManager.numCard = 0;
        CardManager.numCardMaterial = 0;
        CardManager.numItemMaterial = 0;
        CardManager.sumNumber_p = 0;
        CardManager.sumNumber_n = 0;
        CardManager.winnerCase = 0;
        CardManager.isPlayerStood = false;;
        CardManager.isCPUStood = false;
        CardManager.isItemSelected = false;
        CardManager.isItemFinished = false;
        CardManager.isCardsOpened = true;
        CardManager.isYourTurn = true;
        CardManager.isOneMoreHit = false;
        CardManager.isOneLastHit = false;
        CardManager.isAbleToHit = true;
        CardManager.isAbleToUse = false;
        CardManager.isCalcFinished = false;
        CardManager.isGameStarted = false;
        CardManager.isGameEnded = false;

        CardMotion.pos_card = 0;

        CPUManager.itemNumber_CPU = 0;
        CPUManager.isFirstOut = true;
        CPUManager.isChanceLeft = false;
        CPUManager.isOnceSkipped = false;

        HitManager.isHitPushed = false;
        HitManager.isHitDown = false;

        SetCards.CardList = new List<GameObject>();
        SetCards.isLimitActivated = false;
        SetCards.isLimitCameBack = true;
        SetCards.isDirectlyLimited = false;
        SetCards.isDigitsActivated = false;
        SetCards.isDigitsCameBack = true;
        SetCards.isDirectlyDigits = false;
        SetCards.isDigitsColorChanged = true;

        SetItems.playerItemNumber = 0;

        StandManager.isStandPushed = false;
        StandManager.isStandDown = false;
    }

    //BJゲーム：終了後に初期化する
    void Reset01Settings()
    {
        CardManager01.numLeftTurn = 15;
        CardManager01.numCard = 0;
        CardManager01.numDeletedCard = 0;
        CardManager01.numCardMaterial = 0;
        CardManager01.numItemMaterial = 0;
        CardManager01.sumNumber_p = 0;
        CardManager01.sumNumber_n = 0;
        CardManager01.subNumber = 0;
        CardManager01.winnerCase = 0;
        CardManager01.bustCase = 0;
        CardManager01.numSelectedItems = 0;
        CardManager01.start = 0;
        CardManager01.isItemSelected = false;
        CardManager01.isItemFinished = false;
        CardManager01.isCardsOpened = true;
        CardManager01.isYourTurn = true;
        CardManager01.isFirstHit = true;
        CardManager01.isAbleToHit = true;
        CardManager01.isAbleToUse = false;
        CardManager01.isGameStarted = false;
        CardManager01.isGameEnded = false;

        CPUManager01.itemNumber_c1 = 0;
        CPUManager01.itemNumber_c2 = 0;
        CPUManager01.isCPUsItemUsed = false;

        HitManager01.isHitPushed = false;
        HitManager01.isHitDown = false;

        SetCards01.CardList = new List<GameObject>();
        SetCards01.isLimitActivated = false;
        SetCards01.isLimitCameBack = true;
        SetCards01.isDirectlyLimited = false;
        SetCards01.isDigitsActivated = false;
        SetCards01.isDigitsCameBack = true;
        SetCards01.isDirectlyDigits = false;
        SetCards01.isDigitsColorChanged = true;

        SetItems01.itemNumber_p1 = 0;
        SetItems01.itemNumber_p2 = 0;
        SetItems01.ItemList = new List<GameObject>();

        TextManager01.subSavedNumber = 0;
    }
}
