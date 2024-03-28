using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Šâ@§Œä
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
        //y²ˆÚ“®
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        pos.y -= transformSpeed * Time.deltaTime;
        myTransform.position = pos;

        //‰ñ“]ˆÚ“®
        myTransform.Rotate(0, 0, rotateSpeed * Time.deltaTime, Space.World);

        //‰æ–ÊŠO‚Åíœ
        if(pos.y <= desPos) {
            Destroy(gameObject);
        }
    }
}
