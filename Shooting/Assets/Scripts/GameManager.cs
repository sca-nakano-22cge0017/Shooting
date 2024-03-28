using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ���C���Q�[������
/// </summary>
public class GameManager : MonoBehaviour
{
    //�v���C���[
    public GameObject Player;
    [SerializeField] PlayerController playerController;

    [SerializeField, Header("�I�u�W�F�N�g�����ʒu�@Y���W")] float CreatePosY = 8;

    //��Q��
    [SerializeField] GameObject[] Rock;
    [SerializeField, Header("RockPosition")] float maxRockPos, minRockPos;
    bool rockCreate;
    int num = 0;
    float size, rockPos;

    //�A�C�e��
    [SerializeField] GameObject Items;
    bool itemCreate;
    float itemPos;
    [SerializeField] float itemCrePosMin = -3;
    [SerializeField] float itemCrePosMax = 3;

    //�G
    [SerializeField] GameObject[] Viran;
    [SerializeField, Header("ViranPosition")] float maxViranPos, minViranPos;
    bool viranCreate;
    float viranPos;
    int viranNum;
    int probability;
    [SerializeField, Header("ViranOccurrenceProbability")] int threeDirection, twoDirection, highSpeed;

    //�{�X
    [SerializeField] GameObject boss;
    [SerializeField] GameObject bossHPBar;
    [SerializeField] Image bossHP;
    bool isBoss, bossDamage;

    //�e
    [SerializeField] GameObject Bullet;
    float bulletTime;
    [SerializeField] float bulletsSpan = 0.5f;

    //HP
    [SerializeField] Image[] HP;
    public static int hp;
    bool minusHp, plusHp;

    //�J�E���g�_�E��
    [SerializeField] Text countDown;

    //�S�[��
    [SerializeField] Image Goal;
    [SerializeField] float playTime = 60;
    float time;
    bool clear;

    //�Q�[���I�[�o�[
    [SerializeField] Text GameOverText;

    //�G���j��
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
        KillScore.text = killScore.ToString();

        switch(state)
        {
            case STATE.WAIT:
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

    void PLAY()
    {
        //��̐���
        if (rockCreate)
        {
            num = Random.Range(0, Rock.Length);
            rockPos = Random.Range(minRockPos, maxRockPos);

            Instantiate(Rock[num], new Vector3(rockPos, CreatePosY, 0), Quaternion.identity);

            rockCreate = false;
            StartCoroutine("RockCoolTime");
        }

        //�G�̐���
        if (viranCreate && !isBoss)
        {
            viranPos = Random.Range(minViranPos, maxViranPos);
            probability = Random.Range(1, 100);

            //probability�̐��l�ɂ���Đ�������G��ς���
            if(probability <= threeDirection) { viranNum = 0; }
            else if (probability > threeDirection && probability <= twoDirection) { viranNum = 1; }
            else if(probability > twoDirection && probability <= highSpeed) { viranNum = 2; } 

            Instantiate(Viran[viranNum], new Vector3(viranPos, CreatePosY, 0), Quaternion.identity);
            viranCreate = false;
            StartCoroutine("ViranCoolTime");
        }

        //�񕜃A�C�e������
        if(itemCreate && hp < 3) {
            
            itemPos = Random.Range(itemCrePosMin, itemCrePosMax);
            Instantiate(Items, new Vector3(itemPos, CreatePosY, 0), Quaternion.identity);
            itemCreate = false;
            StartCoroutine("ItemCoolTime");
        }

        //�e�̐���
        bulletTime += Time.deltaTime;
        if (bulletTime >= bulletsSpan) //���Ԋu�Œe�𐶐�
        {
            Transform playerPos = Player.GetComponent<Transform>();
            Vector3 bulletPos = playerPos.position;

            Instantiate(Bullet, bulletPos, Quaternion.identity);
            bulletTime = 0f;
        }

        //HP����
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

        //�G���j���W�v
        if(kill) {
            ++killScore;
            kill = false;
        }

        //�Q�[���I�[�o�[����
        if(hp <= 0)
        {
            state = STATE.GAMEOVER;
            playerController.GameOver();
            StartCoroutine("GameOver");
        }

        //�{�X���� ��莞�Ԍo������{�X��˓�
        time += Time.deltaTime;
        if(time >= playTime) {
            isBoss = true;
            BOSS();
        }
    }

    /// <summary>
    /// �{�X�퐧��
    /// </summary>
    void BOSS() {
        boss.SetActive(true);
        bossHPBar.SetActive(true);
        
        //�{�X�o��
        if(boss.transform.localPosition.y >= 0.5f) {
            boss.transform.localPosition += new Vector3(0, -2.2f, 0) * Time.deltaTime;
        }
        //�{�XHP�\��
        if(bossHPBar.transform.localPosition.y >= 250) {
            bossHPBar.transform.localPosition += new Vector3(0, -150, 0) * Time.deltaTime;
        }
        
        //�{�X�_���[�W����
        if(bossDamage) {
            bossHP.fillAmount -= 0.1f;
            bossDamage = false;
        }

        //�{�X��HP��0�ɂȂ�����N���A
        if(bossHP.fillAmount <= 0) {
            bossHPBar.SetActive(false);
            state = STATE.CLEAR;
        }
    }

    float goalSpeed = 300;

    /// <summary>
    /// �N���A����
    /// </summary>
    void CLEAR()
    {
        Goal.enabled = true; //�S�[���\��

        if(Goal.transform.position.y >= 600)
        {
            Goal.transform.position -= new Vector3(0, goalSpeed * Time.deltaTime, 0);
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

    /// <summary>
    /// HP����
    /// </summary>
    public void MinusHp() {
        minusHp = true;
    }

    /// <summary>
    /// HP����
    /// </summary>
    public void PlusHp() {
        plusHp = true;
    }

    /// <summary>
    /// �{�XHP����
    /// </summary>
    public void BossMinusHP() {
        bossDamage = true;
    }

    /// <summary>
    /// �G���G����
    /// </summary>
    public void Kill() {
        kill = true;
    }

    /// <summary>
    /// �N���A
    /// </summary>
    public void Clear() {
        clear = true;
    }

    //�����N�[���^�C��
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

    /// <summary>
    /// �J�E���g�_�E��
    /// </summary>
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

        //�Q�[���J�n
        state = STATE.PLAY;
        playerController.Play();
    }

    /// <summary>
    /// �V�[���J��
    /// </summary>
    IEnumerator SceneChange() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ResultScene");
        sceneChange = false;
    }

    /// <summary>
    /// �Q�[���I�[�o�[���̃V�[���J��
    /// </summary>
    IEnumerator GameOver() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("TitleScene");
    }
}