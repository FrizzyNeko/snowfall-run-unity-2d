using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] PowerupSO powerUp;

    PlayerController player;
    SpriteRenderer spriteRenderer;
    float timeLeft;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = powerUp.Duration;
    }

    private void Update()
    {
        CountdownTimer();
    }

    void CountdownTimer()
    {
        if (spriteRenderer.enabled)
            return;

        if (timeLeft <= 0f)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            player.DeactivatePowerUp(powerUp);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if (collision.gameObject.layer == layerIndex && spriteRenderer.enabled)
        {
            spriteRenderer.enabled = false;
            player.ActivatePowerUp(powerUp);
        }
    }
}
