using UnityEngine;
using TMPro;

public class ItemBase : MonoBehaviour
{
    [Header("Base Settings")]
    public int itemID = 1; // must match CollectableItem itemID

    [Header("Float after placed")]
    public float floatHeight = 0.2f;
    public float floatSpeed = 1.5f;

    [Header("UI")]
    public TMP_Text hintText;

    private bool _playerNearby = false;
    private bool _itemPlaced = false;
    private GameObject _placedItem;
    private Vector3 _baseFloatOrigin;

    void Start()
    {
        if (hintText != null) hintText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Float the placed item on the base
        if (_itemPlaced && _placedItem != null)
        {
            float newY = _baseFloatOrigin.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            _placedItem.transform.position = new Vector3(_baseFloatOrigin.x, newY, _baseFloatOrigin.z);
        }

        if (_playerNearby && !_itemPlaced)
        {
            if (ItemManager.Instance.IsCarrying(itemID))
            {
                if (hintText != null)
                {
                    hintText.gameObject.SetActive(true);
                    hintText.text = "[ Press E to place item here ]";
                }
                if (Input.GetKeyDown(KeyCode.E))
                    PlaceItem();
            }
            else
            {
                if (hintText != null)
                {
                    hintText.gameObject.SetActive(true);
                    hintText.text = "This spot is empty. Find the matching item first.";
                }
            }
        }
    }

    void PlaceItem()
    {
        _placedItem = ItemManager.Instance.PlaceItem(itemID, transform.position + Vector3.up * 0.5f);
        if (_placedItem != null)
        {
            _itemPlaced = true;
            _baseFloatOrigin = _placedItem.transform.position;
        }
        if (hintText != null) hintText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _playerNearby = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = false;
            if (hintText != null) hintText.gameObject.SetActive(false);
        }
    }
}
