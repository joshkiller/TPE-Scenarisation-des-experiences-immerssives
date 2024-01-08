using UnityEngine;

public class CarController : MonoBehaviour
{
    public S1CarControllerParameters properties;
    public float steerForce = 10f;   // Force de direction
    public float maxSteerAngle = 30f; // Angle de direction maximum
   

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private bool isBraking = false; // Variable pour le freinage

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Lire les entr�es de l'utilisateur � partir des joysticks VR
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Lire l'�tat du bouton de freinage � partir de l'entr�e du joystick
        isBraking = Input.GetButton("Fire1"); // Assurez-vous que "BrakeButton" correspond au bouton configur� dans l'Input Manager
    }

    private void FixedUpdate()
    {
        // Appliquer la force du moteur
        rb.AddForce(transform.forward * verticalInput * properties.motorForce);

        // Calculer l'angle de direction
        float steerAngle = maxSteerAngle * horizontalInput;

        // Appliquer la force de direction aux roues avant
        rb.AddTorque(transform.up * steerAngle * steerForce);

        // Appliquer la force de freinage si le bouton de freinage est enfonc�
        if (isBraking)
        {
            rb.AddForce(-rb.velocity * properties.brakeForce);
        }
    }
}
