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
        
        SetAuxLine(0.02f, 12f);
    }

    public void SetAuxLine(float gap, float angleY=0f, float angleZ=0f)
    {
        for (var i = 0; i < cubes.Count; i += 1)
        {
            var current = cubes[i].position;
            current.x = (i - 1.5f) * gap;
            
            cubes[i].position = current;
            cubes[i].eulerAngles = new Vector3(0, angleY, angleZ);
        }
    }
}
