using UnityEngine;

public class PedestrianController : MonoBehaviour
{
    public Transform pointA; // Point de d�part du pi�ton
    public Transform pointB; // Point d'arriv�e du pi�ton
    public S1ParameterPredestrian properties;

    private bool isWalking = false;

    private void Update()
    {
        if (isWalking)
        {
            // D�placez le pi�ton vers le point B
            float step = properties.walkingSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);

            // Une fois arriv� au point B, arr�tez le mouvement
            if (transform.position == pointB.position)
            {
                isWalking = false;
            }
        }
    }

    public void StartWalking()
    {
        // Activez le mouvement du pi�ton en commen�ant � marcher du point A vers le point B
        isWalking = true;
    }
}
