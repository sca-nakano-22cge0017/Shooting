using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    float firingSpeed = 10;
    [SerializeField] float speed;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        if(this.transform.position.y >= 5)
        {
            Destroy(gameObject);
        }
    }
}
