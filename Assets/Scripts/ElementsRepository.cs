using TMPro;
using UnityEngine;

public class ElementsRepository : MonoBehaviour
{
    [SerializeField] private GameObject[] _slots;
    private int _slotsAllowed;
    private TMP_Text _elementName;
    private string _elementString;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _playerParent;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ElementsManager _elementsManager;
    private PlayerStats _slotStats;

    private void Start() {
        _slotsAllowed = _elementsManager.GetAllItems().Length;
        foreach (GameObject _slot in _slots) {
            _slot.SetActive(false);
        }
        for (int i = 0; i < _slotsAllowed; i++) {
            _slotStats = _slots[i].transform.GetComponent<PlayerStats>();
            _elementString = _elementsManager.GetItemById(i);
            _slots[i].SetActive(true);
            _elementName = _slots[i].GetComponentInChildren<TMP_Text>();
            _elementName.SetText(_elementString);
            _slotStats.SetStats(_elementString, i);
        }
    }

    public void InstantiateElement(int clickedIndex) {
        _slotsAllowed = _elementsManager.GetAllItems().Length;
        GameObject _newPlayer = Instantiate(_player, _spawnPoint.transform.position, Quaternion.identity, _playerParent);
        int _elementId = _slots[clickedIndex].transform.GetComponent<PlayerStats>().GetId();
        _elementString = _elementsManager.GetItemById(_elementId);
        _newPlayer.GetComponent<PlayerStats>().SetStats(_elementString, _elementId);
    }
}
