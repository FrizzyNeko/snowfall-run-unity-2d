using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float restartDelay = 1f;
    [SerializeField] ParticleSystem crashParticles;

    PlayerController playerController;

    void Awake()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int floorLayer = LayerMask.NameToLayer("Floor");

        if (collision.gameObject.layer == floorLayer)
        {
            playerController.DisableControls();
            crashParticles.Play();
            Invoke(nameof(ReloadScene), restartDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
