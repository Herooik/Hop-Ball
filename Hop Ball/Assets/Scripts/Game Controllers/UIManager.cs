using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;
    [SerializeField] private TextMeshProUGUI pickUpText;
    [SerializeField] private IntReference score;
    [SerializeField] private IntReference highScore;
    [SerializeField] private IntReference pickUps;
    [Header("UI Animators")]
    [SerializeField] private Animator startGameAnim;
    [SerializeField] private Animator shopAnim;
    [SerializeField] private Animator gameplayAnim;
    [SerializeField] private Canvas endGameCanvas;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        highScoreText.text = "BEST: " + highScore.Value;
    }

    private void Update()
    {
        RefreshPlayerValues();
    }

    public void RefreshPlayerValues()
    {
        scoreText.text = score.Value.ToString();
        pickUpText.text = pickUps.Value.ToString();
    }


    public void ShowStartGameUI()
    {
        startGameAnim.SetBool("isHidden", false);

        GameManager.Instance.gameObject.SetActive(true);
        shopAnim.SetBool("isHidden", true);
    }

    public void ShowShopUI()
    {
        GameManager.Instance.gameObject.SetActive(false);
        startGameAnim.SetBool("isHidden", true);
        shopAnim.SetBool("showShop", true);
        shopAnim.SetBool("isHidden", false);
    }

    public void ShowGameplayUI()
    {
        startGameAnim.SetBool("isHidden", true);
        gameplayAnim.SetBool("isHidden", false);
    }
    
    public void ShowEndGameUI()
    {
        if (GameManager.Instance.IsHighScore)
        {
            newHighScoreText.gameObject.SetActive(true);
        }
        endGameCanvas.gameObject.SetActive(true);
    }
    
    public void RestartLevel()
    {
        ShowStartGameUI();
        SceneManager.LoadScene(0);
    }
}
