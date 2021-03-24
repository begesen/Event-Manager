using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public PlayerData data;
    public float speedMultiplier = 3000f;

    Rigidbody2D rb;
    bool facingRight = true;

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnPlayerLanded, OnLanded);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnPlayerLanded, OnLanded);
    }

    void OnLanded()
    {
        data.isLanded = true;
        animator.SetBool("isLanded", data.isLanded);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && data.isLanded)
        {
            EventManager.Broadcast(GameEvent.OnJump);
            rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
            data.isLanded = false;
            animator.SetBool("isLanded", data.isLanded);
        }




    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speedMultiplier * Time.fixedDeltaTime, rb.velocity.y);
        if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            Flip();
        }
        else if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            Flip();
        }
        data.speed = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", data.speed);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
