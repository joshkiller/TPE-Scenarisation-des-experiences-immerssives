using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementPieton : MonoBehaviour
{
    public PredestrianParameters properties;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float deplacementHorizontal = Input.GetAxis("Horizontal"); // Axe horizontal du joystick
        float deplacementVertical = Input.GetAxis("Vertical"); // Axe vertical du joystick

        Vector3 deplacement = new Vector3(deplacementHorizontal, 0, deplacementVertical);

        // Normaliser le vecteur de d�placement pour qu'il ait une longueur maximale de 1
        if (deplacement.magnitude > 1)
        {
            deplacement.Normalize();
        }

        // Tourner le pi�ton dans la direction choisie
        if (deplacement != Vector3.zero)
        {
            Quaternion rotationVersNouvelleDirection = Quaternion.LookRotation(deplacement);
            transform.rotation = rotationVersNouvelleDirection;
        }

        // Ajuster la vitesse de d�placement
        deplacement *= properties.vitesseMax;

        // Effectuer la translation dans la direction choisie
        characterController.Move(deplacement * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZoneDeCollision")) // Changer "ZoneDeCollision" pour le tag appropri� de votre zone sp�cifique
        {
            S2DeplacementVoiture voitureScript = FindObjectOfType<S2DeplacementVoiture>(); // Trouver le script de d�placement de la voiture
            if (voitureScript != null)
            {
                voitureScript.AugmenterVitesse(properties.vitesseMaxAugmentee); // Appeler la m�thode pour augmenter la vitesse de la voiture
            }
        }
    }
}
