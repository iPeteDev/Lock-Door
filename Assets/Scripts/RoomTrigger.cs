using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [Header("Room Settings")]
    public int roomIndex = 0;                // 0 = room1, 1 = room2, 2 = room3, 3 = room4

    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _spawnManager.SetRoom(roomIndex);
            Debug.Log("Player entered room: " + (roomIndex + 1));
        }
    }

}
