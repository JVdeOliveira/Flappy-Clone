using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public static Level Instance { get; private set; }

    private enum Difficulty
    {
        Easy,
        Normal,
        High,
        Impossible
    }

    [SerializeField] private Bird bird;
    [SerializeField] private Transform collumnPrefab;
    [SerializeField] private Transform floorContainer;

    private float delay = 1.25f;
    private float currentTime;

    private float collumnSpeed = 5f;
    private float collumnGapSize;
    private int collumnCountCreate;

    private List<Floor> floorList;
    private List<Collumn> collumnList;

    private bool isActive = false;

    public int ScoreAmount { get; private set; }
    public event EventHandler OnScoreChanged;

    private void Awake()
    {
        Instance = this;

        collumnList = new List<Collumn>();
        floorList = new List<Floor>();

        foreach (Transform floorTransform in floorContainer)
        {
            Floor floor = floorTransform.GetComponent<Floor>();
            floorList.Add(floor);
        }

        SetDifficulty(GetDifficulty());

        bird.OnPlayStarted += Bird_OnPlayStarted;
        bird.OnDie += Bird_OnDie;
    }

    private void Bird_OnDie(object sender, System.EventArgs e)
    {
        isActive = false;
    }

    private void Bird_OnPlayStarted(object sender, System.EventArgs e)
    {
        isActive = true;
    }

    private void CrateCollumn()
    {
        float yPositionRange = 4f - (collumnGapSize / 2);

        Vector3 CratePosition = new()
        {
            x = 10f,
            y = UnityEngine.Random.Range(-yPositionRange, yPositionRange)
        };

        Collumn collumn = Instantiate(collumnPrefab, CratePosition, Quaternion.identity).GetComponent<Collumn>();

        collumn.SetCollum(collumnGapSize, collumnSpeed);
        collumn.transform.SetParent(transform);

        collumnList.Add(collumn);
        collumnCountCreate++;
    }

    public void AddScore()
    {
        ScoreAmount++;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                collumnGapSize = 5f;
                break;

            case Difficulty.Normal:
                collumnGapSize = 4f;
                break;

            case Difficulty.High:
                collumnGapSize = 3f;
                break;

            case Difficulty.Impossible:
                collumnGapSize = 2f;
                break;
        }
    }

    private Difficulty GetDifficulty()
    {
        if (collumnCountCreate >= 30) return Difficulty.Impossible;
        if (collumnCountCreate >= 20) return Difficulty.High;
        if (collumnCountCreate >= 10) return Difficulty.Normal;
        return Difficulty.Easy;
    }

    private void Update()
    {
        if (!isActive) return;

        for (int i = 0; i < collumnList.Count; i++)
        {
            Collumn collumn = collumnList[i];
            collumn.Move();

            if (collumn.transform.position.x <= -10)
            {
                collumnList.Remove(collumn);
                Destroy(collumn.gameObject);
                i--;
            }
        }

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            CrateCollumn();
            SetDifficulty(GetDifficulty());

            currentTime = delay;
        }

        foreach (Floor floor in floorList)
        {
            floor.Move(collumnSpeed);
        }
    }
}
