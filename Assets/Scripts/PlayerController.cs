using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Vector2 _offset = Vector2.zero;
    private bool _isDragging = false;
    [SerializeField] private LineRenderer _linePrefab;
    private GameObject _linesParent;
    private Transform _linesParentTransform;
    private const string _lineParentTag = "LinesParent";
    [SerializeField] private float _playerRadius;
    private Transform _player;
    private const string _pointTag = "Point";
    private Dictionary<GameObject, LineRenderer> _connectedPoints = new Dictionary<GameObject, LineRenderer>();
    private bool _overCollider = false;
    [SerializeField] private MergeSpawner _mergeSpawner;
    private Transform _newBlockTransform = null;
    private GameObject _collisionObject1 = null;
    private GameObject _collisionObject2 = null;

    private void Start() {
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
        
    }

    private void Update() {
        _player = this.transform;
        if (_player == null) return;
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

    private void OnMouseDown() {
        _isDragging = true;
        Vector2 _curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 _screenToWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(_curScreenPoint).x, Camera.main.ScreenToWorldPoint(_curScreenPoint).y);
        Vector2 _thisTransform = new Vector2(this.transform.position.x, this.transform.position.y);
        _offset = _thisTransform - _screenToWorldPoint;
    }

    private void OnMouseDrag() {
        if (_isDragging) {
            Vector2 _curScreenPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 _screenToWorldPoint = new Vector2(Camera.main.ScreenToWorldPoint(_curScreenPoint).x, Camera.main.ScreenToWorldPoint(_curScreenPoint).y);
            transform.position = _screenToWorldPoint + _offset;
        }
    }

    private void OnMouseUp() {
        _isDragging = false;
        if (_overCollider) {
            Destroy(_collisionObject1);
            Destroy(_collisionObject2);
            _collisionObject1.GetComponent<PlayerController>()._DestroyLines();
            _collisionObject2.GetComponent<PlayerController>()._DestroyLines();
            int _resultantID = _collisionObject1.GetComponent<PlayerStats>().GetId() + _collisionObject2.GetComponent<PlayerStats>().GetId();
            _mergeSpawner.CreateNewBlock(_newBlockTransform.position, _resultantID);
            _resetNewBlockInfo();
        }
    }

    internal void _DestroyLines() {
        Destroy(_linesParent);
    }

    internal void _resetNewBlockInfo() {
        _overCollider = false;
        this._newBlockTransform = null;
        this._collisionObject1 = null;
        this._collisionObject2 = null;
    }

    internal void SendNewBlockInfo(Transform _newBlockTransform, GameObject _collisionObject1, GameObject _collisionObject2) {
        this._newBlockTransform = _newBlockTransform;
        this._collisionObject1 = _collisionObject1;
        this._collisionObject2 = _collisionObject2;
    }

    internal void SetOverColliderTrue() {
        _overCollider = true;
    }

    internal bool IsDragging() {
        return _isDragging;
    }
}
