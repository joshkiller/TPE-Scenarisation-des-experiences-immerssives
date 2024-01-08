using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject newCarPrefab; // R�f�rence vers le mod�le de la nouvelle voiture
    public float spawnDistanceBehind = 5f; // Distance derri�re la voiture du joueur o� la nouvelle voiture appara�tra
    public GameObject voiture1; // R�f�rence � la premi�re voiture

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision d�tect�e avec : " + other.name);

        Debug.Log("trig");
        if (other.CompareTag("Car"))
        {
            Debug.Log("trigger");

            // Acc�dez � un enfant de l'objet "other" (par exemple, le premier enfant)
            Transform child = other.transform.GetChild(0);

            if (child != null)
            {
                // Calcule la position d'apparition derri�re l'enfant de la voiture du joueur
                Vector3 spawnPosition = child.position + (-child.forward * spawnDistanceBehind);
                spawnPosition.y += 5f; // Ajustez l'axe Y de la position � 0.5
                spawnPosition.z -= 15;

                Debug.Log(spawnPosition);
                // Fait appara�tre la nouvelle voiture � la position calcul�e
                GameObject newCar = Instantiate(newCarPrefab, spawnPosition, other.transform.rotation);

                // Copie de la vitesse de la premi�re voiture (voiture1) et l'appliquez � la nouvelle voiture (newCar)
                Rigidbody newCarRigidbody = newCar.GetComponent<Rigidbody>();
                Rigidbody voiture1Rigidbody = voiture1.GetComponent<Rigidbody>(); // S'Assurer de r�f�rencer correctement la premi�re voiture
                                                                                  // Obtenir la r�f�rence au script Car2Controller de la voiture 2 nouvellement instanci�e
                Car2Controller car2Controller = newCar.GetComponent<Car2Controller>();
                // Configurez la r�f�rence � la voiture 1 dans le script Car2Controller
                car2Controller.SetCar1(voiture1);

                if (newCarRigidbody != null && voiture1Rigidbody != null)
                {
                    newCarRigidbody.velocity = voiture1Rigidbody.velocity;
                }
                else
                {
                    Debug.LogWarning("La premi�re voiture ou la nouvelle voiture n'a pas de composant Rigidbody.");
                }

            }
            else
            {
                Debug.LogWarning("L'objet 'other' n'a pas d'enfant.");
            }
        }
    }
}