using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance { get; private set; }

    public List<Plant> plants = new List<Plant>();

    private void Awake()
    {
        Instance = this;
    }

    public void Pause()
    {        
        foreach (Plant plant in plants)
        {
            if (plant == null) continue;
            plant.TransitionToDisable();
        }
    }
    public void RemovePlant(Plant plant)
    {
        plants.Remove(plant);
    }
}
