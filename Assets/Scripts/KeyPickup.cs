using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [Header("Key Settings")]
    public int keyID = 1;

    [Header("Float Settings")]
    public float floatHeight = 0.3f;
    public float floatSpeed = 2f;
    public float rotateSpeed = 90f;

    [Header("UI")]
    public GameObject hintText;              // drag HintText UI object here

    private Vector3 _startPosition;
    private bool _playerNearby = false;

    void Start()
    {
        _startPosition = transform.position;
        if (hintText != null) hintText.SetActive(false);
    }

    void Update()
    {
        float newY = _startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        if (_playerNearby)
        {
            if (Input.GetKeyDown(KeyCode.E))
                Collect();
        }
    }

    void Collect()
    {
        PlayerKeyInventory.Instance.CollectKey(keyID);
        if (hintText != null) hintText.SetActive(false);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = true;
            if (hintText != null) hintText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = false;
            if (hintText != null) hintText.SetActive(false);
        }
    }
}