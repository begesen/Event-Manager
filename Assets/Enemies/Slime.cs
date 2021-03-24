using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Vector3[] waypoints;
    
    public PlayerData data;

    public float speed = 10f;
    public bool facingRight = false;
    bool isAlive = true;
    Animator animator;
    Rigidbody2D rb;
    CircleCollider2D trigCol;
    PolygonCollider2D phyCol;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trigCol = GetComponent<CircleCollider2D>();
        phyCol = GetComponent<PolygonCollider2D>();
        StartCoroutine("MoveSlime");

    }

    int currentTarget = 0;
    private void Update()
    {
        if ((transform.position - waypoints[currentTarget]).magnitude >= .1f)
        {
            //transform.Translate(waypoints[currentTarget]);
            if (transform.position.x < waypoints[currentTarget].x && facingRight)
            {
                Flip();
            }
            else if (transform.position.x > waypoints[currentTarget].x && !facingRight)
            {
                Flip();
            }
        }
        else
        {
            currentTarget = (currentTarget + 1) % waypoints.Length;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


    IEnumerator MoveSlime()
    {
        while (isAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentTarget], .1f);
            yield return new WaitForSeconds(2.5f/speed);
        }
    }

    void OnDead(){
        EventManager.Broadcast(GameEvent.OnSlimeKilled);
        isAlive = false;
        animator.SetBool("isAlive",isAlive);
        phyCol.enabled = false;
        trigCol.enabled = false;
        transform.rotation = new Quaternion(0,0,-.5f,0);
        rb.AddForce(new Vector2(0,5),ForceMode2D.Impulse);
        Invoke("DestroySlime",3f);
    }

    void DestroySlime(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && data.isInvulnerable){
            OnDead();
        }
    }
}
