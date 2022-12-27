using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collumn : MonoBehaviour
{
    [SerializeField] private Transform collumButton;
    [SerializeField] private Transform collumTop;

    [SerializeField] private float speed;

    private void Move()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;
    }

    public void SetGap(float gap)
    {
        float basePosition = 4f; //No Gap

        Vector3 collumTopPosition = new(transform.position.x, basePosition + (.5f * gap));
        Vector3 collumButtonPosition = new(transform.position.x, -(basePosition + (.5f * gap)));

        collumButton.position = collumButtonPosition;
        collumTop.position = collumTopPosition;
    }

    public void Stop()
    {
        speed = 0f;
    }
}
