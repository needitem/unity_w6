using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rig2D;
    Animator animator;
    ItemGenerate tile;
    GameObject gameDirector;

    [SerializeField] private float jumpForce = 380.0f;
    [SerializeField] private float walkForce = 30.0f;
    [SerializeField] private float maxSpeed = 1.0f;
    [SerializeField] private bool grounded = false;

    private int key = 0;

    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tile = FindObjectOfType<ItemGenerate>();
        gameDirector = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (grounded)
        {
            animator.SetBool("jumping", false);
            if (Input.GetKeyDown(KeyCode.Space))
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
            key = 1;
            transform.localScale = new Vector3(key, 1, 1);
            animator.SetBool("moving", true);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            key = -1;
            transform.localScale = new Vector3(key, 1, 1);
            animator.SetBool("moving", true);
        }

        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    private void FixedUpdate()
    {
        float speedX = Mathf.Clamp(rig2D.velocity.x, -maxSpeed, maxSpeed);
        rig2D.velocity = new Vector2(speedX, rig2D.velocity.y);
        rig2D.AddForce(transform.right * key * walkForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("ClearScene");
        }
        else if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
        else if (collision.gameObject.tag == "Item")
        {
            gameDirector.GetComponent<GameDirector>().score += tile.CatEatsItem(transform.position);
        }
    }
}
