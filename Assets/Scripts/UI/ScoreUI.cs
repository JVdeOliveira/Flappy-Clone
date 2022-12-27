using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro.enabled = false;
    }

    private void Start()
    {
        Level.Instance.OnScoreChanged += Level_OnScoreChanged;
        Level.Instance.OnGameStarted += Level_OnGameStarted;
        Level.Instance.OnGameOver += Level_OnGameOver;
    }

    private void Level_OnGameOver(object sender, System.EventArgs e)
    {
        textMeshPro.enabled = false;
    }

    private void Level_OnGameStarted(object sender, System.EventArgs e)
    {
        textMeshPro.enabled = true;
    }

    private void Level_OnScoreChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        textMeshPro.text = Level.Instance.ScoreAmount.ToString();
    }
}
