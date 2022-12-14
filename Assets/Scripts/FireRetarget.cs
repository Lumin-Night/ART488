using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRetarget : MonoBehaviour
{
    public SpringJoint Sprite;
    public Rigidbody newTarget;
    public Light fireLight;
    [SerializeField] private float fireIntensity;
    [SerializeField] private float fireRange;
    private void OnTriggerEnter(Collider other)
    {
        Sprite.connectedBody = newTarget;
        fireLight.intensity = fireIntensity;
        fireLight.range = fireRange;
    }
}
