using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCreate : MonoBehaviour
{
    [SerializeField] GameObject[] Rock;
    [SerializeField, Header("Position")] float maxPos, minPos;
    bool create;
    int num = 0;
    float size, pos;

    void Start()
    {
        create = true;
    }

    void Update()
    {
        if(create) {
            create = false;
            StartCoroutine("Create");
        }
    }

    IEnumerator Create() {
        yield return new WaitForSeconds(2);
        num = Random.Range(0, Rock.Length);
        pos = Random.Range(minPos, maxPos);
        GameObject inst = Instantiate(Rock[num], new Vector3(pos, 5, 0), Quaternion.identity);
        inst.layer = LayerMask.NameToLayer("Default");
        create = true;
    }
}
