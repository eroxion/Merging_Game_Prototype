using System.Collections.Generic;
using UnityEngine;

public class ElementsManager : MonoBehaviour
{
    [SerializeField] private string[] _items;
    private List<string> _allItems = new List<string>();
    

    private void Start() {
        for (int i = 0; i < _items.Length; i++) {
            Element _newElement = new Element(_items[i], i);
            string json = JsonUtility.ToJson(_newElement);
            _allItems.Add(json);
        }
    }

    internal string GetItemById(int _elementId) {
        return _allItems[_elementId];
    }

    internal string[] GetAllItems() {
        return _items;
    }
}
