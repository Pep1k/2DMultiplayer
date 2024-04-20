using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] private GameObject canvas;
    [SerializeField] float cooldown;
    [SerializeField] Transform shootPoint;

    private PhotonView photonView;
    private Vector3 direction;
    private Vector3 prevDirectoin;
    float timer;
    private Rigidbody2D rb;
    bool isGrounded;
    bool isFacingRight = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (photonView.IsMine == false)
            return;
        float horizontal = Input.GetAxis("Horizontal");
        direction = new Vector3(horizontal, 0, 0);
        direction.Normalize();
        if (horizontal > 0 )
        {
            isFacingRight = true;
            Vector3 scale = transform.localScale;
            scale.x = 1;
            canvas.transform.localScale = scale/ 100;
            transform.localScale = scale;
        }
        else if (horizontal < 0 )
        {
            isFacingRight = false;
            Vector3 scale = transform.localScale;
            scale.x = -1;
            canvas.transform.localScale = scale/ 100;
            transform.localScale = scale;
        }
        if (direction.magnitude != 0)
        {
            prevDirectoin = direction;
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        Launch();


    }

    private void Launch()
    {
        if (timer >= cooldown & Input.GetMouseButtonDown(0))
        {
                var bullet = PhotonNetwork.Instantiate("Bullet", shootPoint.position, Quaternion.identity);
                timer = 0;
                var script = bullet.GetComponent<Bullet>();
                script.creatorView = photonView;
                script.SetDiection(new Vector2(prevDirectoin . x, 0));
                var rendery = bullet.GetComponent<SpriteRenderer>();
                if (isFacingRight == false)
                {
                    rendery.flipX = true;
                }
                else
                {
                    rendery.flipX = false;
                }
           
        }
        timer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction .x * speed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

    }
}
