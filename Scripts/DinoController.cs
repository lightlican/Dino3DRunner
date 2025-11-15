using UnityEngine;

public class DinoController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpCooldown = 2f;

    private Rigidbody rb;
    private bool canJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.tag = "Player";

        
        jumpForce = 10f;      
        jumpCooldown = 2f;   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !GameManager.Instance.IsGameOver())
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
            canJump = false;
            Invoke("EnableJump", jumpCooldown);

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayJumpSound();
        }
    }

    void EnableJump()
    {
        canJump = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayCrashSound();

            GameManager.Instance.GameOver();
        }
    }
}