using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViranController : MonoBehaviour
{
    float speed;
    int shots;
    float angle;
    float coolTime;
    float launchAngle;
    [SerializeField] GameObject Bullet;
    float bulletTime = 0;
    [SerializeField] GameObject particleObject;
    [SerializeField] StatuaData status;

    void Start()
    {
        speed = status.Speed;
        shots = status.Shots;
        angle = status.Angle;
        coolTime = status.CoolTime;
    }

    void Update()
    {
        this.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

        bulletTime += Time.deltaTime;
        if(bulletTime >= coolTime) {
            Transform ViranPos = this.GetComponent<Transform>();
            Vector3 bulletPos = ViranPos.position;

            for (int i = 0; i < shots; i++) {
                if(shots % 2 == 0) {
                    launchAngle = i * angle + angle / 2;
                }
                else { launchAngle = i * angle;}
                Instantiate(Bullet, bulletPos, Quaternion.Euler(0, 0, launchAngle));
            }
            bulletTime = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Instantiate(particleObject, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
