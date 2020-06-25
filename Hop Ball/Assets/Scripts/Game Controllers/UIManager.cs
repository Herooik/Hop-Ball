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
    
    [SerializeField] private Animator startGameAnim;
    [SerializeField] private Animator shopAnim;
    [SerializeField] private Animator gameplayAnim;
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private TextMeshProUGUI newHighScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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
        gameplayAnim.SetBool("isHidden", true);
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
