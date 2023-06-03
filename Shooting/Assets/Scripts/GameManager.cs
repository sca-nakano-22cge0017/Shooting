using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //プレイヤー
    public GameObject Player;
    [SerializeField] PlayerController playerController;

    //障害物
    [SerializeField] GameObject[] Rock;
    [SerializeField, Header("RockPosition")] float maxRockPos, minRockPos;
    bool rockCreate;
    int num = 0;
    float size, rockPos;

    //アイテム
    [SerializeField] GameObject Items;
    bool itemCreate;
    float itemPos;

    //敵
    [SerializeField] GameObject Viran;
    [SerializeField, Header("ViranPosition")] float maxViranPos, minViranPos;
    bool viranCreate;
    float viranPos;

    //弾
    [SerializeField] GameObject Bullet;
    float bulletTime;

    //HP
    [SerializeField] Image[] HP;
    int hp;
    bool minusHp, plusHp;

    //カウントダウン
    [SerializeField] Text countDown;

    //ゴール
    [SerializeField] Image Goal;
    [SerializeField] float playTime = 60;
    float time;

    //ゲームオーバー
    [SerializeField] Text GameOverText;
 
    enum STATE { WAIT = 0, PLAY, GAMEOVER, CLEAR, };
    STATE state = 0;
    bool sceneChange;

    void Start()
    {
        state = STATE.WAIT;
        sceneChange = false;
        StartCoroutine("CountDown");

        rockCreate = false;
        viranCreate = false;
        itemCreate = false;
        StartCoroutine("RockCoolTime");
        StartCoroutine("ViranCoolTime");
        StartCoroutine("ItemCoolTime");

        Goal.enabled = false;
        GameOverText.enabled = false;

        minusHp = false;
        plusHp = false;
        hp = HP.Length;
    }

    void Update()
    {
        switch(state)
        {
            case STATE.WAIT:
                WAIT();
                break;
            case STATE.PLAY:
                PLAY();
                break;
            case STATE.CLEAR:
                CLEAR();
                break;
            case STATE.GAMEOVER:
                GAMEOVER();
                break;
        }
    }

    void WAIT()
    {
    }

    void PLAY()
    {
        //岩の生成
        if (rockCreate)
        {
            num = Random.Range(0, Rock.Length);
            rockPos = Random.Range(minRockPos, maxRockPos);
            GameObject inst = Instantiate(Rock[num], new Vector3(rockPos, 8, 0), Quaternion.identity);
            inst.layer = LayerMask.NameToLayer("Default");
            rockCreate = false;
            StartCoroutine("RockCoolTime");
        }

        //敵の生成
        if (viranCreate)
        {
            viranPos = Random.Range(minViranPos, maxViranPos);
            Instantiate(Viran, new Vector3(viranPos, 8, 0), Quaternion.identity);
            viranCreate = false;
            StartCoroutine("ViranCoolTime");
        }

        //アイテム生成
        if(itemCreate) {
            itemPos = Random.Range(-3, 3);
            Instantiate(Items, new Vector3(itemPos, 8, 0), Quaternion.identity);
            itemCreate = false;
            StartCoroutine("ItemCoolTime");
        }

        //弾の生成
        bulletTime += Time.deltaTime;
        if (bulletTime >= 0.5f)
        {
            Transform playerPos = Player.GetComponent<Transform>();
            Vector3 bulletPos = playerPos.position;

            Instantiate(Bullet, bulletPos, Quaternion.identity);
            bulletTime = 0f;
        }

        //HP処理
        if(minusHp) {
            HP[hp - 1].GetComponent<Image>().color = new Color(0, 0, 0, 255);
            hp--;
            minusHp = false;
        }

        if(plusHp) {
            HP[hp - 1].GetComponent<Image>().color = new Color(255, 0, 0, 255);
            hp++;
            plusHp = false;
        }

        if(hp >= 3) hp = 3;
        if(hp <= 0) hp = 0;

        //ゲームオーバー判定
        if(hp <= 0)
        {
            state = STATE.GAMEOVER;
            playerController.GameOver();
        }

        //クリア判定 
        time += Time.deltaTime;
        if (time >= playTime)
        {
            state = STATE.CLEAR;
        }
    }

    void CLEAR()
    {
        Goal.enabled = true;
        if(Goal.transform.position.y >= 600)
        {
            Goal.transform.position -= new Vector3(0, 300 * Time.deltaTime, 0);
        }
        else if(Goal.transform.position.y <= 600) {
            playerController.Clear();
            sceneChange = true;
        }

        if(sceneChange) {
            StartCoroutine("SceneChange");
        }
    }

    void GAMEOVER()
    {
        GameOverText.enabled = true;
    }

    public void MinusHp() {
        minusHp = true;
    }

    public void PlusHp() {
        plusHp = true;
    }

    IEnumerator RockCoolTime()
    {
        yield return new WaitForSeconds(2);
        rockCreate = true;
    }

    IEnumerator ViranCoolTime()
    {
        yield return new WaitForSeconds(3);
        viranCreate = true;
    }

    IEnumerator ItemCoolTime() {
        yield return new WaitForSeconds(5);
        itemCreate = true;
    }

    IEnumerator CountDown()
    {
        for (int i = 3; i > 0; i--)
        {
            countDown.text = "" + i;
            yield return new WaitForSeconds(1);
        }
        countDown.text = "START!!";
        yield return new WaitForSeconds(1);
        countDown.enabled = false;
        state = STATE.PLAY;
        playerController.Play();
    }

    IEnumerator SceneChange() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ResultScene");
        sceneChange = false;
    }
}
