using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {

            Debug.Log("pieton");
            // D�clenchez le mouvement du pi�ton en appelant une m�thode
            PedestrianController pedestrianController = FindObjectOfType<PedestrianController>(); // Assurez-vous d'avoir une r�f�rence au script du pi�ton
            if (pedestrianController != null)
            {
                pedestrianController.StartWalking();
            }
        }
    }
}
