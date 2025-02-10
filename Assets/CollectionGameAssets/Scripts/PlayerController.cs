using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    Rigidbody rb;

    [SerializeField]
    private float movementSpeed = 5.0f;
    [SerializeField]
    private float jumpForce = 5.0f;

    private bool isGrounded;

    public AudioClip jumpSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.IsPlaying) {
            return;
        }
        Jump();
    }

    void FixedUpdate()
    {
        if (LevelManager.IsPlaying) {
            Move();
        } else {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    void Move()
    {
        // get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Debug.Log("horizontal = " + horizontal + "; vertical = " + vertical);

        // compute a movement vector
        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

        // apply force
        rb.AddForce(movement * movementSpeed);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // disable mid-air jumps
            isGrounded = false;

            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = jumpSFX;
            audioSource.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {   
        ContactPoint contact = collision.contacts[0];

        if (contact.normal.y > 0.5f)
        {
            isGrounded = true;
        }

        //Debug.Log("Collided position" + contact.point);
        //Debug.Log("Contact normal: " + contact.normal);
    }
}
