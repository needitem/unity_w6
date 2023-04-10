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
        if (rig2D.velocity.y == 0) { animator.SetBool("jumping", false); }
        if (Input.GetKeyDown(KeyCode.Space) && rig2D.velocity.y == 0)
        {
            rig2D.AddForce(transform.up * jumpForce);
            animator.SetBool("jumping", true);
        }

        key = 0;
        animator.SetBool("moving", false);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            key = 1; transform.localScale = new Vector3(key, 1, 1); animator.SetBool("moving", true); 
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { 
            key = -1; transform.localScale = new Vector3(key, 1, 1); animator.SetBool("moving", true); 
        }

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void FixedUpdate()
    {
        float speedX = Mathf.Clamp(rig2D.velocity.x, -maxSpeed, maxSpeed);
        rig2D.AddForce(transform.right * key * walkForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            Debug.Log("Goal");
            SceneManager.LoadScene("ClearScene");
        }
    }
}
