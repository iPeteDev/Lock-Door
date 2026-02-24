using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public int doorID = 1;
    public float slideDistance = 3f;
    public float slideSpeed = 4f;

    [Header("Slide Direction")]
    public Vector3 slideDirection = Vector3.right;   // change in Inspector if needed

    [Header("UI")]
    public Text hintText;

    private bool _isOpen = false;
    private bool _playerNearby = false;
    private Vector3 _closedPosition;
    private Vector3 _openPosition;

    void Start()
    {
        _closedPosition = transform.position;
        _openPosition = _closedPosition + slideDirection.normalized * slideDistance;

        if (hintText != null)
            hintText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Animate slide
        if (_isOpen)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _openPosition,
                Time.deltaTime * slideSpeed
            );
        }

        // Player nearby interaction
        if (_playerNearby && !_isOpen)
        {
            UpdateHintText();

            if (Input.GetKeyDown(KeyCode.E))
                TryOpen();
        }
    }

    void TryOpen()
    {
        if (PlayerKeyInventory.Instance.HasKey(doorID))
        {
            _isOpen = true;
            PlayerKeyInventory.Instance.UseKey(doorID);
            HideHint();
        }
    }

    void UpdateHintText()
    {
        if (hintText == null) return;
        hintText.gameObject.SetActive(true);

        if (PlayerKeyInventory.Instance.HasKey(doorID))
            hintText.text = "[E] Open Door " + doorID;
        else
            hintText.text = "Collect Key " + doorID + " to unlock this door!";
    }

    void HideHint()
    {
        if (hintText != null)
            hintText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = false;
            HideHint();
        }
    }
}