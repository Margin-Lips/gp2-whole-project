using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static int displayNumber = 5;
    public static bool isInstDisplayed = false;
    bool isInstFirstPage = true;
    
    [SerializeField] GameObject firstSet_Panel;
    [SerializeField] GameObject description_Panel;
    [SerializeField] GameObject description_Card;
    [SerializeField] GameObject description_Items;
    [SerializeField] GameObject instruction_Panel;
    [SerializeField] GameObject instruction_Button;

    // マテリアル格納用
    public Material[] MaterialSet = new Material[5];

    public GameObject nameObject;
    public GameObject detailObject;

    public GameObject discObject_1;
    public GameObject discObject_2;
    public GameObject moveObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text nameText = nameObject.GetComponent<Text>();
        Text detailText = detailObject.GetComponent<Text>();

        Text discText_1 = discObject_1.GetComponent<Text>();
        Text discText_2 = discObject_2.GetComponent<Text>();
        Text moveText = moveObject.GetComponent<Text>();

        if(isInstDisplayed)
        {
            firstSet_Panel.SetActive(false);
            description_Panel.SetActive(false);
            description_Card.SetActive(false);
            description_Items.SetActive(false);
            instruction_Panel.SetActive(true);

            //説明画面からメニュー画面へ戻る
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isInstDisplayed = false;
            }

            //説明画面のページを切り替える
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(isInstFirstPage)
                {
                    isInstFirstPage = false;
                }
                else
                {
                    isInstFirstPage = true;
                }
            }

            if(isInstFirstPage)
            {
                //説明画面：ゲームルールの説明
                discText_1.text = "BJ GAME：'BlackJack'を基にしたアイテム追加ver.\n　　　　  アイテムを駆使してより攻撃的なBJを";
                discText_2.text = "01 GAME：ダーツの'01'を基にした BJ の長期戦ver.\n　　　　　 15ターン以内に相手より早く101JUSTに";
                moveText.text = "-NEXT(ENTER)-";
                instruction_Button.SetActive(false);
            }
            else
            {
                //説明画面：操作の説明
                discText_1.text = "OPERATION：HIT/STAND操作 & アイテム操作='CLICK'\n　　　　　　  ターン終了='SPACE', 画面遷移='ENTER'";
                discText_2.text = "TURN FLOW：HIT→アイテム使用/×→STAND/×→終了\n　　　　  ※01 GAMEの際は'2HIT-1SET'となる";
                moveText.text = "-PREV(ENTER)-";
                instruction_Button.SetActive(true);
            }
        }
        else
        {
            if(displayNumber != 5)
            {
                firstSet_Panel.SetActive(false);
                description_Panel.SetActive(true);
                description_Card.SetActive(true);
                description_Items.SetActive(true);
                instruction_Panel.SetActive(false);
                instruction_Button.SetActive(false);

                //表示アイテムと効果説明を切り替える
                if(displayNumber == 0)
                {
                    //Debug.Log("limit");
                    nameText.text = "-LIMIT CARD-";
                    detailText.text = "次のターン、HITした際に出てくる\n数字がA(1)~5のいずれかになる";
                }
                else if(displayNumber == 1)
                {
                    //Debug.Log("half");
                    nameText.text = "-HALF CARD-";
                    detailText.text = "そのターンに引いたカードの数字を\n半分にする(1=1,小数点以下切り捨て)";
                }
                else if(displayNumber == 2)
                {
                    //Debug.Log("double");
                    nameText.text = "-DOUBLE CARD-";
                    detailText.text = "そのターンに引いたカードの数字を\n2倍にする(数字が6以下の場合のみ)";
                }
                else if(displayNumber == 3)
                {
                    //Debug.Log("origin");
                    nameText.text = "-ORIGIN CARD-";
                    detailText.text = "そのターンに引いた絵札の数字を\n数札に変換する(J=11,Q=12,K=13)";
                }
                else if(displayNumber == 4)
                {
                    //Debug.Log("digits");
                    nameText.text = "-DIGITS CARD-";
                    detailText.text = "次のターン、HITした際に出てくる\n数字が10~K(13)のいずれかになる";
                }

                description_Card.GetComponent<MeshRenderer>().material = MaterialSet[displayNumber];
            }
            else
            {
                //メニュー画面：初期表示
                firstSet_Panel.SetActive(true);
                description_Panel.SetActive(false);
                description_Card.SetActive(false);
                description_Items.SetActive(true);
                instruction_Panel.SetActive(false);
                instruction_Button.SetActive(false);
            }
        }
    }
}
