using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒAƒCƒeƒ€§Œä
/// </summary>
public class ItemsController : MonoBehaviour
{
    float speed = 2; //ˆÚ“®‘¬“x
    [SerializeField, Header("Å¬ƒTƒCƒY")] float min;
    [SerializeField, Header("Šgk‚ÌU•")] float amplitude = 0.05f;
    [SerializeField, Header("Šgk‚Ì‘¬“x")] float cycleSpeed = 3f;

    void Update()
    {
        //ˆÚ“®
        this.transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        //Šgk
        float scale = amplitude * Mathf.Sin(Time.time * cycleSpeed) + min;
        this.transform.localScale = new Vector3(scale, scale, 1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //ƒvƒŒƒCƒ„[‚ÉG‚ê‚½‚çÁ‚¦‚é
        if(other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
