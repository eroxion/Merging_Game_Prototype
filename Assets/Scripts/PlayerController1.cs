using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController1 : MonoBehaviour {
    [SerializeField] private LineRenderer _linePrefab;
    private GameObject _linesParent;
    private Transform _linesParentTransform;
    private const string _lineParentTag = "LinesParent";
    [SerializeField] private float _playerRadius;
    private Transform _player;
    private const string _pointTag = "Point";
    private Dictionary<GameObject, LineRenderer> _connectedPoints = new Dictionary<GameObject, LineRenderer>();
    private Controls _inputActions;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    private Vector2 _lastPosition;

    private void Start() {
        _inputActions = new Controls();
        _inputActions.Movement.Enable();
        Transform _environment = this.transform.parent.parent.transform;
        
        _linesParent = new GameObject("LinesParent_" + gameObject.GetInstanceID());
        for (int i = 0; i < _environment.transform.childCount; i++) {
            if (_environment.GetChild(i).CompareTag(_lineParentTag)) {
                _linesParentTransform = _environment.GetChild(i);
                break;
            }
        }
        _linesParent.transform.SetParent(_linesParentTransform);
        _linesParent.tag = _lineParentTag;
        _linesParentTransform = _linesParent.transform;
        _lastPosition = Vector2.zero;
    }

    private void Update() {
        _player = this.transform;
        if (_player == null) return;
        Vector2 _position = _inputActions.Movement.Walking.ReadValue<Vector2>();
        _player.transform.position += (new Vector3(_position.x, _position.y, 0f)) * _speed * Time.deltaTime;
        if (_position != Vector2.zero) {
            Quaternion _toRotate = Quaternion.LookRotation(Vector3.forward, _position);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _toRotate, _rotateSpeed * Time.deltaTime);
        }
        PointsFetching_LineInitialization();
        List<GameObject> _pointsToRemove = new List<GameObject>();
        PointsInclusion(_pointsToRemove);
        PointsExclusion(_pointsToRemove);
    }

    private void PointsExclusion(List<GameObject> _pointsToRemove) {
        foreach (GameObject _point in _pointsToRemove) {
            Destroy(_connectedPoints[_point].gameObject);
            _connectedPoints.Remove(_point);
        }
    }

    private void PointsInclusion(List<GameObject> _pointsToRemove) {
        foreach (var _keyPairValue in _connectedPoints) {
            GameObject _point = _keyPairValue.Key;
            LineRenderer _line = _keyPairValue.Value;

            UpdateLine(_point);

            if (Vector2.Distance(_player.position, _point.transform.position) > _playerRadius) {
                _pointsToRemove.Add(_point);
            }
        }
    }

    private void PointsFetching_LineInitialization() {
        Collider2D[] _colliders = Physics2D.OverlapCircleAll(_player.transform.position, _playerRadius);
        foreach (Collider2D collider in _colliders) {
            if (collider.CompareTag(_pointTag)) {
                GameObject _point = collider.gameObject;

                if (!_connectedPoints.ContainsKey(_point)) {
                    LineRenderer _line = Instantiate(_linePrefab, _linesParent.transform);
                    _connectedPoints[_point] = _line;
                    UpdateLine(_point);
                }
            }
        }
    }

    private void UpdateLine(GameObject _point) {
        if (_connectedPoints.ContainsKey(_point)) {
            LineRenderer _line = _connectedPoints[_point];
            _line.positionCount = 2;
            _line.SetPosition(0, _player.position);
            _line.SetPosition(1, _point.transform.position);
        }
    }
}
