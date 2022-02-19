using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public Animator animator;
    private float speed = 5f;
    void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        PlayerAnimation(horizontal);
        PlayerMove(horizontal);
    }

    private void PlayerMove(float horizontal)
    {
        Vector2 currentPosition = transform.position;
        currentPosition.x = currentPosition.x + speed * horizontal * Time.deltaTime;
        transform.position = currentPosition;
    }

    private void PlayerAnimation(float horizontal)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector2 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if(horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}
