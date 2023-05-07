using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float xOffset = 0;
    public float zOffset = 0;

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x + xOffset, target.transform.position.y, transform.position.z + zOffset);
    }
}
