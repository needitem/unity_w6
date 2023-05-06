using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float xOffset = 0;
    public float zOffset = 0;
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x + xOffset, transform.position.y, target.position.z + zOffset);
    }
}
