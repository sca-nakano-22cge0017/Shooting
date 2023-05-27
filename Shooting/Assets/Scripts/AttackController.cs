using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject Bullet;

    void Start()
    {
        
    }

    void Update()
    {
        Transform playerPos = Player.GetComponent<Transform>();
        Vector3 pos = playerPos.position;
        Instantiate(Bullet, pos, Quaternion.identity);
    }
}
