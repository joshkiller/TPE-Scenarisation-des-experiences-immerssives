using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject newCarPrefab; // Référence vers le modèle de la nouvelle voiture
    public float spawnDistanceBehind = 5f; // Distance derrière la voiture du joueur où la nouvelle voiture apparaîtra
    public GameObject voiture1; // Référence à la première voiture

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision détectée avec : " + other.name);

        Debug.Log("trig");
        if (other.CompareTag("Car"))
        {
            Debug.Log("trigger");

            // Accédez à un enfant de l'objet "other" (par exemple, le premier enfant)
            Transform child = other.transform.GetChild(0);

            if (child != null)
            {
                // Calcule la position d'apparition derrière l'enfant de la voiture du joueur
                Vector3 spawnPosition = child.position + (-child.forward * spawnDistanceBehind);
                spawnPosition.y += 5f; // Ajustez l'axe Y de la position à 0.5
                spawnPosition.z -= 15;

                Debug.Log(spawnPosition);
                // Fait apparaître la nouvelle voiture à la position calculée
                GameObject newCar = Instantiate(newCarPrefab, spawnPosition, other.transform.rotation);

                // Copie de la vitesse de la première voiture (voiture1) et l'appliquez à la nouvelle voiture (newCar)
                Rigidbody newCarRigidbody = newCar.GetComponent<Rigidbody>();
                Rigidbody voiture1Rigidbody = voiture1.GetComponent<Rigidbody>(); // S'Assurer de référencer correctement la première voiture
                                                                                  // Obtenir la référence au script Car2Controller de la voiture 2 nouvellement instanciée
                Car2Controller car2Controller = newCar.GetComponent<Car2Controller>();
                // Configurez la référence à la voiture 1 dans le script Car2Controller
                car2Controller.SetCar1(voiture1);

                if (newCarRigidbody != null && voiture1Rigidbody != null)
                {
                    newCarRigidbody.velocity = voiture1Rigidbody.velocity;
                }
                else
                {
                    Debug.LogWarning("La première voiture ou la nouvelle voiture n'a pas de composant Rigidbody.");
                }

            }
            else
            {
                Debug.LogWarning("L'objet 'other' n'a pas d'enfant.");
            }
        }
    }
}