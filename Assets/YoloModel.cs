using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using UnityEngine.XR.ARFoundation;

public class YoloModel : MonoBehaviour
{
    public RenderTexture m_RenderTexture;

    ARCameraManager m_CameraMgr;
    ARCameraBackground m_CamBG;

    // Start is called before the first frame update
    void Start()
    {
        m_CameraMgr = FindObjectOfType<ARCameraManager>();
        m_CamBG = FindObjectOfType<ARCameraBackground>();
        if (m_CameraMgr == null)
            Debug.Log("ARCameraManager Component Missing!");
        else
            m_CameraMgr.frameReceived += onFrame;
        if (m_CamBG == null)
            Debug.Log("ARCameraBackground Component Missing!");

    }
 
    void onFrame(ARCameraFrameEventArgs args)
    {
        foreach (var t in args.textures)
            Graphics.Blit(t, m_RenderTexture, m_CamBG.material);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
