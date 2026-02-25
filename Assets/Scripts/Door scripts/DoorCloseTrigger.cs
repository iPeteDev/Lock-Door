using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    public DoorController door;

    void OnTriggerExit(Collider other)
    {
        // fires when player fully exits the trigger = fully passed through
        if (other.CompareTag("Player"))
            door.CloseDoor();
    }
}