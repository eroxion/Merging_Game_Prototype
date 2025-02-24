using UnityEngine;

public class MergeSpawner : MonoBehaviour
{
    private GameObject _playersParent;
    [SerializeField] private GameObject _player;
    [SerializeField] private ElementsManager _elementsManager;
    private PlayerStats _playerStats;
    internal void CreateNewBlock(Vector2 _position, int _IDsum) {
        _elementsManager = GameObject.FindWithTag("ElementsManager").GetComponentInChildren<ElementsManager>();
        _playersParent = GameObject.FindWithTag("PlayersParent");
        if (_playersParent == null ) Debug.Log("NULL");
        Instantiate(_player, _position, Quaternion.identity, _playersParent.transform);
        _playerStats = _player.GetComponent<PlayerStats>();
        _playerStats.SetStats(_elementsManager.GetItemById(_IDsum), _IDsum);
    }
}
