using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public GameObject player;

    // The initial offset between the player and the camera.
    // We want to maintain this offset as we move the camera.
    private Vector3 offset;

    // Start is called before the first frame update.
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame, after Update.
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
