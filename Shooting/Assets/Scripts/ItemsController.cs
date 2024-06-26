using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム制御
/// </summary>
public class ItemsController : MonoBehaviour
{
    float speed = 2; //移動速度
    [SerializeField, Header("最小サイズ")] float min;
    [SerializeField, Header("拡縮の振幅")] float amplitude = 0.05f;
    [SerializeField, Header("拡縮の速度")] float cycleSpeed = 3f;

    void Update()
    {
        //移動
        this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        //拡縮
        float scale = amplitude * Mathf.Sin(Time.time * cycleSpeed) + min;
        this.transform.localScale = new Vector3(scale, scale, 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //プレイヤーに触れたら消える
        if(other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
