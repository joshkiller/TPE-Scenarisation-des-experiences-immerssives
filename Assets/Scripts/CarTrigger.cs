using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {

            Debug.Log("pieton");
            // Déclenchez le mouvement du piéton en appelant une méthode
            PedestrianController pedestrianController = FindObjectOfType<PedestrianController>(); // Assurez-vous d'avoir une référence au script du piéton
            if (pedestrianController != null)
            {
                pedestrianController.StartWalking();
            }
        }
    }
}
