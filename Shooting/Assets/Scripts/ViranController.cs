using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViranController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject Bullet;
    float bulletTime = 0;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

        bulletTime += Time.deltaTime;
        if(bulletTime >= 0.5f) {
            Transform ViranPos = this.GetComponent<Transform>();
            Vector3 bulletPos = ViranPos.position;

            Instantiate(Bullet, bulletPos, Quaternion.Euler(0, 0, 0));
            Instantiate(Bullet, bulletPos, Quaternion.Euler(0, 0, 60));
            Instantiate(Bullet, bulletPos, Quaternion.Euler(0, 0, -60));
            bulletTime = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
