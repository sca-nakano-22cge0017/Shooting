using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �v���C���[����
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("�ړ����x")] float speed;

    [SerializeField] Image[] HP;

    bool isDamage, invincible;
    enum STATE { WAIT = 0, PLAY, GAMEOVER, CLEAR, };
    STATE state = 0;

    [SerializeField] GameManager gameManager;

    [Header("�Q�[���I�[�o�[���o")]
    [SerializeField, Header("�h��̑��x")] float cycleSpeed = 10;
    [SerializeField, Header("�h�ꕝ")] float amplitude = 0.001f;

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

    //����\��Ԃł̏���
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

        //�_���[�W�𕉂�����
        if(isDamage) {
            StartCoroutine("Damage"); //�_���[�W���o
            gameManager.MinusHp(); //HP����
            isDamage = false;
        }
    }

    //�Q�[���I�[�o�[���o
    void GAMEOVER() {
        float x = Mathf.Sin(Time.time * cycleSpeed) * amplitude;
        this.transform.position -= new Vector3(x, speed * Time.deltaTime, 0);
    }

    //�N���A���o
    void CLEAR() {
        float clearSpeed = 1.3f;
        this.transform.position += new Vector3(0, speed * Time.deltaTime * clearSpeed, 0);
    }

    //���X�N���v�g����STATE��ύX����֐�
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
        //���G�ɂԂ�������_���[�W
        if((other.gameObject.tag == "Rock" || other.gameObject.tag == "Viran" || other.gameObject.tag == "Viran's Bullet") && !invincible) {
            isDamage = true;
        }

        //�񕜃A�C�e���ɐG�ꂽ���
        if(other.gameObject.tag == "Recovery") {
            gameManager.PlusHp();
        }
    }

    /// <summary>
    /// �_���[�W���o
    /// </summary>
    IEnumerator Damage() {
        for (int i = 0; i < 3; i++) {
            invincible = true;�@//���G��Ԃɂ���

            //�_��
            GetComponent<SpriteRenderer>().color = new Color (0, 0, 0, 0.2f);
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1.0f);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.5f);
        invincible = false;�@//���G����
    }
}
