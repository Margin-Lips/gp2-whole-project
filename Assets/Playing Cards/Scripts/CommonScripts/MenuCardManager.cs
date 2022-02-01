using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCardManager : MonoBehaviour
{
    //マテリアル格納用
    public Material[] MaterialSet = new Material[6];

    int itemNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //選択されている間は"BACK CARD"に切り替える
        if(MenuManager.displayNumber == itemNumber)
        {
            GetComponent<MeshRenderer>().material = MaterialSet[5];
        }
        else
        {
            GetComponent<MeshRenderer>().material = MaterialSet[itemNumber];
        }
        
    }

    public void SaveItemNumber()
    {
        itemNumber = SetDescription.descItemNumber;
    }

    public void onClickItemAct()
    {
        //表示中のアイテムと異なる場合は切り替える
        if(MenuManager.displayNumber != itemNumber)
        {
            MenuManager.displayNumber = itemNumber;
        }
        else
        {
            MenuManager.displayNumber = 5;
        }
    }
}
