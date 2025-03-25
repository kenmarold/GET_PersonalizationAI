using UnityEngine;

public class MoodAdaptiveEnvironment : MonoBehaviour
{
    public Light directionalLight;
    public AudioSource ambientMusic;
    public AudioClip calmClip;
    public AudioClip intenseClip;
    public Transform player;

    public float speedThreshold = 5f;
    public int enemiesDefeatedRecently = 0; // Hook this up to your enemy defeat logic

    private Vector3 lastPosition;
    private float currentSpeed;

    void Start()
    {
        lastPosition = player.position;
        ambientMusic.loop = true;
        ambientMusic.playOnAwake = false;
        ambientMusic.clip = calmClip;
        ambientMusic.Play();
    }

    void Update()
    {
        CalculatePlayerSpeed();
        UpdateEnvironment();
    }

    void CalculatePlayerSpeed()
    {
        currentSpeed = (player.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = player.position;
    }

    void UpdateEnvironment()
    {
        bool isIntense = currentSpeed >= speedThreshold || enemiesDefeatedRecently > 3;

        directionalLight.color = isIntense ? Color.gray : Color.yellow;

        AudioClip targetClip = isIntense ? intenseClip : calmClip;
        if (ambientMusic.clip != targetClip)
        {
            ambientMusic.clip = targetClip;
            ambientMusic.Play();
        }
    }
}
