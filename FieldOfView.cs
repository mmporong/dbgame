using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius = 5f;
    [Range(0, 360)]

    public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    
    // Happy new year
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
