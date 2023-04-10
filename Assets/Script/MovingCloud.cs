using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f; // The speed of the cloud movement.
    [SerializeField] private float moveDistance = 1.0f; // The distance the cloud will move left and right.

    // Update is called once per frame
    void Update()
    {
        // This method is not used in this script.
    }

    private void FixedUpdate()
    {
        // Move the cloud left and right by moveDistance.
        transform.position = new Vector3(Mathf.PingPong(Time.time * speed, moveDistance * 2) - moveDistance, transform.position.y, transform.position.z);
    }
}
