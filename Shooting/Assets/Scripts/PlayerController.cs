using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤー制御
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("移動速度")] float speed;

    [SerializeField] Image[] HP;

    bool isDamage, invincible;
    enum STATE { WAIT = 0, PLAY, GAMEOVER, CLEAR, };
    STATE state = 0;

    [SerializeField] GameManager gameManager;

    [Header("ゲームオーバー演出")]
    [SerializeField, Header("揺れの速度")] float cycleSpeed = 10;
    [SerializeField, Header("揺れ幅")] float amplitude = 0.001f;

    void Start()
    {
        isDamage = false;
        invincible = false;
    }

    void Update()
    {
        switch(state) {
            case STATE.PLAY:
                PLAY();
                break;
            case STATE.GAMEOVER:
                GAMEOVER();
                break;
            case STATE.CLEAR:
                CLEAR();
                break;
        }
    }

    //操作可能状態での処理
    void PLAY()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        if(Input.GetKey(KeyCode.W) && pos.y <= 4.5) {
            pos.y += speed * Time.deltaTime;
            myTransform.position = pos;
        }
        if(Input.GetKey(KeyCode.S) && pos.y >= -4.5) {
            pos.y -= speed * Time.deltaTime;
            myTransform.position = pos;
        }
        if(Input.GetKey(KeyCode.A) && pos.x >= -3.0) {
            pos.x -= speed * Time.deltaTime;
            myTransform.position = pos;
        }
        if(Input.GetKey(KeyCode.D) && pos.x <= 3.0) {
            pos.x += speed * Time.deltaTime;
            myTransform.position = pos;
        }

        //ダメージを負ったら
        if(isDamage) {
            StartCoroutine("Damage"); //ダメージ演出
            gameManager.MinusHp(); //HP減少
            isDamage = false;
        }
    }

    //ゲームオーバー演出
    void GAMEOVER() {
        float x = Mathf.Sin(Time.time * cycleSpeed) * amplitude;
        this.transform.position -= new Vector3(x, speed * Time.deltaTime, 0);
    }

    //クリア演出
    void CLEAR() {
        float clearSpeed = 1.3f;
        this.transform.position += new Vector3(0, speed * Time.deltaTime * clearSpeed, 0);
    }

    //他スクリプトからSTATEを変更する関数
    public void Play() { 
        state = STATE.PLAY;
    }

    public void GameOver() {
        state = STATE.GAMEOVER;
    }

    public void Clear() {
        state = STATE.CLEAR;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //岩や敵にぶつかったらダメージ
        if((other.gameObject.tag == "Rock" || other.gameObject.tag == "Viran" || other.gameObject.tag == "Viran's Bullet") && !invincible) {
            isDamage = true;
        }

        //回復アイテムに触れたら回復
        if(other.gameObject.tag == "Recovery") {
            gameManager.PlusHp();
        }
    }

    /// <summary>
    /// ダメージ演出
    /// </summary>
    IEnumerator Damage() {
        for (int i = 0; i < 3; i++) {
            invincible = true;　//無敵状態にする

            //点滅
            GetComponent<SpriteRenderer>().color = new Color (0, 0, 0, 0.2f);
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1.0f);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.5f);
        invincible = false;　//無敵解除
    }
}
