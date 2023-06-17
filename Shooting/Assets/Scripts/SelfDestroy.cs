using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    ParticleSystem explode;

    void Start()
    {
        explode = this.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(explode.isStopped) {
            Destroy(this.gameObject);
        }
    }
}
