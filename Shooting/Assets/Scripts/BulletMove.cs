using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed;
    float angle;
    Vector3 vec;

    void Start()
    {
        angle = this.transform.rotation.z; //Inspector‚©‚çŠp“x‚ðŽæ“¾
        Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
        vec = direction * speed * Time.deltaTime;
    }

    void Update()
    {
        transform.position += vec;

        if(this.transform.position.y >= 5)
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
