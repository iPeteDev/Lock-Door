using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerKeyInventory : MonoBehaviour
{
    public static PlayerKeyInventory Instance;

    [Header("Key HUD Slots (bottom of screen)")]
    public Image[] keySlots;                 // 4 UI Image slots, drag in order
    public Color lockedColor = new Color(1f, 1f, 1f, 0.2f);    // dim = not collected
    public Color collectedColor = new Color(1f, 0.85f, 0f, 1f); // gold = collected

    private HashSet<int> _collectedKeys = new HashSet<int>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        ResetHUD();
    }

    void ResetHUD()
    {
        for (int i = 0; i < keySlots.Length; i++)
        {
            if (keySlots[i] != null)
                keySlots[i].color = lockedColor;
        }
    }

    public void CollectKey(int keyID)
    {
        _collectedKeys.Add(keyID);
        UpdateHUD(keyID, true);
        Debug.Log("Key " + keyID + " collected!");
    }

    public bool HasKey(int keyID)
    {
        return _collectedKeys.Contains(keyID);
    }

    public void UseKey(int keyID)
    {
        _collectedKeys.Remove(keyID);
        UpdateHUD(keyID, false);
    }

    void UpdateHUD(int keyID, bool collected)
    {
        // keyID 1 = slot index 0, keyID 2 = slot index 1, etc.
        int index = keyID - 1;
        if (index >= 0 && index < keySlots.Length && keySlots[index] != null)
            keySlots[index].color = collected ? collectedColor : lockedColor;
    }
}
