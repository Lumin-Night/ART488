using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Collect : MonoBehaviour
{
    public PlayerController3D PlayerController3D;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            PlayerController3D.Collect();
            Destroy(this.gameObject);
        }
    }
}
