using Crest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WaterPostEffects : MonoBehaviour
{

    private PostProcessVolume _volume;

    private void Start()
    {
        _volume = GetComponent<PostProcessVolume>();
    }

    private void Update()
    {
        if(OceanRenderer.Instance.ViewerHeightAboveWater < 0)
        {
            _volume.isGlobal = true;
        }
        else if (OceanRenderer.Instance.ViewerHeightAboveWater > 0)
        {
            _volume.isGlobal = false;
        }
    }
}
