using UnityEngine;

public class Car2Controller : MonoBehaviour
{
    public float depassementSpeedKmH = 20f; // Vitesse de d�passement de la voiture 2 en km/h
    public float distanceDeSecurite = 10f; // Distance de s�curit� entre les deux voitures
    public float deplacementLateralDistance = 3f; // Distance lat�rale � gauche de la position initiale sur l'axe X
    private Vector3 depassementStartPosition; // Position de d�part du d�passement
    private Vector3 depassementTargetPosition; // Position cible pour le d�passement
    private float depassementSpeed = 0f; // Vitesse de d�passement de la voiture 2 en m/s
    private bool isDepassement = false; // Indicateur de d�passement en cours
    private bool isRabattement = false; // Indicateur de rabattement en cours
    private GameObject car1; // R�f�rence � la voiture 1
    private bool hasStartedDepassement = false;
    private float lateralProgress = 0f; // Progr�s du d�placement lat�ral
    private Vector3 rabattementStartPosition; // Position de d�part du rabattement
    private float rabattementDistance = 0f; // Distance de rabattement
    private float rabattementProgress = 0f; // Progr�s du rabattement

    private void Update()
    {
        if (isDepassement)
        {
            if (lateralProgress < deplacementLateralDistance)
            {
                // D�placement lat�ral progressif vers la gauche
                float lateralDelta = Time.deltaTime * (deplacementLateralDistance / depassementSpeed);
                transform.position -= new Vector3(lateralDelta, 0f, 0f);
                lateralProgress += lateralDelta;
            }
            else
            {
                // Augmenter la vitesse en m/s
                float car2Speed = (depassementSpeedKmH / 3.6f); // Conversion de km/h en m/s
                GetComponent<Rigidbody>().velocity = car2Speed * transform.forward;

                // Calculez la nouvelle position de la voiture 2 en fonction de la vitesse de d�passement
                float depassementDistance = car2Speed * Time.deltaTime;
                Vector3 depassementDelta = transform.forward * depassementDistance;
                transform.position += depassementDelta;

                // Si la voiture 2 a d�pass� suffisamment la voiture 1, activez le rabattement
                if (transform.position.z > car1.transform.position.z)
                {
                    isDepassement = false;
                    isRabattement = true;

                    // Initialisez le rabattement
                    rabattementStartPosition = transform.position;
                }
            }
        }

        if (isRabattement)
        {
            // R�duire progressivement la distance lat�rale
            if (lateralProgress > 0)
            {
                float lateralDelta = Time.deltaTime * (deplacementLateralDistance / depassementSpeed);
                transform.position += new Vector3(lateralDelta, 0f, 0f);
                lateralProgress -= lateralDelta;
            }

            // Garder la vitesse en m/s
            float car2Speed = (depassementSpeedKmH / 3.6f); // Conversion de km/h en m/s
            GetComponent<Rigidbody>().velocity = car2Speed * transform.forward;

            // Calculez la nouvelle position de la voiture 2 en fonction de la vitesse de d�passement
            float depassementDistance = car2Speed * Time.deltaTime;
            Vector3 depassementDelta = transform.forward * depassementDistance;
            transform.position += depassementDelta;

            // Assurez-vous que la position Z reste sup�rieure � celle de la voiture 1
            if (transform.position.z <= car1.transform.position.z)
            {
                isRabattement = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!hasStartedDepassement && other.CompareTag("spawn"))
        {
            // La "voiture 1" entre dans le d�clencheur de la "voiture 2", commencez le d�passement.
            depassementStartPosition = transform.position;
            depassementTargetPosition = depassementStartPosition + (transform.forward * distanceDeSecurite) - (3f * transform.right);

            // Trouver la voiture 1 (CarController) dans la sc�ne
            CarController car1Controller = FindObjectOfType<CarController>();

            if (car1Controller != null)
            {
                // Utiliser car1Controller pour obtenir la r�f�rence � la voiture 1
                car1 = car1Controller.gameObject;
            }
            else
            {
                Debug.LogError("CarController for car 1 not found in the scene.");
            }

            // Calculer la vitesse de la voiture 1 en km/h
            float car1SpeedKmH = car1.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;

            // Calculer la vitesse de la "voiture 2" (voiture 1 + 20 km/h)
            float car2SpeedKmH = car1SpeedKmH + 20f;

            // Convertir la vitesse en m/s
            depassementSpeed = car2SpeedKmH / 3.6f;

            isDepassement = true;
            hasStartedDepassement = true;
        }
    }

    public void SetCar1(GameObject car1)
    {
        this.car1 = car1;
    }
}
