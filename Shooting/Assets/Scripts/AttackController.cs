using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒvƒŒƒCƒ„[‚ÌUŒ‚
/// </summary>
public class AttackController : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject Bullet;

    float time;
    [SerializeField] float coolTime = 0.5f;

    void Update()
    {
        //ˆê’èŠÔ–ˆ‚É’e‚ğ¶¬
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
