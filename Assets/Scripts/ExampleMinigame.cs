using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExampleMinigame : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] Button winButton;
    [SerializeField] Button loseButton;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        winButton.onClick.AddListener(OnWinButtonClicked);
        loseButton.onClick.AddListener(OnLoseButtonClicked);
    }
    void Update()
    {

    }
    void OnWinButtonClicked()
    {
        gameManager.WinMG();
    }

    void OnLoseButtonClicked()
    {
        gameManager.LoseMG();   
    }
}
