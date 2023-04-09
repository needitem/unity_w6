using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rig2D;
    Animator animator;

    [SerializeField] private float jumpForce = 680.0f;
    [SerializeField] private float walkForce = 30.0f;
    [SerializeField] private float maxSpeed = 2.0f;

    private int key = 0;


    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rig2D.velocity.y == 0)
        {
            rig2D.AddForce(transform.up * jumpForce);
        }

        key = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { key = 1; transform.localScale = new Vector3(key, 1, 1); }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { key = -1; transform.localScale = new Vector3(key, 1, 1); }

        float speedX = Mathf.Clamp(rig2D.velocity.x, -maxSpeed, maxSpeed);
        rig2D.AddForce(transform.right * key * walkForce);

        animator.speed = speedX / 2;

        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Goal");
        SceneManager.LoadScene("ClearScene");
    }
}
