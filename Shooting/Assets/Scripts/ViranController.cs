using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViranController : MonoBehaviour
{
    [SerializeField] float speed;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
