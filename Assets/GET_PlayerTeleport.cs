using UnityEngine;

public class GET_PlayerTeleport : MonoBehaviour
{
    [Header("Teleport Targets")]
    public Transform[] teleportLocations;

    private int currentLocationIndex = -1;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TeleportToRandomLocation();
        }
    }

    void TeleportToRandomLocation()
    {
        if (teleportLocations.Length < 2)
        {
            Debug.LogWarning("Need at least 2 teleport locations.");
            return;
        }

        int newIndex;
        do
        {
            newIndex = Random.Range(0, teleportLocations.Length);
        } while (newIndex == currentLocationIndex);

        currentLocationIndex = newIndex;
        Vector3 newPosition = teleportLocations[newIndex].position;

        if (characterController != null)
        {
            characterController.enabled = false; // Disable before changing position
            transform.position = newPosition;
            characterController.enabled = true; // Re-enable after move
        }
        else
        {
            transform.position = newPosition;
        }

        Debug.Log($"Teleported to location: {newIndex} at {newPosition}");
    }
}