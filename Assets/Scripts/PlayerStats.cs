using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private TMP_Text _nameplate;
    [SerializeField] private string _elementName;
    [SerializeField] private int _elementId;

    private void Start() {
        SetStats(_elementName, _elementId);
    }

    internal void SetStats(string _eName, int _eId) {
        _nameplate = this.transform.GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>();
        if (_nameplate != null) {
            if (_eName != null) {
                this._elementName = _eName;
                this._elementId = _eId;
                _nameplate.SetText(_eName);
            }
            else Debug.LogWarning("Element name not fetched.");
        }
        else Debug.LogWarning("No nameplate referenced.");
    }

    internal int GetId() {
        return _elementId;
    }

    internal string GetName() {
        return _elementName;
    }

}
