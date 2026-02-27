using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [Header("Settings")]
    public int totalItemsNeeded = 5;

    [Header("The small hidden room door in Room 2")]
    public DoorController hiddenRoomDoor; // drag the small room door here

    [Header("Key 2 inside the hidden room")]
    public GameObject key2Object; // drag Key 2 here, disable it in scene first

    private Dictionary<int, GameObject> _carriedItems = new Dictionary<int, GameObject>();
    private int _placedCount = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PickUpItem(int itemID, GameObject itemObj)
    {
        if (!_carriedItems.ContainsKey(itemID))
            _carriedItems[itemID] = itemObj;
        Debug.Log("Carrying item " + itemID);
    }

    public bool IsCarrying(int itemID)
    {
        return _carriedItems.ContainsKey(itemID);
    }

    public GameObject PlaceItem(int itemID, Vector3 position)
    {
        if (!_carriedItems.ContainsKey(itemID)) return null;

        GameObject obj = _carriedItems[itemID];
        _carriedItems.Remove(itemID);

        obj.SetActive(true);
        obj.transform.position = position;

        // Disable so it cant be picked up again
        Collider col = obj.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        CollectableItem ci = obj.GetComponent<CollectableItem>();
        if (ci != null) ci.enabled = false;

        _placedCount++;
        Debug.Log("Placed item " + itemID + " | Total placed: " + _placedCount);

        if (_placedCount >= totalItemsNeeded)
            OnAllItemsPlaced();

        return obj;
    }

    void OnAllItemsPlaced()
    {
        Debug.Log("All 5 items placed! Hidden room unlocked!");

        // Auto open the hidden room door permanently
        if (hiddenRoomDoor != null)
            hiddenRoomDoor.OpenPermanently();
        else
            Debug.LogError("Hidden room door not assigned in ItemManager!");

        // Activate Key 2 inside the hidden room
        if (key2Object != null)
            key2Object.SetActive(true);
        else
            Debug.LogError("Key 2 object not assigned in ItemManager!");
    }
}
