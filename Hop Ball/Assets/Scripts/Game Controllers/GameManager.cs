using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [SerializeField] private FloatReference movingChance;
    [SerializeField][Range(0,1)] private float startingMovingChance = 0.05f;
    [SerializeField][Range(0,1)] private float maxMovingChance = 0.6f;

    [SerializeField] private PlayerController _playerController;

    private bool _isGameStarted;
    private bool _isMovingChanceIncreased;

    public int IsFirst;

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
        if (TouchToStartGame()) return;
        
        RefreshPlayerValues();
        
        CheckForMovingChance();
    }

    private bool TouchToStartGame()
    {
        if (Input.GetMouseButtonDown(0) && !_isGameStarted)
        {
            //if (EventSystem.current.IsPointerOverGameObject()) return true;
            
            if(EventSystem.current.IsPointerOverGameObject())
                return true;
             
            //check touch
            if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began )
            {
                if(EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                    return true;
            }
            
            UIManager.Instance.ShowGameplayUI();
            _isGameStarted = true;
            _playerController.enabled = true;
        }

        return false;
    }

    public void RefreshPlayerValues()
    {
        scoreText.text = score.Value.ToString();
        coinsText.text = pickUps.Value.ToString();
    }

    private void CheckForMovingChance()
    {
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
