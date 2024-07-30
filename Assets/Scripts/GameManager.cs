using System.Collections;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int lives = 4;
    public int score = 0;
    private float timer = 0;
    private bool wonMG;
    private bool firstTime;
    private GameObject currentMG;
    private bool minigameActive = false;
    private bool minigameEnded = false;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] Animator background;
    [SerializeField] AudioSource audioSource;
    [SerializeField] MinigameData[] minigameDatas;
    void Start()
    {
        Application.targetFrameRate = 60;
        promptText.text = ("");
        UpdateUI();
        StartCoroutine(GameLoop());
    }
    void Update()
    {
        if (minigameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = ((int)timer + 1).ToString();
            if (timer <= 0 && !minigameEnded)
            {
                LoseMG();
            }
        }
    }
    private IEnumerator GameLoop()
    {
        while (lives > 0)
        {
            if (!firstTime)
            {
                SFXController.PlaySFX("Next", 0.2f);
                yield return new WaitUntil(() => audioSource.isPlaying == false);
                firstTime = true;
            }
            NextMG();
            background.SetBool("MinigameActive", true);
            minigameActive = true;
            minigameEnded = false;
            yield return new WaitUntil(() => !minigameActive);
            if (wonMG)
            {
                SFXController.PlaySFX("WinMG", 0.2f);
                wonMG = false;
            }
            else
            {
                SFXController.PlaySFX("LossMG", 0.2f);
            }
            background.SetBool("MinigameActive", false);
            yield return new WaitForSeconds(0.5f);
            promptText.text = ("");
            timerText.text = ("");
            Destroy(currentMG);
            UpdateUI();
            if (lives > 0)
            {
                yield return new WaitUntil(() => audioSource.isPlaying == false);
                SFXController.PlaySFX("Next", 0.2f);
                yield return new WaitUntil(() => audioSource.isPlaying == false);
            }
        }
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        SFXController.PlaySFX("CompleteLoss", 0.2f);
        Debug.Log("Game Over");
    }
    public void LoseMG()
    {
        if (!minigameEnded)
        {
            StartCoroutine(WaitForTimerToFinish(false));
            minigameEnded = true;
        }
    }
    public void WinMG()
    {
        if (!minigameEnded)
        {
            StartCoroutine(WaitForTimerToFinish(true));
            minigameEnded = true;
        }
    }
    private IEnumerator WaitForTimerToFinish(bool won)
    {
        if (timer > 2)
        {
            timer = 2;
        }
        while (timer > 0)
        {
            yield return null;
        }
        minigameActive = false;
        if (won)
        {
            score++;
            wonMG = true;
        }
        else
        {
            lives--;
            score++;
        }
    }
    void NextMG()
    {
        int randomIndex = Random.Range(0, minigameDatas.Length);
        MinigameData selectedMinigame = minigameDatas[randomIndex];
        currentMG = Instantiate(selectedMinigame.minigamePrefab, gameObject.transform);
        promptText.text = selectedMinigame.minigamePrompt;
        timer = selectedMinigame.countdownTime;
    }
    void UpdateUI()
    {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }
}
