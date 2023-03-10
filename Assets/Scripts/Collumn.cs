using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collumn : MonoBehaviour
{
    [SerializeField] private Transform collumButton;
    [SerializeField] private Transform collumTop;

    private float speed;

    public void Move()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;
    }

    public void SetCollum(float gap, float speed)
    {
        this.speed = speed;

        float basePosition = 4f; //No Gap

        Vector3 collumTopPosition = new(0, basePosition + (.5f * gap));
        Vector3 collumButtonPosition = new(0, -(basePosition + (.5f * gap)));

        collumButton.localPosition = collumButtonPosition;
        collumTop.localPosition = collumTopPosition;
    }
}
