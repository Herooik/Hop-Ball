using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private IntReference highScore;
    [SerializeField] private IntReference score;
    [SerializeField] private IntReference pickUps;
    
    private int IsFirst;
    void Start ()
    {
        IsFirst = PlayerPrefs.GetInt("IsFirst") ;
        
        if (IsFirst == 0)
        {
            score.Value = 0;
            highScore.Value = 0;
            pickUps.Value = 0;
            PlayerPrefs.SetInt("IsFirst", 1);
        } 
        else
        {
            Debug.Log("welcome again!");
        }
    }
}
