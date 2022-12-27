using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private const float X_START_POSITION = 11.5f;
    private const float X_END_POSITION = -13.5f;

    public void Move(float speed)
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        if (transform.position.x <= X_END_POSITION)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = X_START_POSITION;
            transform.position = newPosition;
        }
    }
}
