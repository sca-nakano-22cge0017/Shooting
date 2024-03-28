using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 岩　制御
/// </summary>
public class RockController : MonoBehaviour
{
    float transformSpeed, rotateSpeed;
    float desPos = -7f;

    void Start()
    {
        transformSpeed = 3;
        rotateSpeed = 90;
    }

    void Update()
    {
        //y軸移動
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        pos.y -= transformSpeed * Time.deltaTime;
        myTransform.position = pos;

        //回転移動
        myTransform.Rotate(0, 0, rotateSpeed * Time.deltaTime, Space.World);

        //画面外で削除
        if(pos.y <= desPos) {
            Destroy(gameObject);
        }
    }
}
