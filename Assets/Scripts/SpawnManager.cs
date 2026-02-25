using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points — assign in order Room 1 to 4")]
    public Transform[] spawnPoints;          // drag Room1Spawn, Room2Spawn etc here

    private int _currentRoom = 0;            // starts at room 1 (index 0)

    public void SetRoom(int roomIndex)
    {
        _currentRoom = roomIndex;
        Debug.Log("Spawn point updated to room: " + (roomIndex + 1));
    }

    public void Respawn(GameObject player)
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        // must disable CharacterController before moving player
        if (cc != null) cc.enabled = false;

        player.transform.position = spawnPoints[_currentRoom].position;
        player.transform.rotation = spawnPoints[_currentRoom].rotation;

        if (cc != null) cc.enabled = true;

        Debug.Log("Player respawned at room: " + (_currentRoom + 1));
    }
}
