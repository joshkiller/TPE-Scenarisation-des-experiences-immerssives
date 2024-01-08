using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class PredestrianParameters
{
    public float vitesseMax = 2.0f; // Vitesse maximale de marche du piéton
    public float vitesseMaxAugmentee = 60;
    public float DistanceDecision = 15.0f; // Distance de trigger deplacement
}
