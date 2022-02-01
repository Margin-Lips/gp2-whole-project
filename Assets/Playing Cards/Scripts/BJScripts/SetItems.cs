using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItems : MonoBehaviour
{
    //生成するプレハブ格納用
    public GameObject ItemCard;
    //マテリアル格納用
    public Material[] MaterialSet = new Material[5];
    //CPU用のアイテム
    public static GameObject ItemCards_CPU;

    public static int playerItemNumber = 0;
    bool isAllItemsSet = false;

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerItem();
    }

    // Update is called once per frame
    void Update()
    {
        SetCPUItem();
    }

    //プレイヤーのアイテムを設定する
    void SetPlayerItem()
    {
        for(int i=0; i<3; i++)
        {
            //マテリアルをランダムで設定する(0～4)
            CardManager.numItemMaterial = Random.Range(0, 5);
            //生成するオブジェクトの位置を設定する
            Vector3 pos = new Vector3((i-1.0f)*0.2f, 1.3f, 0);
            //設定した位置にプレハブを生成する
            GameObject ItemCards = Instantiate(ItemCard, pos, Quaternion.Euler(0, 0, 180.0f));
            //プレハブのマテリアルを設定する
            ItemCards.GetComponent<MeshRenderer>().material = MaterialSet[CardManager.numItemMaterial];
            //アイテムの番号を記憶する
            ItemCards.GetComponent<ItemEffects>().SaveItemNumber();
        }
    }

    //CPUのアイテムを設定する
    void SetCPUItem()
    {
        if(CardManager.isItemSelected && !isAllItemsSet)
        {
            //プレイヤーのアイテム番号に応じて範囲を変更する
            if(playerItemNumber == 0)
            {
                //マテリアルをランダムで設定する(1～4)
                CardManager.numItemMaterial = Random.Range(1, 5);
            }
            else if(playerItemNumber == 4)
            {
                //マテリアルをランダムで設定する(0～3)
                CardManager.numItemMaterial = Random.Range(0, 4);
            }
            else
            {
                //マテリアルをランダムで設定する(0～4)
                CardManager.numItemMaterial = Random.Range(0, 5);
            }
            
            //生成するオブジェクトの位置を設定する
            Vector3 pos_CPU = new Vector3(1.3f, 1.3f, 0.71f);
            //設定した位置にプレハブを生成する
            ItemCards_CPU = Instantiate(ItemCard, pos_CPU, Quaternion.Euler(0, 0, 0));
            //プレハブのマテリアルを設定する
            ItemCards_CPU.GetComponent<MeshRenderer>().material = MaterialSet[CardManager.numItemMaterial];
            //アイテムの番号を記憶する
            CPUManager.itemNumber_CPU = CardManager.numItemMaterial;
            ItemCards_CPU.GetComponent<ItemEffects>().SaveItemNumber();
            //CPUのアイテムとして設定する
            ItemCards_CPU.GetComponent<ItemEffects>().JudgeIsCPUsCard();

            isAllItemsSet = true;
        }
    }
}
