using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject itemSelect_Panel;
    [SerializeField] GameObject systemText_Panel;

    // Start is called before the first frame update
    void Start()
    {
        itemSelect_Panel.SetActive(true);
        systemText_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //アイテムの選択が完了したら
        if(CardManager.isItemSelected || CardManager01.isItemSelected)
        {
            itemSelect_Panel.SetActive(false);
            systemText_Panel.SetActive(true);
        }
    }
}
