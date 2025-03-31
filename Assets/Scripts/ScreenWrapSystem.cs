using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapSystem : MonoBehaviour
{
    [SerializeField]
    GameObject[] _objects;

    [SerializeField]
    private Vector2 _lowerBounds = new Vector2(-50, -50);

    [SerializeField]
    private Vector2 _upperBounds = new Vector2(50, 50);

    private void FixedUpdate()
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            Vector3 newPosition = _objects[i].transform.position;

            if (newPosition.x < _lowerBounds.x)
                newPosition.x = _upperBounds.x - 0.5f;

            if (newPosition.y < _lowerBounds.y)
                newPosition.y = _upperBounds.y - 0.5f;

            if (newPosition.x > _upperBounds.x)
                newPosition.x = _lowerBounds.x + 0.5f;

            if (newPosition.y > _upperBounds.y)
                newPosition.y = _lowerBounds.y + 0.5f;

            _objects[i].transform.position = newPosition;
        }
    }
}
