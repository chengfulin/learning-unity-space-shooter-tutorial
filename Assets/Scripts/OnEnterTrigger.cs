using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnterTrigger : MonoBehaviour
{
    [SerializeField] [Header("Target Tag Name")] private string targetTag = null;
    [SerializeField] [Header("Callback")] private UnityEvent doStuff = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag && doStuff != null)
        {
            doStuff.Invoke();
        }
    }
}
