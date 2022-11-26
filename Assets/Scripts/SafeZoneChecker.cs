using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafeZoneChecker : MonoBehaviour
{
    private int SafeZone;
    [SerializeField] private string SafeZoneLayerName="SafeZone";
    public PlayerController3D PlayerController3D;
    [SerializeField] private int SafeZoneCounter;
    public Light HeaderLight;

    private void Awake()
    {
        SafeZoneCounter = 0;
        SafeZone = LayerMask.NameToLayer(SafeZoneLayerName);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == SafeZone)
        {
            SafeZoneCounter--;
            if (SafeZoneCounter <= 0)
            {                
                PlayerController3D.SetSlow();
                HeaderLight.color = Color.red;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == SafeZone)
        {
            SafeZoneCounter++;
            if (SafeZoneCounter >= 0)
            {
                PlayerController3D.ClearSlow();
                HeaderLight.color = Color.white;
            }
        }

    }

    private void Update()
    {
        transform.localPosition = new Vector3(PlayerController3D.moveXZ.x * 1.2f , 0, PlayerController3D.moveXZ.y * 1.2f);
    }



}




