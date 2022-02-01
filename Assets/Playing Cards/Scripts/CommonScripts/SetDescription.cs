using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDescription : MonoBehaviour
{
    //生成するプレハブの親要素
    public GameObject ItemHolder;
    //生成するプレハブ格納用
    public GameObject ItemCard;
    //マテリアル格納用
    public Material[] MaterialSet = new Material[5];

    public static int descItemNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<5; i++)
        {
            //生成するオブジェクトの位置
            Vector3 pos = new Vector3((i-2.0f)*0.25f, 1.3f, -0.33f);
            //プレハブを生成
            GameObject ItemCards = Instantiate(ItemCard, pos, Quaternion.Euler(0, 0, 0));
            ItemCards.transform.parent = ItemHolder.transform;
            //プレハブのマテリアルを設定
            ItemCards.GetComponent<MeshRenderer>().material = MaterialSet[i];
            //アイテムの番号を記憶
            descItemNumber = i;
            ItemCards.GetComponent<MenuCardManager>().SaveItemNumber();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
