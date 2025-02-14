using UnityEngine;

public class CoinBehavior : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed = 90.0f;

    [SerializeField]
    private int scoreValue = 5;

    public static int ScoreTotal {get; set;}

    public AudioClip pickupSFX;

    LevelManager levelManager;

    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Score from " + transform.name + ": " + scoreValue);

        levelManager = FindAnyObjectByType<LevelManager>();
        Debug.Log("Found LM: " + levelManager.name);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        if (Input.GetKeyDown(KeyCode.Tab)) {
            DestroyPickup();
        }
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

        ScoreTotal += scoreValue;

        if (levelManager)
            levelManager.SetScoreText(ScoreTotal);

        Destroy(gameObject, 2);
    }

    void OnDestroy()
    {
        //pickupCount--;
        Debug.Log("Add score: " + scoreValue);
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
