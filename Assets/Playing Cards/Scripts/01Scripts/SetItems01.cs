using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItems01 : MonoBehaviour
{
    //生成するプレハブ格納用
    public GameObject ItemCard;
    //マテリアル格納用
    public Material[] MaterialSet = new Material[5];
    //CPU用のアイテム
    public GameObject ItemCards_CPU;

    public static int itemNumber_p1 = 0;
    public static int itemNumber_p2 = 0;

    //プレハブを格納するリスト
    public static List<GameObject> ItemList = new List<GameObject>();

    Vector3 pos_CPU;
    bool isAllItemsSet = false;

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerItems();
    }

    // Update is called once per frame
    void Update()
    {
        if(CardManager01.isItemSelected && !isAllItemsSet)
        {
            SetCPUItems();
            isAllItemsSet = true;
        }
    }

    //プレイヤーのアイテムを設定する
    void SetPlayerItems()
    {
        for(int i=0; i<5; i++)
        {
            //マテリアルをランダムで設定する(0～4)
            CardManager01.numItemMaterial = Random.Range(0, 5);
            //生成するオブジェクトの位置を設定する
            Vector3 pos = new Vector3((i-2.01f)*0.2f, 1.3f, 0);
            //設定した位置にプレハブを生成する
            GameObject ItemCards = Instantiate(ItemCard, pos, Quaternion.Euler(0, 0, 180.0f));
            //プレハブのマテリアルを設定する
            ItemCards.GetComponent<MeshRenderer>().material = MaterialSet[CardManager01.numItemMaterial];
            //アイテムの番号を記憶する
            ItemCards.GetComponent<ItemEffects01>().SaveItemNumber();
        }
    }

    //CPUのアイテムを設定する
    void SetCPUItems()
    {
        for(int i=0; i<2; i++)
        {
            //プレイヤーのアイテム番号に応じて範囲を変更する
            if((itemNumber_p1 ==0 && itemNumber_p2 == 4) || (itemNumber_p1 ==4 && itemNumber_p2 == 0))
            {
                //マテリアルをランダムで設定する(1～3)
                CardManager01.numItemMaterial = Random.Range(1, 4);
            }
            else
            {
                if(itemNumber_p1 == 0 || itemNumber_p2 == 0)
                {
                    //マテリアルをランダムで設定する(1～4)
                    CardManager01.numItemMaterial = Random.Range(1, 5);
                }
                else if(itemNumber_p1 == 4 || itemNumber_p2 == 4)
                {
                    //マテリアルをランダムで設定する(0～3)
                    CardManager01.numItemMaterial = Random.Range(0, 4);
                }
                else
                {
                    //マテリアルをランダムで設定する(0～4)
                    CardManager01.numItemMaterial = Random.Range(0, 5);
                }
            }
            
            //2箇所にアイテムを配置する
            if(i == 0)
            {
                //1枚目：生成するオブジェクトの位置を設定する
                pos_CPU = new Vector3(1.13f, 1.3f, 0.71f);
                CPUManager01.itemNumber_c1 = CardManager01.numItemMaterial;
            }
            else
            {
                //2枚目：生成するオブジェクトの位置を設定する
                pos_CPU = new Vector3(1.3f, 1.3f, 0.71f);
                CPUManager01.itemNumber_c2 = CardManager01.numItemMaterial;
            }
            
            //設定した位置にプレハブを生成する
            ItemCards_CPU = Instantiate(ItemCard, pos_CPU, Quaternion.Euler(0, 0, 0));
            //プレハブのマテリアルを設定する
            ItemCards_CPU.GetComponent<MeshRenderer>().material = MaterialSet[CardManager01.numItemMaterial];
            //アイテムの番号を記憶する
            ItemCards_CPU.GetComponent<ItemEffects01>().SaveItemNumber();
            //CPUのアイテムとして設定する
            ItemCards_CPU.GetComponent<ItemEffects01>().JudgeIsCPUsCard();
            //プレハブをリストに追加する
            ItemList.Add(ItemCards_CPU);
        }
    }
}