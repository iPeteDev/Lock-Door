using UnityEngine;

public class KeyHoldDisplay : MonoBehaviour
{
    [Header("Key Models — assign in order 1 to 4")]
    public GameObject[] keyModels;

    void Start()
    {
        Debug.Log("KeyHoldDisplay START — Total models assigned: " + keyModels.Length);
        for (int i = 0; i < keyModels.Length; i++)
        {
            if (keyModels[i] == null)
                Debug.LogError("KeyModel slot " + i + " is NULL!");
            else
                Debug.Log("KeyModel slot " + i + " = " + keyModels[i].name);
        }
    }

    public void ShowKey(int keyID)
    {
        Debug.Log("ShowKey called with keyID: " + keyID);
        int index = keyID - 1;

        if (keyModels == null || keyModels.Length == 0)
        {
            Debug.LogError("Key Models array is empty!");
            return;
        }

        if (index >= 0 && index < keyModels.Length)
        {
            if (keyModels[index] == null)
            {
                Debug.LogError("keyModels[" + index + "] is null!");
                return;
            }
            keyModels[index].SetActive(true);
            Debug.Log("SUCCESS — Key model " + keyID + " is now active!");
        }
        else
        {
            Debug.LogError("Index " + index + " out of range! Array length: " + keyModels.Length);
        }
    }

    public void HideKey(int keyID)
    {
        int index = keyID - 1;
        if (index >= 0 && index < keyModels.Length && keyModels[index] != null)
            keyModels[index].SetActive(false);
    }

    public void HideAll()
    {
        foreach (GameObject key in keyModels)
            if (key != null)
                key.SetActive(false);
    }
}