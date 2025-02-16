using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(AudioSource))]

public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying {get; private set;}

    public static int currentLevel;

    [SerializeField]
    private float levelTime = 30.0f;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text messageText;
    public GameObject nextButton;
    public AudioClip winSoundEffect;
    public AudioClip loseSoundEffect;
    private float countdown;

    private AudioSource audioSource;

    // called before Start(), called as soon as the Unity engine compiles everything
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Current level: " + currentLevel);
        Debug.Log("levelTime: " + levelTime);

        countdown = levelTime;

        CoinBehavior.ResetPickups();
        SetScoreText(0);
        IsPlaying = true;
        nextButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlaying)
        {
            LevelTimer();
            SetTimerText();

            if (currentLevel == 0) {
                if (CoinBehavior.ScoreTotal >= 20) {
                    //win
                    LevelBeat();
                }
            } else if (currentLevel == 1) {
                if (CoinBehavior.ScoreTotal >= 35) {
                    //win
                    LevelBeat();
                }
            } else if (currentLevel == 2) {
                if (CoinBehavior.ScoreTotal >= 75 && FlagBehavior.ObjectiveReached) {
                    //win
                    GameComplete();
                }
            }

            if (countdown <= 0) {
                //lose
                LevelLost();
            }
        }
    }

    void LevelTimer()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0) {
            countdown = 0;
        }

        // Debug.Log("countdown = " + countdown.ToString("0.00"));
    }

    void SetTimerText()
    {
        timerText.text = countdown.ToString("0.00");
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    void LevelBeat() {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();

        IsPlaying = false;

        // play sound effect
        PlaySoundFX(winSoundEffect);
        // display message
        DisplayMessage("You Win!");

        nextButton.SetActive(true);

        //CoinBehavior.ResetPickups();
        //Invoke("ReloadSameScene", 5);
        //ReloadSameScene();
    }

    public void LevelLost() {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();

        IsPlaying = false;
        
        // play sound effect
        PlaySoundFX(loseSoundEffect);
        // display message
        DisplayMessage("GAME OVER");

        CoinBehavior.ResetPickups();
        Invoke("ReloadSameScene", 5); //waits 5 seconds before calling ReloadSameScene()
        //ReloadSameScene();
    }

    void GameComplete()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        
        IsPlaying = false;

        //play sound effect
        PlaySoundFX(winSoundEffect);
        //display message
        DisplayMessage("GAME COMPLETED");

        CoinBehavior.ResetPickups();

        currentLevel = -1;

        Debug.Log("Next button: " + nextButton);

        Debug.Log("Next button child: " + nextButton.transform.GetChild(0));

        Debug.Log("TextMeshPro: " + nextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>());

        nextButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Play again?";

        nextButton.SetActive(true);
    }

    void PlaySoundFX(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }

    void DisplayMessage(string message)
    {
        if (!messageText)
            return;

        messageText.text = message;

        if (FlagBehavior.ObjectiveReached && currentLevel == 2) {
            messageText.fontSize = 50;
        } else {
            messageText.fontSize = 100;
        }

        messageText.enabled = true;
    }

    void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    void ReloadSameScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadNextLevel()
    {
        /*
        if (nextLevel.Length > 0) {
            LoadSceneByName(nextLevel);
        } else {
            Debug.Log("Next level not specified");
        }
        */
        //LoadSceneByName("Level" + (currentLevel + 1));
        LoadSceneByIndex(++currentLevel);
    }
}
