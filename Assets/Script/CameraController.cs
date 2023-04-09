using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Cat");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
