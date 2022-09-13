using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneChecker : MonoBehaviour
{
    private int SafeZone;
    [SerializeField] private string SafeZoneLayerName="SafeZone";
    public PlayerController3D PlayerController3D;

    private void Awake()
    {
        SafeZone = LayerMask.NameToLayer(SafeZoneLayerName);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == SafeZone)
        {
            PlayerController3D.SetSlow();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == SafeZone)
        {
            PlayerController3D.ClearSlow();
        }

    }
}


