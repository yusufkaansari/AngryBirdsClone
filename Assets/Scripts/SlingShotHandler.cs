using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingShotHandler : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer _leftLineRenderer;
    [SerializeField] private LineRenderer _rightLineRenderer;

    [Header("Transform References")]
    [SerializeField] private Transform _leftStartPosition;
    [SerializeField] private Transform _rightStartPosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _idlePosition;

    [Header("Slingshot Stats")]
    [SerializeField] private float _maxDistance = 3.5f;

    [Header("Scripts")]
    [SerializeField] private SlingShotArea _slingShotArea;

    private Vector2 _slingShotLinesPosition;

    private bool _clickeWithinArea;

    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;
    }
    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _slingShotArea.IsWithSlingshotArea())
        {
            _clickeWithinArea = true;
        }
        if (Mouse.current.leftButton.isPressed && _clickeWithinArea)
        {
            DrawSlingShot();
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _clickeWithinArea = false;
        }
    }

    private void DrawSlingShot()
    {
        if (!_leftLineRenderer.enabled && !_rightLineRenderer.enabled)
        {
            _leftLineRenderer.enabled = true;
            _rightLineRenderer.enabled = true;
        }
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _slingShotLinesPosition = _centerPosition.position + Vector3.ClampMagnitude(touchPosition - _centerPosition.position,_maxDistance);
        SetLines(_slingShotLinesPosition);
    }

    private void SetLines(Vector3 position)
    {
        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1,_leftStartPosition.position);

        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _rightStartPosition.position);
    }
}
