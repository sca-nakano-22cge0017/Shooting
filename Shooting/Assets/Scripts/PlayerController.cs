using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Image[] HP;
    bool isDamage, invincible;
    enum STATE { WAIT = 0, PLAY, GAMEOVER, CLEAR, };
    STATE state = 0;

    [SerializeField] GameManager gameManager;

    void Start()
    {
        isDamage = false;
        invincible = false;
    }

    void Update()
    {
        switch(state) {
            case STATE.WAIT:
                WAIT();
                break;
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

    void WAIT() {

    }

    void PLAY() //操作可能状態での処理
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

        if(isDamage) {
            StartCoroutine("Damage");
            gameManager.MinusHp();
            isDamage = false;
        }
    }

    void GAMEOVER() {
        float x = Mathf.Sin(Time.time * 10f) * 0.001f;
        this.transform.position -= new Vector3(x, speed * Time.deltaTime, 0);
    }

    void CLEAR() {
        this.transform.position += new Vector3(0, speed * Time.deltaTime * 1.3f, 0);
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
        if((other.gameObject.tag == "Rock" || other.gameObject.tag == "Viran" || other.gameObject.tag == "Viran's Bullet") && !invincible) {
            isDamage = true;
        }

        if(other.gameObject.tag == "Recovery") {
            gameManager.PlusHp();
        }
    }

    IEnumerator Damage() {
        for (int i = 0; i < 3; i++) {
            invincible = true;
            GetComponent<SpriteRenderer>().color = new Color (0, 0, 0, 0.2f);
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1.0f);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.5f);
        invincible = false;
    }
}
