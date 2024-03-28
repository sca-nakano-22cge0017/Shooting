using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̍U��
/// </summary>
public class AttackController : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject Bullet;

    float time;
    [SerializeField] float coolTime = 0.5f;

    void Update()
    {
        //��莞�Ԗ��ɒe�𐶐�
        time += Time.deltaTime;
        if(time >= coolTime)
        {
            Transform playerPos = Player.GetComponent<Transform>();
            Vector3 pos = playerPos.position;

            Instantiate(Bullet, pos, Quaternion.identity);
            time = 0f;
        }
    }
}
