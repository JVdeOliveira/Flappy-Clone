using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    private void Start()
    {
        Level.Instance.OnGameStarted += Level_OnGameStarted;
    }

    private void Level_OnGameStarted(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }
}
