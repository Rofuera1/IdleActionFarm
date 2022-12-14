using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Moving", menuName = "GameSettings/MovingProperty", order = 1)]
public class MovingProperty : ScriptableObject
{
    public float Sensivity;
    public Vector3 BoundsMaxCoordinates;
    public Vector3 BoundsMinCoordinates;
}
