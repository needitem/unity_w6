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
    [SerializeField] private float maxSpeed = 1.0f;
    [SerializeField] private bool grounded = false;

    private int key = 0;


    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            animator.SetBool("jumping", false);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rig2D.AddForce(transform.up * jumpForce);
                animator.SetBool("jumping", true);
                grounded = false;
            }
            
        }

        key = 0;
        animator.SetBool("moving", false);
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            key = 1; transform.localScale = new Vector3(key, 1, 1); animator.SetBool("moving", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
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
        else if (collision.gameObject.tag == "Ground") // Ground에 충돌했을 때 grounded를 true로 변경
        {
            grounded = true;
            transform.SetParent(collision.transform, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = false;
            transform.parent = null;
        }
    }
}
