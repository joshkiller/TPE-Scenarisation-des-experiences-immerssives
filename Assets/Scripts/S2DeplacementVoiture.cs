using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2DeplacementVoiture : MonoBehaviour
{
    public S2ParametersDeplacementVoiture properties;
    
    private Rigidbody rb; // Composant Rigidbody de la voiture

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ConvertirVitesseKMHToMS(); // Convertir la vitesse en m/s
    }

    void ConvertirVitesseKMHToMS()
    {
        // Convertir la vitesse de km/h à m/s
        float vitesseMS = properties.vitesseKMH * 1000.0f / 3600.0f;
        rb.velocity = transform.forward * vitesseMS;
    }

    public void AugmenterVitesse(float nouvelleVitesse)
    {
        float vitesseMS = nouvelleVitesse * 1000.0f / 3600.0f;
        rb.velocity = transform.forward * vitesseMS;
    }
}
