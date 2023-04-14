using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    
    private Camera playerCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPos();
    }

    /// <summary>
    /// Update the Camera Postion so that it's following and behind the player at all times
    /// </summary>
    private void UpdateCameraPos()
    {
        playerCamera.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3.0f, -8);
    }
}
