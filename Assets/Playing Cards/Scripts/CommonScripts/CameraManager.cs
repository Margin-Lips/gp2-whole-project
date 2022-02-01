using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 pos, pos_pre, pos_cur;
    float start;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        pos_pre = transform.position;
        pos_cur.Set(0, 3.2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //アイテム選択が完了したら
        if(CardManager.isGameStarted)
        {
            start = Time.time;
            isMoving = true;

            CardManager.isGameStarted = false;
        }
        if(CardManager01.isGameStarted)
        {
            start = Time.time;
            isMoving = true;

            CardManager01.isGameStarted = false;
        }

        if(isMoving)
        {
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        float r = (Time.time - start) / 1.2f;
        pos = r*pos_cur + (1.0f-r)*pos_pre;

        if(r > 1.0f)
        {
            pos = new Vector3(0, 3.2f, 0);
            isMoving = false;
        }

        transform.position = pos;
    }
}
