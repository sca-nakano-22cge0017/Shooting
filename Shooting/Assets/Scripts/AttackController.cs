using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject Bullet;

    float time;

    void Start()
    {

    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= 0.5f)
        {
            Transform playerPos = Player.GetComponent<Transform>();
            Vector3 pos = playerPos.position;

            Instantiate(Bullet, pos, Quaternion.identity);
            time = 0f;
        }
    }
}
