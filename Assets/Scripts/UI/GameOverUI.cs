using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void Start()
    {
        Level.Instance.OnGameOver += Level_OnGameOver;
        gameObject.SetActive(false);
    }

    private void Level_OnGameOver(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        textMeshProUGUI.text = Level.Instance.ScoreAmount.ToString();
    }
}
