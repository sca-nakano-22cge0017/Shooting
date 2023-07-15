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
    [SerializeField] GameObject[] Viran;
    [SerializeField, Header("ViranPosition")] float maxViranPos, minViranPos;
    bool viranCreate;
    float viranPos;
    int viranNum;
    int probability;
    [SerializeField, Header("ViranOccurrenceProbability")] int threeDirection, twoDirection, highSpeed;

    //ボス
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossHPBar;
    [SerializeField] Image bossHP;
    bool isBoss, bossDamage;

    //弾
    [SerializeField] GameObject Bullet;
    float bulletTime;

    //HP
    [SerializeField] Image[] HP;
    public static int hp;
    bool minusHp, plusHp;

    //カウントダウン
    [SerializeField] Text countDown;

    //ゴール
    [SerializeField] Image Goal;
    [SerializeField] float playTime = 60;
    float time;
    bool clear;

    //ゲームオーバー
    [SerializeField] Text GameOverText;

    //敵撃破数
    public static int killScore;
    bool kill;
    [SerializeField] Text KillScore;
 
    enum STATE { WAIT = 0, PLAY, GAMEOVER, CLEAR, };
    STATE state = 0;
    bool sceneChange;

    void Start()
    {
        killScore = 0;
        kill = false;
        clear = false;
        isBoss = false;
        bossDamage = false;

        state = STATE.WAIT;
        sceneChange = false;
        StartCoroutine("CountDown");

        rockCreate = false;
        viranCreate = false;
        itemCreate = false;
        StartCoroutine("RockCoolTime");
        StartCoroutine("ViranCoolTime");
        StartCoroutine("ItemCoolTime");

        boss.SetActive(false);
        bossHPBar.SetActive(false);

        Goal.enabled = false;
        GameOverText.enabled = false;

        minusHp = false;
        plusHp = false;
        hp = HP.Length;
    }

    void Update()
    {
        KillScore.text = killScore + "";

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
        if (viranCreate && !isBoss)
        {
            viranPos = Random.Range(minViranPos, maxViranPos);
            probability = Random.Range(1, 100);

            if(probability <= threeDirection) { viranNum = 0; }
            else if (probability > threeDirection && probability <= twoDirection) { viranNum = 1; }
            else if(probability > twoDirection && probability <= highSpeed) { viranNum = 2; } 

            Instantiate(Viran[viranNum], new Vector3(viranPos, 8, 0), Quaternion.identity);
            viranCreate = false;
            StartCoroutine("ViranCoolTime");
        }

        //回復アイテム生成
        if(itemCreate && hp < 3) {
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
        if(minusHp && hp > 0) {
            hp--;
            HP[hp].GetComponent<Image>().color = new Color(0, 0, 0, 255);
            minusHp = false;
        }

        if(plusHp && hp < 3) {
            hp++;
            HP[hp - 1].GetComponent<Image>().color = new Color(255, 0, 0, 255);
            plusHp = false;
        }

        if(hp <= 0) hp = 0;
        if(hp >= 3) hp = 3;

        //敵撃破数集計
        if(kill) {
            ++killScore;
            kill = false;
        }

        //ゲームオーバー判定
        if(hp <= 0)
        {
            state = STATE.GAMEOVER;
            playerController.GameOver();
            StartCoroutine("GameOver");
        }

        //ボス判定
        time += Time.deltaTime;
        if(time >= playTime) {
            isBoss = true;
            BOSS();
        }
    }

    void BOSS() {
        boss.SetActive(true);
        bossHPBar.SetActive(true);
        
        if(boss.transform.localPosition.y >= 0.5f) {
            boss.transform.localPosition += new Vector3(0, -2.2f, 0) * Time.deltaTime;
        }
        if(bossHPBar.transform.localPosition.y >= 250) {
            bossHPBar.transform.localPosition += new Vector3(0, -150, 0) * Time.deltaTime;
        }
        
        if(bossDamage) {
            bossHP.fillAmount -= 0.1f;
            bossDamage = false;
        }
        if(bossHP.fillAmount <= 0) {
            bossHPBar.SetActive(false);
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
        bossHPBar.SetActive(false);
    }

    public void MinusHp() {
        minusHp = true;
    }

    public void PlusHp() {
        plusHp = true;
    }

    public void BossMinusHP() {
        bossDamage = true;
    }

    public void Kill() {
        kill = true;
    }

    public void Clear() {
        clear = true;
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

    IEnumerator GameOver() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("TitleScene");
    }
}