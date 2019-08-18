using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scrolling background using Mathf.Repeat(Time.time * scrollSpeed, maxOffset).
/// if (scrollSpeed > 0) offset from 0 to maxOffset
/// if (scrollSpeed < 0) offset from maxOffset to 0, repeatly
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    [Header("Scrolling Speed")] public float scrollSpeed;
    [Header("Max Offset")] public float maxOffset;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, maxOffset);
        transform.position = initialPosition + Vector3.forward * newPosition;
    }
}
