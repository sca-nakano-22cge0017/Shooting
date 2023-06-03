using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    float speed = 2;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        float scale = 0.1f * Mathf.Sin(Time.time * 3f) + 0.5f;
        this.transform.localScale = new Vector3(scale, scale, 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
