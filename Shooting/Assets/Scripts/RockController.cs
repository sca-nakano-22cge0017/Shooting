using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    float transformSpeed, rotateSpeed;

    void Start()
    {
        transformSpeed = 3;
        rotateSpeed = 90;
    }

    void Update()
    {
        //y���ړ�
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        pos.y -= transformSpeed * Time.deltaTime;
        myTransform.position = pos;

        //��]�ړ�
        myTransform.Rotate(0, 0, rotateSpeed * Time.deltaTime, Space.World);

        if(pos.y <= -7) {
            Destroy(gameObject);
        }
    }
}
