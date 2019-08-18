using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// As exit an collider
/// </summary>
public class OnExitCollider : MonoBehaviour
{
    [Header("Target Tag Name")] public string targetTag = null;
    [Header("Callback")] public UnityEvent doStuff = null;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag && doStuff != null)
        {
            doStuff.Invoke();
        }
    }
}
