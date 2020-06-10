using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsHighScore { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private IntReference score;
    [SerializeField] private IntReference highScore;
    [SerializeField] private IntReference pickUps;
    
    [Header("Moving Chance")]
    [SerializeField][Range(0,1)] private float startingMovingChance = 0.05f;
    [SerializeField] private FloatReference movingChance;
    [SerializeField][Range(0,1)] private float maxMovingChance = 0.6f;

    [SerializeField] private PlayerController _playerController;

    private bool _isGameStarted;
    private bool _isMovingChanceIncreased;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        highScoreText.text = "BEST: " + highScore.Value;
        score.Value = 0;
        movingChance.Value = startingMovingChance;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isGameStarted)
        {
            UIManager.Instance.ShowGameplayUI();
            _isGameStarted = true;
            _playerController.enabled = true;
        }
        
        scoreText.text = score.Value.ToString();
        coinsText.text = pickUps.Value.ToString();

        
        if (score.Value % 10 == 0 && score.Value != 0 && !_isMovingChanceIncreased && movingChance.Value <= maxMovingChance)
        {
            _isMovingChanceIncreased = true;
            StartCoroutine(IncreasingMovingChance());
        }
    }

    private IEnumerator IncreasingMovingChance()
    {
        movingChance.Value += startingMovingChance;
        yield return new WaitForSeconds(1);
        _isMovingChanceIncreased = false;
    }

    public void CheckForHighscore()
    {
        if (score.Value > highScore.Value)
        {
            IsHighScore = true;
            highScore.Value = score.Value;
        }
    }
}
