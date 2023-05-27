using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Image[] HP;
    bool isDamage, invincible;
    int imageNum;

    void Start()
    {
        isDamage = false;
        imageNum = HP.Length;
    }

    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;

        if(Input.GetKey(KeyCode.W) && pos.y <= 4.5) {
            pos.y += speed * Time.deltaTime;
            myTransform.position = pos;
        }
        if(Input.GetKey(KeyCode.S) && pos.y >= -4.5) {
            pos.y -= speed * Time.deltaTime;
            myTransform.position = pos;
        }
        if(Input.GetKey(KeyCode.A) && pos.x >= -3.0) {
            pos.x -= speed * Time.deltaTime;
            myTransform.position = pos;
        }
        if(Input.GetKey(KeyCode.D) && pos.x <= 3.0) {
            pos.x += speed * Time.deltaTime;
            myTransform.position = pos;
        }

        if(isDamage) {
            StartCoroutine("Damage");
            HP[imageNum - 1].GetComponent<Image>().color = new Color(0,0,0,255);
            imageNum--;
            isDamage = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Rock" && !invincible) {
            isDamage = true;
        }
    }

    IEnumerator Damage() {
        for (int i = 0; i < 3; i++) {
            invincible = true;
            GetComponent<SpriteRenderer>().color = new Color (0, 0, 0, 0.2f);
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1.0f);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(0.5f);
        invincible = false;
    }
}
