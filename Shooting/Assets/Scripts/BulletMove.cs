using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e����
/// </summary>
public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed;
    float angle;
    Vector3 vec;

    void Start()
    {
        angle = this.transform.rotation.z; //Inspector����p�x���擾
        Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0); //�p�x����x�N�g���Z�o
        vec = direction * speed * Time.deltaTime;
    }

    void Update()
    {
        //�ړ�
        transform.position += vec;

        //�e�폜
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
