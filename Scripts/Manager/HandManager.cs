using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

    public List<Plant> plantPrefabList;
    private Plant currentPlant;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        FollowCursor();
    }
    public bool AddPlant(PlantType plantType)
    {
        if (currentPlant != null) return false;
        Plant plantPrefab = GetPlantPrefab(plantType);
        if (plantPrefab==null)
        {
            print("要种植的植物不存在");
            return false;
        }
        currentPlant=GameObject.Instantiate(plantPrefab);       
        return true;
    }

    private Plant GetPlantPrefab(PlantType plantType)
    {
        foreach (Plant plant in plantPrefabList)
        {
            if (plant.plantType==plantType)
            {
                return plant;
            }
        }
        return null;
    }

    void FollowCursor()
    {
        if (currentPlant == null) return;       
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;   
    currentPlant.transform.position = mouseWorldPosition;
    }

    public void OnCellClick(Cell cell)
    {
        if (currentPlant == null) return;
        bool isSuccess = cell.AddPlant(currentPlant);
        if (isSuccess)
        {
            currentPlant = null;
        }
    }
}
