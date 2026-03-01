using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Checkpoint ID")]
    public int checkpointID = 1;

    private bool _activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (_activated) return;
        if (other.CompareTag("Player"))
        {
            _activated = true;
            // Set this as the respawn point
            PlayerHealth.Instance.respawnPoint = transform;
            Debug.Log("Checkpoint " + checkpointID + " activated!");
        }
    }
}
