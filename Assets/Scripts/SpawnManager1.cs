using System.Collections;
using UnityEngine;

public class SpawnManager1 : MonoBehaviour
{
    public GameObject voiturePrefab; // Pr�fab de la voiture � instancier
    public Transform pointSpawn; // Position de spawn initial
    public float intervalleSpawn = 5.0f; // Intervalle de spawn en secondes
    public float distanceMaximale = 500.0f; // Distance maximale avant de d�truire la voiture

    void Start()
    {
        StartCoroutine(SpawnerVoiture());
    }

    IEnumerator SpawnerVoiture()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalleSpawn);

            // Instancier la voiture � la position de spawn
            GameObject nouvelleVoiture = Instantiate(voiturePrefab, pointSpawn.position, Quaternion.identity);

            // D�marrer la coroutine pour surveiller la distance de la voiture
            StartCoroutine(DetruireSiTropLoin(nouvelleVoiture.transform));
        }
    }

    IEnumerator DetruireSiTropLoin(Transform voitureTransform)
    {
        Vector3 positionInitiale = voitureTransform.position;

        while (true)
        {
            // Attendre avant de v�rifier � nouveau la distance
            yield return new WaitForSeconds(1.0f);

            // Calculer la distance entre la position actuelle et la position initiale
            float distance = Vector3.Distance(voitureTransform.position, positionInitiale);

            if (distance > distanceMaximale)
            {
                // Si la distance d�passe la distance maximale, d�truire la voiture
                Destroy(voitureTransform.gameObject);
                break; // Sortir de la boucle
            }
        }
    }
}
