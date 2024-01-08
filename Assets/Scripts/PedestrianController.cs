using UnityEngine;

public class PedestrianController : MonoBehaviour
{
    public Transform pointA; // Point de départ du piéton
    public Transform pointB; // Point d'arrivée du piéton
    public S1ParameterPredestrian properties;

    private bool isWalking = false;

    private void Update()
    {
        if (isWalking)
        {
            // Déplacez le piéton vers le point B
            float step = properties.walkingSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);

            // Une fois arrivé au point B, arrêtez le mouvement
            if (transform.position == pointB.position)
            {
                isWalking = false;
            }
        }
    }

    public void StartWalking()
    {
        // Activez le mouvement du piéton en commençant à marcher du point A vers le point B
        isWalking = true;
    }
}
