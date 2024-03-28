using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// ボス制御
/// </summary>
public class BossController : MonoBehaviour
{
    float speed;
    int shots;
    float angle, correction;
    float coolTime;
    float launchAngle;
    [SerializeField] GameObject Bullet;
    float bulletTime = 0;
    [SerializeField] GameObject particleObject;
    [SerializeField] StatuaData[] status;
    int num;
    int hitCount;

    GameObject MainManager;

    public void Start() {
        num = Random.Range(0, status.Length);

        speed = status[num].Speed;
        shots = status[num].Shots;
        angle = status[num].Angle;
        correction = status[num].Correction;
        coolTime = status[num].CoolTime;
        hitCount = 0;

        MainManager = GameObject.Find("MainManager");
    }

    public void Update() {
        this.transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);

        //ステータスをランダムで変更
        num = Random.Range(0, status.Length);
        speed = status[num].Speed;
        shots = status[num].Shots;
        angle = status[num].Angle;
        coolTime = status[num].CoolTime;
        correction = status[num].Correction;

        //弾生成
        bulletTime += Time.deltaTime;
        if(bulletTime >= coolTime) {
            Transform ViranPos = this.GetComponent<Transform>();
            Vector3 bulletPos = ViranPos.localPosition;

            for(int i = -1; i < shots - 1; i++) {
                if(shots % 2 == 0) {
                    launchAngle = i * angle + angle / 2;
                } else { launchAngle = i * angle; }
                Instantiate(Bullet, bulletPos, Quaternion.Euler(0, 0, launchAngle + correction));
            }
            bulletTime = 0f;
        }

        //ボス討伐
        if(hitCount >= 10) {
            for (int i = 0; i <= 1; i++) {
                Instantiate(particleObject, this.transform.localPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Bullet") {
            MainManager.GetComponent<GameManager>().BossMinusHP();
            hitCount++;
        }
    }
}
