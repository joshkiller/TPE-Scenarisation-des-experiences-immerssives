using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    public GameObject objectToInteractWith; // Référence vers l'objet spécifique dans votre scène

    private void Start()
    {
        // Utilisez objectToInteractWith pour interagir avec l'objet spécifique
        if (objectToInteractWith != null)
        {
            // Par exemple, déplacez cet objet vers l'objet spécifique
            transform.position = objectToInteractWith.transform.position;
        }
    }
}

