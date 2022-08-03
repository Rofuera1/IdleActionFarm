using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "GameSettings/StatsProperty", order = 2)]
public class StatsProperty : ScriptableObject
{
    public int PlayerMaxGrassCapacity;
    public int GoldForSingleGrass;
    public float SecondsForGrassToRegrow;
}
