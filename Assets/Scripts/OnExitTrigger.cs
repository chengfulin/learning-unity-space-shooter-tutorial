using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// As exit an collider
/// </summary>
public class OnExitTrigger : MonoBehaviour
{
    [SerializeField] [Header("Target Tag Name")] private string targetTag = null;
    [SerializeField] [Header("Callback")] private UnityEvent doStuff = null;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag && doStuff != null)
        {
            doStuff.Invoke();
        }
    }
}
