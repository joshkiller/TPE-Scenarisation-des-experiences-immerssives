using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class S1CarControllerParameters 
{
    public float motorForce = 1000f; // Force du moteur
    public float brakeForce = 500f;  // Force de freinage
    public float distanceArret = 50f; // Distance d'arret du vehicule
    public float vitesseDepart;
    public float vitesseAAtteindre;
}
