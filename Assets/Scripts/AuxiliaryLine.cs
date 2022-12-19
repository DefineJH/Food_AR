using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliaryLine : MonoBehaviour
{
    private List<Transform> cubes;
    
    private void Awake()
    {
        cubes = new List<Transform>();
        
        var transforms = GetComponentsInChildren<Transform>();
        foreach (var tf in transforms)
        {
            if (tf != transform)
                cubes.Add(tf);
        }
        
        Hide()
        // SetAuxLine(0.02f, 12f);
    }

    public void SetAuxLine(int count=4, float gap=0.02f, float angleY=0f, float angleZ=0f)
    {
        Hide();
        for (var i = 0; i < count; i += 1)
        {
            var current = cubes[i].position;
            current.x = (i - count * (count-1) / 2.0f) * gap;
            
            cubes[i].position = current;
            cubes[i].eulerAngles = new Vector3(0, angleY, angleZ);
            cubes[i].scale = new Vector3(0.00025, 0.008, 0.1);
        }
    }

    public void Hide()
    {
        foreach (var cube in cubes)
            cube.transform.scale = new Vector3();
    }
}
