using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10) {
            LevelManager levelManager = FindAnyObjectByType<LevelManager>();

            levelManager.LevelLost();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {

            if (transform.position.y >= collision.gameObject.transform.position.y + 0.2) {
                PlayerController playerController = GetComponent<PlayerController>();
                playerController.Jump(2.5f);
                
                return;
            }

            LevelManager levelManager = FindAnyObjectByType<LevelManager>();

            levelManager.LevelLost();
            Destroy(gameObject);
        }
    }
}
