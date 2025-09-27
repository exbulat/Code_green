using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text timerText;

    [Header("Game")]
    public int score = 0;
    public float timeLeft = 60f;

    [Header("Penalties (настройки)")]
    [Tooltip("Сколько секунд отнимать за неправильный бросок")]
    public float wrongTimePenalty = 5f;
    [Tooltip("Сколько очков отнимать за неправильный бросок (можно 0)")]
    public int wrongScorePenalty = 5;
    [Tooltip("Сколько очков давать за правильный бросок")]
    public int correctScore = 7;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    private bool isGameOver = false;

    [Header("Feedback")]
    public Color penaltyFlashColor = Color.red;
    public float flashDuration = 0.35f;
    
    

    private Color timerDefaultColor;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (timerText != null) timerDefaultColor = timerText.color;
        UpdateUI();
    }

    void Update()
    {
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0f) timeLeft = 0f;
            UpdateTimerText();

            if (timeLeft == 0f)
                EndGame();
        }
    }

    public void OnCorrectDrop()
    {
        AddScore(correctScore);
        // можно добавить звук/эффект здесь
    }

    public void OnWrongDrop()
    {
        if (wrongScorePenalty != 0) AddScore(-wrongScorePenalty);
        LoseTime(wrongTimePenalty);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score < 0) score = 0;
        UpdateScoreText();
    }

    public void LoseTime(float amount)
    {
        timeLeft -= amount;
        if (timeLeft < 0f) timeLeft = 0f;
        UpdateTimerText();
        StartCoroutine(FlashTimer());
        if (timeLeft == 0f) EndGame();
    }

    IEnumerator FlashTimer()
    {
        if (timerText == null) yield break;
        timerText.color = penaltyFlashColor;
        yield return new WaitForSeconds(flashDuration);
        timerText.color = timerDefaultColor;
    }

    void UpdateUI()
    {
        UpdateScoreText();
        UpdateTimerText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = score.ToString();
    }

    void UpdateTimerText()
    {
        if (timerText != null) timerText.text = Mathf.CeilToInt(timeLeft).ToString();
    }

    void EndGame()
{
    if (isGameOver) return;
    isGameOver = true;

    Debug.Log("Game Over! Score: " + score);

    if (gameOverPanel != null)
    {
        gameOverPanel.SetActive(true);

        // поднимаем панель выше всех
        gameOverPanel.transform.SetAsLastSibling();

        if (gameOverText != null)
            gameOverText.text = "Вы набрали " + score + " очков!";
    }

    // Остановим спавнер
    FindObjectOfType<TrashSpawner>().StopSpawning();

    // Можно остановить время (по желанию)
    // Time.timeScale = 0f;
}


}
