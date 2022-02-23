using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Animator player_animator;
    [SerializeField]
    Rigidbody2D player_Rb;

    float runspeed = 5.0f;
    [SerializeField]
    bool onGround;
    private int extraJump = 0;
    private bool isPickUp;
    private float currentTime;
    private bool finishTimer = true;
    [SerializeField] public Text countDownText;
    public GameObject powerUpObject;

    private void Awake()
    {
        player_Rb = gameObject.GetComponent<Rigidbody2D>();
        countDownText.gameObject.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("powerUp"))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        isPickUp = true;
        finishTimer = false;
        countDownText.gameObject.SetActive(true);
        Destroy(powerUpObject.gameObject);
        currentTime = 10f;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (currentTime == 0f)
        {
            finishTimer = true;
        }

        bool vertical = Input.GetKeyDown(KeyCode.UpArrow);

        if (currentTime == 0f || (finishTimer == true))
        {

            Player_Jump(vertical);
        }


        if ((isPickUp == true) && (currentTime > 0f) && finishTimer == false)
        {
            currentTime -= 1 * Time.deltaTime;
            countDownText.text = currentTime.ToString("0");
            PlayerDoubleJump(vertical);
        }
        else if (currentTime <= 0f)
        {
            currentTime = 0f;
            countDownText.gameObject.SetActive(false);
            isPickUp = false;
        }

        Player_Movement(horizontal);
        Player_Run(horizontal);
    }



    void Player_Jump(bool vertical)
    {

        if ((vertical) && (isPickUp != true) && (onGround == true))
        {
            player_animator.SetBool("IsJump", true);
            player_Rb.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
        }
        else if (onGround == false)
        {
            player_animator.SetBool("IsJump", false);
        }
    }
    private void PlayerDoubleJump(bool vertical)
    {
        if (onGround == true)
        {
            extraJump = 1;
        }

        if ((vertical) && (extraJump > 0))
        {
            if (onGround == true)
            {
                player_animator.SetBool("IsJump", true);
            }
            else
            {
               player_animator.SetBool("IsJump", true);
            }
            player_Rb.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            extraJump--;
        }
        else if (onGround == false)
        {
            player_animator.SetBool("IsJump", false);
        }
    }

    void Player_Movement(float horizontal)
    {
        player_animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 scale = transform.localScale;

        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
    }

    void Player_Run(float horizontal)
    {
        Vector2 move_position = transform.position;
        move_position.x += horizontal * runspeed * Time.deltaTime;
        transform.position = move_position;
    }

}
