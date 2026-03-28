using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour


{
    public static GameManager Instance;

    [Header("Keycard Spawn Indexes")]
    public int redKeycardIndex = -1;
    public int yellowKeycardIndex = -1;
    public int blueKeycardIndex = -1;

    [Header("Trapdoor States")]
    public bool redTrapdoorOpen = false;
    public bool blueTrapdoorOpen = false;
    public bool yellowTrapdoorOpen = false;

    [Header("Keycard Collected")]
    public bool redKeycardPickedUp = false;
    public bool yellowKeycardPickedUp = false;
    public bool blueKeycardPickedUp = false;


    [System.Serializable]
    public class BatterySavedData
    {
        public Vector3 position;
        public bool pickedUp;
        public BatterySavedData(Vector3 pos)
        {
            position = pos;
            pickedUp = false;
        }
    }

    [Header("Battery Save Data")]
    public List<BatterySavedData> batteryData = new List<BatterySavedData>();
    public bool batteriesGenerated = false;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
    }

    public void SaveBatterySpawns(List<Vector3> positions)
    {
        batteryData.Clear();

        for (int i = 0; i < positions.Count; i++)
        {
            batteryData.Add(new BatterySavedData(positions[i]));
        }

        batteriesGenerated = true;
    }

    public void MarkBatteryPickedUp(int index)
    {
        if (index >= 0 && index < batteryData.Count)
        {
            batteryData[index].pickedUp = true;
        }
    }
}
