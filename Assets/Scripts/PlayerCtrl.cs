using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    [Header("Player's Bullet")] public GameObject bullet = null;
    [Header("Fire Spot")] public GameObject fireSpot = null;
    [Header("Offset from Camera")] public float depth = 0.0f;

    private Camera MainCamera { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        this.MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FireByMouse();
        FireByKeyboard();
        MoveByMouse();
    }

    /// <summary>
    /// As player disabled (hidden from scene)
    /// </summary>
    private void OnDisable()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Fire a bullet as mouse clicked.
    /// </summary>
    private void FireByMouse()
    {
        if (Input.GetMouseButtonDown(0) && fireSpot != null)
        {
            // Instantiate a bullet, at the fire spot's position.
            Instantiate(bullet, fireSpot.transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Fire a bullet as key 'Space' down.
    /// </summary>
    private void FireByKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space) && fireSpot != null)
        {
            // Instantiate a bullet, at the fire spot's position.
            Instantiate(bullet, fireSpot.transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Move the player as mouse moved.
    /// </summary>
    private void MoveByMouse()
    {
        // Use mouse position
        if (Input.mousePosition.x >= 0 &&
            Input.mousePosition.x < Screen.width &&
            Input.mousePosition.y >= 0 &&
            Input.mousePosition.y < Screen.height)
        {
            transform.position = MainCamera.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth));
        }
    }
}
