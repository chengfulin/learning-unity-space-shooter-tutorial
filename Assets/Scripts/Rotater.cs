using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Constantly rotate a GameObject on an axis with an angle in degree.
/// </summary>
public class Rotater : MonoBehaviour
{
    [SerializeField] [Header("Axis")] private Vector3 axis = Vector3.forward;
    [SerializeField] [Header("Angle in degree")] private float angle = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis, angle);
    }
}
