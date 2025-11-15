using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float currentSpeed = GameManager.Instance.GetWorldSpeed();

        if (rb != null)
        {
            rb.velocity = new Vector3(0, 0, -currentSpeed);
        }

        if (transform.CompareTag("Obstacle") && transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }
}