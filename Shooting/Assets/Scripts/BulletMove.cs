using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾制御
/// </summary>
public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed;
    float angle;
    Vector3 vec;

    void Start()
    {
        angle = this.transform.rotation.z; //Inspectorから角度を取得
        Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0); //角度からベクトル算出
        vec = direction * speed * Time.deltaTime;
    }

    void Update()
    {
        //移動
        transform.position += vec;

        //弾削除
        if(this.transform.position.y >= 4.5)
        {
            Destroy(gameObject);
        }
        if(this.gameObject.tag == "Viran's Bullet") {
            if(this.transform.position.y <= -5) {
                Destroy(gameObject);
            }
        }
    }
}
