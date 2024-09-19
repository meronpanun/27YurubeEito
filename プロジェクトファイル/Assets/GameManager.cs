using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("�K�v�ȃR���|�[�l���g��o�^")]
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    WallSpawner wallSpawner;
    [SerializeField]
    Text mileageText;  // ���s����(m)
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text highScoreText;
    [SerializeField]
    GameObject gameOverCanvas;

    [Header("�Q�[���ݒ�")]
    [SerializeField]
    int scoreDigits = 8;
    [SerializeField, Tooltip("1���[�g�������b�ő��邩")]
    float secondsPerMeter = 0.075f;
    [SerializeField, Tooltip("1���[�g�����������ɉ��Z������{�X�R�A")]
    int scorePerMeter = 10;
    bool timerIsActive = false;
    int mileage = 0;    //���s����
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
        maxScore = (int)Mathf.Pow(10, scoreDigits) - 1; //�X�R�A�̍ő�l���쐬�B�Ⴆ�΁A8���Ȃ�99999999
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
        scoreText.text = "Score: " + score.ToString("D" + scoreDigits.ToString());  //ToString�ɓ���̕������n���ƁA�����Ȃǂ��w��ł���
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
        // �n�C�X�R�A�̃Z�[�u
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
