using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroSupported;

    // Seuils pour différents états
    private float noMovementThreshold = 0.02f; // Seuil pour détecter l'absence totale de mouvement/rotation
    private float vibrationStressThreshold = 0.2f; // Seuil pour les petites vibrations (stress)
    private float normalMovementThreshold = 7f; // Seuil pour le mouvement physique normal (marcher/courir)
    private float stressDurationThreshold = 3f; // Temps requis pour confirmer l'état de stress

    private Vector3 previousMovement;
    private float stressTimer = 0f;

    void Start()
    {
        Debug.Log("Initialisation du gyroscope...");
        gyroSupported = SystemInfo.supportsGyroscope;

        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            Debug.Log("Le gyroscope est activé.");
        }
        else
        {
            Debug.LogError("Le gyroscope n'est pas pris en charge sur cet appareil. Utilisation de l'accéléromètre.");
        }

        previousMovement = Vector3.zero;
    }

    void Update()
    {
        Vector3 currentMovement = gyroSupported ? gyro.rotationRateUnbiased : Input.acceleration;
        DetectState(currentMovement);
    }

    void DetectState(Vector3 currentMovement)
    {
        // Calculer le changement de mouvement
        Vector3 deltaMovement = currentMovement - previousMovement;
        previousMovement = currentMovement;

        float magnitude = deltaMovement.magnitude;

        // Si aucun mouvement ou rotation n'est détecté, c'est un état de calme
        if (magnitude < noMovementThreshold)
        {
            Debug.Log("calme");
            stressTimer = 0f;
        }
        // Vérifier les petites vibrations pour détecter le stress
        else if (magnitude < vibrationStressThreshold)
        {
            stressTimer += Time.deltaTime;

            if (stressTimer > stressDurationThreshold)
            {
                Debug.Log("stressé");
            }
            else
            {
                Debug.Log("stressé");
            }
        }
        // Si le mouvement est grand et régulier, c'est un mouvement physique normal
        else if (magnitude >= normalMovementThreshold)
        {
            Debug.Log("marcher ou courir");
            stressTimer = 0f;
        }
        // Si le mouvement est entre les deux, c'est un mouvement normal
        else
        {
            Debug.Log("marcher ou courir");
            stressTimer = 0f;
        }
    }
}
