using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private enum Difficulty
    {
        Easy,
        Normal,
        High,
        Impossible
    }

    [SerializeField] private Bird bird;
    [SerializeField] private Transform collumnPrefab;

    private float delay = 1.25f;
    private float currentTime;

    private float collumnGapSize;
    private List<Collumn> collumnList;
    private int collumnCountCreate;

    private bool isActive = false;

    private void Awake()
    {
        collumnList = new List<Collumn>();
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
            y = Random.Range(-yPositionRange, yPositionRange)
        };

        Collumn collumn = Instantiate(collumnPrefab, CratePosition, Quaternion.identity).GetComponent<Collumn>();

        collumn.SetGap(collumnGapSize);
        collumn.transform.SetParent(transform);

        collumnList.Add(collumn);
        collumnCountCreate++;
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
    }
}
