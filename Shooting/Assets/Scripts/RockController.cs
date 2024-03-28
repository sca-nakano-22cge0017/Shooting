using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��@����
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
        //y���ړ�
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        pos.y -= transformSpeed * Time.deltaTime;
        myTransform.position = pos;

        //��]�ړ�
        myTransform.Rotate(0, 0, rotateSpeed * Time.deltaTime, Space.World);

        //��ʊO�ō폜
        if(pos.y <= desPos) {
            Destroy(gameObject);
        }
    }
}
