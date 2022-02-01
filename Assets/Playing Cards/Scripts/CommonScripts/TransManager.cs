using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransManager : MonoBehaviour
{
    float speed = 0.008f;
    float r, g, b, alpha;
    bool isTransCompleted;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Image>().color.r;
        g = GetComponent<Image>().color.g;
        b = GetComponent<Image>().color.b;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTransCompleted){
            GetComponent<Image>().color = new Color(r, g, b, 1 - alpha);
            alpha += speed;

            if(alpha >= 1){
                Destroy(this.gameObject);
            }
        }
    }
}
