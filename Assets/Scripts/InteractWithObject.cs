using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public GameObject objectToInteractWith; // R�f�rence vers l'objet sp�cifique dans votre sc�ne

    private void Start()
    {
        // Utilisez objectToInteractWith pour interagir avec l'objet sp�cifique
        if (objectToInteractWith != null)
        {
            // Par exemple, d�placez cet objet vers l'objet sp�cifique
            transform.position = objectToInteractWith.transform.position;
        }
    }
}

