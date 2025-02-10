using UnityEngine;

public class CoinBehavior : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed = 90.0f;

    [SerializeField]
    private int score = 5;

    public static int ScoreTotal {get; set;}

    public AudioClip pickupSFX;

    LevelManager levelManager;

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Score from " + transform.name + ": " + score);

        levelManager = FindAnyObjectByType<LevelManager>();
        Debug.Log("Found LM: " + levelManager.name);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player")) {
            DestroyPickup();
        }
    }

    void DestroyPickup()
    {
        PlayAudio();

        animator = GetComponent<Animator>();
        animator.SetTrigger("pickupDestroyed");

        ScoreTotal += score;

        if (levelManager)
            levelManager.SetScoreText(ScoreTotal);

        Destroy(gameObject, 2);
    }

    void OnDestroy()
    {
        //pickupCount--;
        Debug.Log("Add score: " + score);
    }

    void PlayAudio()
    {
        // var audioSource = GetComponent<AudioSource>();

        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);

        // audioSource.Play();
    }

    public static void ResetPickups()
    {
        ScoreTotal = 0;
    }
}
