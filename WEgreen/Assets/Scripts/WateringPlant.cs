using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Watering-Plant", menuName = "Watering-Plant")]
public class WateringPlant : ScriptableObject
{
    // Start is called before the first frame update
    public string plantName;
    public int plantWateringIntervall;
}
