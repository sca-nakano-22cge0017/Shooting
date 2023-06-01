using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //ƒvƒŒƒCƒ„[
    public GameObject Player;

    //áŠQ•¨
    [SerializeField] GameObject[] Rock;
    [SerializeField, Header("RockPosition")] float maxRockPos, minRockPos;
    bool rockCreate;
    int num = 0;
    float size, rockPos;

    //“G
    [SerializeField] GameObject Viran;
    [SerializeField, Header("ViranPosition")] float maxViranPos, minViranPos;
    bool viranCreate;
    float viranPos;

    //’e
    [SerializeField] GameObject Bullet;
    float bulletTime;

    //ƒJƒEƒ“ƒgƒ_ƒEƒ“
    [SerializeField] Text countDown;

    //ƒS[ƒ‹
    [SerializeField] Image Goal;
    [SerializeField] float playTime = 60;
    float time;

    enum STATE { WAIT = 0, PLAY, GAMEOVER, CLEAR, };
    STATE state = 0;
    bool start;

    void Start()
    {
        state = STATE.WAIT;
        start = false;
        StartCoroutine("CountDown");

        rockCreate = false;
        viranCreate = false;
        StartCoroutine("RockCoolTime");
        StartCoroutine("ViranCoolTime");

        Goal.enabled = false;
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
                break;
            case STATE.GAMEOVER:
                break;
        }
    }

    void WAIT()
    {
    }

    void PLAY()
    {
        //Šâ‚Ì¶¬
        if (rockCreate)
        {
            num = Random.Range(0, Rock.Length);
            rockPos = Random.Range(minRockPos, maxRockPos);
            GameObject inst = Instantiate(Rock[num], new Vector3(rockPos, 5, 0), Quaternion.identity);
            inst.layer = LayerMask.NameToLayer("Default");
            rockCreate = false;
            StartCoroutine("RockCoolTime");
        }

        //“G‚Ì¶¬
        if (viranCreate)
        {
            viranPos = Random.Range(minViranPos, maxViranPos);
            Instantiate(Viran, new Vector3(viranPos, 5, 0), Quaternion.identity);
            viranCreate = false;
            StartCoroutine("ViranCoolTime");
        }

        //’e‚Ì¶¬
        bulletTime += Time.deltaTime;
        if (bulletTime >= 0.5f)
        {
            Transform playerPos = Player.GetComponent<Transform>();
            Vector3 bulletPos = playerPos.position;

            Instantiate(Bullet, bulletPos, Quaternion.identity);
            bulletTime = 0f;
        }

        //ƒNƒŠƒA”»’è 
        time += Time.deltaTime;
        if (time >= playTime)
        {
            state = STATE.CLEAR;
        }
    }

    void CLEAR()
    {
        Goal.enabled = true;
        Goal.transform.position -= new Vector3(0, 5, 0);

        if(Goal.transform.position.y <= -5)
        {
            SceneManager.LoadScene("ResultScene");
        }
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
        start = true;
        state = STATE.PLAY;
    }
}
