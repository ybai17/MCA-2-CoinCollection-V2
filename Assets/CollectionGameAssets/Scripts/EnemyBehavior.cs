using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;

    public AudioClip enemySFX;
    public AudioClip enemyStompedSFX;

    bool isAlive;

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target && LevelManager.IsPlaying && isAlive) {
            FollowTarget();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            EnemyDie();
        }
    }

    void FollowTarget()
    {
        Vector3 targetGroundedPos = new Vector3(target.position.x, -0.2f, target.position.z);
        transform.LookAt(targetGroundedPos);
        transform.position = Vector3.MoveTowards(transform.position, targetGroundedPos, Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (transform.position.y <= collision.gameObject.transform.position.y - 0.2) {
                Debug.Log(gameObject + " bonked");

                EnemyStomped();

                return;
            }
        }
        
        if (collision.gameObject.CompareTag("Enemy")) {
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        AudioSource.PlayClipAtPoint(enemySFX, Camera.main.transform.position, 0.1f);
        Destroy(gameObject);
    }

    public void EnemyStomped()
    {
        isAlive = false;
        AudioSource.PlayClipAtPoint(enemyStompedSFX, Camera.main.transform.position, 0.4f);

        transform.position = new Vector3(transform.position.x, -0.2f, transform.position.z);

        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        animator = GetComponent<Animator>();

        animator.SetTrigger("gotStomped");
        Destroy(gameObject, 0.5f);
    }
}
