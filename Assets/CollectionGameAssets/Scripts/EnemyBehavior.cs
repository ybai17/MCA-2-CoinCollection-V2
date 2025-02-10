using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;

    public AudioClip enemySFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!target) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (target && LevelManager.IsPlaying) {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            AudioSource.PlayClipAtPoint(enemySFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
