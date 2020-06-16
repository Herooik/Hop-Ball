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
    
    [SerializeField] private Canvas startGameUI;
    [SerializeField] private Canvas shopUI;
    [SerializeField] private Canvas gameplayUI;
    [SerializeField] private Canvas endGameUI;
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
        startGameUI.gameObject.SetActive(true);

        if (shopUI.gameObject.activeInHierarchy)
        {
            GameManager.Instance.gameObject.SetActive(true);
            shopUI.gameObject.SetActive(false);
        }
    }

    public void ShowShopUI()
    {
        GameManager.Instance.gameObject.SetActive(false);
        startGameUI.gameObject.SetActive(false);
        shopUI.gameObject.SetActive(true);
    }

    public void ShowGameplayUI()
    {
        startGameUI.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(true);
    }
    
    public void ShowEndGameUI()
    {
        if (GameManager.Instance.IsHighScore)
        {
            newHighScoreText.gameObject.SetActive(true);
        }
        endGameUI.gameObject.SetActive(true);
    }
    
    public void RestartLevel()
    {
        ShowStartGameUI();
        SceneManager.LoadScene(0);
    }
}
