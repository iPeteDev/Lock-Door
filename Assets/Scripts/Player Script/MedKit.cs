using UnityEngine;
using TMPro;

public class MedKit : MonoBehaviour
{
    [Header("Medkit Settings")]
    public float healAmount = 40f;

    [Header("Float Settings")]
    public float floatHeight = 0.2f;
    public float floatSpeed = 2f;
    public float rotateSpeed = 60f;

    [Header("UI")]
    public TMP_Text hintText;

    private Vector3 _startPosition;
    private bool _playerNearby = false;
    private bool _collected = false;

    void Start()
    {
        _startPosition = transform.position;
        if (hintText != null)
            hintText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_collected) return;

        float newY = _startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        if (_playerNearby && Input.GetKeyDown(KeyCode.E))
            Collect();
    }

    void Collect()
    {
        _collected = true;
        _playerNearby = false;

        PlayerInventory.Instance.AddMedKit();

        if (hintText != null)
            hintText.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_collected) return;
        if (other.CompareTag("Player"))
        {
            _playerNearby = true;
            if (hintText != null)
            {
                hintText.gameObject.SetActive(true);
                hintText.text = "[ Press E to pick up Medkit ]";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = false;
            if (hintText != null)
                hintText.gameObject.SetActive(false);
        }
    }
}

