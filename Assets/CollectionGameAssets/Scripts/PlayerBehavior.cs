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
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            LevelManager levelManager = FindAnyObjectByType<LevelManager>();

            levelManager.LevelLost();
            Destroy(gameObject);
        }
    }
}
