using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("必要なコンポーネントを登録")]
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    WallSpawner wallSpawner;
    [SerializeField]
    Text mileageText;  // 走行距離(m)
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text highScoreText;
    [SerializeField]
    GameObject gameOverCanvas;

    [Header("ゲーム設定")]
    [SerializeField]
    int scoreDigits = 8;
    [SerializeField, Tooltip("1メートルを何秒で走るか")]
    float secondsPerMeter = 0.075f;
    [SerializeField, Tooltip("1メートル走った時に加算される基本スコア")]
    int scorePerMeter = 10;
    bool timerIsActive = false;
    int mileage = 0;    //走行距離
    int maxScore = 0;
    int score = 0;
    int highScore = 0;
    Vector2 playerSpawnPosition;
    Coroutine timer;
    public int Score
    {
        set
        {
            score = Mathf.Clamp(value, 0, maxScore);
            if (score > highScore)
            {
                highScore = score;
            }
            UpdateScoreUi();
        }
        get
        {
            return score;
        }
    }
    public int Mileage
    {
        get
        {
            return mileage;
        }
        set
        {
            mileage = Mathf.Max(value, 0);
            UpdateMileageUi();
        }
    }
    readonly string highScoreKey = "highScore";
    public bool IsActive { get; set; } = true;
    void Start()
    {
        playerSpawnPosition = playerTransform.position;
        InitGame();
        IsActive = true;
    }
    void Update()
    {
        if (!IsActive)
        {
            return;
        }
        if (!timerIsActive)
        {
            timer = StartCoroutine(nameof(MileageTimer));
        }
    }
    public void StartGame()
    {
        playerTransform.position = playerSpawnPosition;
        playerTransform.gameObject.SetActive(true);
        InitGame();
        wallSpawner.IsActive = true;
        IsActive = true;
    }
    void InitGame()
    {
        Mileage = 0;
        maxScore = (int)Mathf.Pow(10, scoreDigits) - 1; //スコアの最大値を作成。例えば、8桁なら99999999
        score = 0;
        timerIsActive = false;
        UpdateScoreUi();
        wallSpawner.InitSpawner();
    }
    IEnumerator MileageTimer()
    {
        timerIsActive = true;
        Mileage++;
        Score += scorePerMeter;
        yield return new WaitForSeconds(secondsPerMeter);
        timerIsActive = false;
    }
    void UpdateMileageUi()
    {
        mileageText.text = mileage.ToString() + "m";
    }
    void UpdateScoreUi()
    {
        scoreText.text = "Score: " + score.ToString("D" + scoreDigits.ToString());  //ToStringに特定の文字列を渡すと、桁数などを指定できる
        highScoreText.text = "High: " + highScore.ToString("D" + scoreDigits.ToString());
    }
    public void GameOver()
    {
        playerTransform.gameObject.SetActive(false);
        wallSpawner.IsActive = false;
        gameOverCanvas.SetActive(true);

        if (timer != null)
        {
            StopCoroutine(nameof(MileageTimer));
            timerIsActive = false;
        }
        IsActive = false;
        // ハイスコアのセーブ
        if (highScore > PlayerPrefs.GetInt(highScoreKey, 0))
        {
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }
    public void Retry()
    {
        gameOverCanvas.SetActive(false);
        StartGame();
    }
}
