using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DoFAdjust : MonoBehaviour
{
    public PostProcessVolume volume;
    public GameObject Player;
    
    [SerializeField] private float depth;
    DepthOfField depthOfField;
    private void Start()
    {
        volume.profile.TryGetSettings(out depthOfField);
    }


    void Update()
    {
        Vector3 playerOffset = Player.transform.position - this.transform.position;
        depth = playerOffset.magnitude;
        depthOfField.focusDistance.value = depth;
    }
}
