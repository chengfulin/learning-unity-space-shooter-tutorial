using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnterCollision : MonoBehaviour
{
    [SerializeField] [Header("Target Tag Name")] private string targetTag = null;
    [SerializeField] [Header("Callback")] private UnityEvent doStuff = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == targetTag && doStuff != null)
        {
            doStuff.Invoke();
        }
    }
}
