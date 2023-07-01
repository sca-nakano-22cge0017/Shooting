using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViranController : MonoBehaviour
{
    float speed;
    int shots;
    float angle, correction;
    float coolTime;
    float launchAngle;
    [SerializeField] GameObject Bullet;
    float bulletTime = 0;
    [SerializeField] GameObject particleObject;
    [SerializeField] StatuaData status;

    GameObject MainManager;

    public void Start()
    {
        speed = status.Speed;
        shots = status.Shots;
        angle = status.Angle;
        correction = status.Correction;
        coolTime = status.CoolTime;

        MainManager = GameObject.Find("MainManager");
    }

    public void Update()
    {
        this.transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);

        bulletTime += Time.deltaTime;
        if(bulletTime >= coolTime) {
            Transform ViranPos = this.GetComponent<Transform>();
            Vector3 bulletPos = ViranPos.localPosition;

            for (int i = -1; i < shots - 1; i++) {
                if(shots % 2 == 0) {
                    launchAngle = i * angle + angle / 2;
                }
                else { launchAngle = i * angle;}
                Instantiate(Bullet, bulletPos, Quaternion.Euler(0, 0, launchAngle + correction));
            }
            bulletTime = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Instantiate(particleObject, this.transform.localPosition, Quaternion.identity);
            MainManager.GetComponent<GameManager>().Kill();
            Destroy(gameObject);
        }
    }
}
