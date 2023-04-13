using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;

    private List<Transform> _segments;
    [SerializeField] private Transform segmentPrefab;
    [SerializeField] private int initialSize = 4;

    private void Start()
    {
        _segments = new List<Transform>
        {
            transform
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        //using Round to keep the snake aligned to the grid 
        var position = this.transform.position;
        position = new Vector3(
            MathF.Round(position.x) + MathF.Round(_direction.x),
            MathF.Round(position.y) + MathF.Round(_direction.y),
            0.0f
        );
        this.transform.position = position;
    }

    private void Grow()
    {
        Transform segemnt = Instantiate(this.segmentPrefab);
        segemnt.position = _segments[_segments.Count - 1].position;
        _segments.Add(segemnt);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Food"))
        {
            Grow();
        }
        else if (col.CompareTag("Obstacle"))
        {
            ResetState();
        }


    }

    private void ResetState()
    {
        _direction = Vector2.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        _segments.Clear();
        _segments.Add(transform);
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
    }


}
