using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Plant currentPlant;
    private void OnMouseDown()
    {
        HandManager.Instance.OnCellClick(this);
    }

    public bool AddPlant(Plant plant)
    {
        if (currentPlant != null) return false;
        AudioManager.Instance.PlayRandom("plant",2);
        currentPlant = plant;
        currentPlant.transform.position = transform.position;
        plant.TransitionToEnable();
        PlantManager.Instance.plants.Add(plant);
        return true;        
    }
    
           
}
