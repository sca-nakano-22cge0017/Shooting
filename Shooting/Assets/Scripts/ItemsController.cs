using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e������
/// </summary>
public class ItemsController : MonoBehaviour
{
    float speed = 2; //�ړ����x
    [SerializeField, Header("�ŏ��T�C�Y")] float min;
    [SerializeField, Header("�g�k�̐U��")] float amplitude = 0.05f;
    [SerializeField, Header("�g�k�̑��x")] float cycleSpeed = 3f;

    void Update()
    {
        //�ړ�
        this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        //�g�k
        float scale = amplitude * Mathf.Sin(Time.time * cycleSpeed) + min;
        this.transform.localScale = new Vector3(scale, scale, 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //�v���C���[�ɐG�ꂽ�������
        if(other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
