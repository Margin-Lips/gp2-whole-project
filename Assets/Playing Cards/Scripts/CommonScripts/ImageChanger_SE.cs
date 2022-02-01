using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger_SE : MonoBehaviour
{
    public Sprite muteSprite;
    public Sprite unmuteSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SoundsManager.isSoundMuted)
        {
            this.GetComponent<Image>().sprite = muteSprite;
        }
        else
        {
            this.GetComponent<Image>().sprite = unmuteSprite;
        }
    }
}
