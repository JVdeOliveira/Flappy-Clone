using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        Level.Instance.OnScoreChanged += Level_OnScoreChanged;
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
