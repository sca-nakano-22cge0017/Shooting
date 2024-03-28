using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// íeêßå‰
/// </summary>
public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed;
    float angle;
    Vector3 vec;

    void Start()
    {
        angle = this.transform.rotation.z; //InspectorÇ©ÇÁäpìxÇéÊìæ
        Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0); //äpìxÇ©ÇÁÉxÉNÉgÉãéZèo
        vec = direction * speed * Time.deltaTime;
    }

    void Update()
    {
        //à⁄ìÆ
        transform.position += vec;

        //íeçÌèú
        if(this.transform.position.y >= 4.5)
        {
            Destroy(gameObject);
        }
        if(this.gameObject.tag == "Viran's Bullet") {
            if(this.transform.position.y <= -5) {
                Destroy(gameObject);
            }
        }
    }
}
