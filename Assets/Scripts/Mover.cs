using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Move a GameObject by constant speed over time.
/// </summary>
public class Mover : MonoBehaviour
{
    [SerializeField] [Header("Constant Speed")] private Vector3 speed = Vector3.forward;
    private Vector3 actualSpeed = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        actualSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += actualSpeed * Time.deltaTime;
    }

    public void SpeedUp(float delta)
    {
        actualSpeed += Vector3.forward * delta;
    }
}
