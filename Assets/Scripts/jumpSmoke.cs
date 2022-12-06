using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpSmoke : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.25f;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
