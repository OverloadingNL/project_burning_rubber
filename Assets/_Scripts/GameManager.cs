using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { set; get; }

    [Header("Speed")]
    public float speed = 0f;
    public bool enableRampUpSpeed = true;
    public float maxSpeed = 50;
    public float speedIncreaseLastTick;
    public float speedIncreaseTime = 1f;
    public float speedIncreaseAmount = .1f;

    [Header("Level generation")]
    public bool tutorial = true;
    public int flatStartSegments = 10;

    [Header("Menu's & UI")]
    public GameObject deathMenu;
    public GameObject gameMenu;
    public GameObject mainMenu;
    public GameObject connectedUI;
    public TextMeshProUGUI mainMenuPlayText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI modifierText;
    public TextMeshProUGUI deathMenuScoreText;
    public TextMeshProUGUI deathMenuCoinText;
    public TextMeshProUGUI hiscoreText;
    public TextMeshProUGUI totalCoinsText;

    public bool IsDead { set; get; }
    public bool isPaused = false;
    public bool isGameStarted = false;

    bool isSaving = false;
    float score;
    float modifierScore = 1;
    int hiscore;
    int lastScore;
    int coinScore;
    int totalCoins;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        mainMenu.SetActive(true);
        gameMenu.SetActive(false);
    }

    private void Update()
    {
        if (MobileInput.Instance.Tap && !isGameStarted)
        {
            if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.currentSelectedGameObject != null)
            {
                return;
            }
            StartRunning();
        }
        if (isGameStarted && !IsDead && !isPaused)
        {
            if (enableRampUpSpeed && !tutorial)
            {
                RampUpSpeed();
            }

            //TODO: scores
        }
    }

    public void StartRunning()
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
            FindObjectOfType<CameraMotor>().IsMoving = true;
            gameMenu.SetActive(true);
            mainMenu.SetActive(false);
            speed = 10;
        }
        if (isPaused)
        {
            TogglePause();
        }
    }

    private void RampUpSpeed()
    {
        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
                enableRampUpSpeed = false;
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            gameMenu.SetActive(false);
            mainMenu.SetActive(true);
            mainMenuPlayText.text = "Resume";
        }
        else
        {
            mainMenu.SetActive(false);
            mainMenuPlayText.text = "Play";
            gameMenu.SetActive(true);
        }

    }

    public void OpenPrivacyPolicy()
    {
        Application.OpenURL("https://overloading.nl/privacy-policy-burning-rubber/");
    }
}
