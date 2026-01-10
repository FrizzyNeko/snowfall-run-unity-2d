using System.Diagnostics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 15f;
    [SerializeField] float baseSpeed = 20f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] ParticleSystem powerUpParticles;
    [SerializeField] ScoreManager scoreManager;


    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    SurfaceEffector2D surfaceEffector2D;

    private Vector2 moveVector;
    bool canControlPlayer = true;
    float previousRotation;
    float totalRotation;
    int activePowerUpCount;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();

        if (canControlPlayer)
        {
            RotatePlayer();
            BoostPlayer();
            CalculateFlips();
        }
    }

    private void RotatePlayer()
    {
        myRigidbody2D.AddTorque(moveVector.x * -torqueAmount);
    }


    void BoostPlayer()
    {
        if (moveVector.y > 0.5f)
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);

        if (totalRotation > 340 || totalRotation < -340)
        {
            totalRotation = 0;
            scoreManager.AddScore(100);
        }

        previousRotation = currentRotation;
    }

    public void DisableControls()
    {
        canControlPlayer = false;
    }

    public void ActivatePowerUp(PowerupSO powerup)
    {
        powerUpParticles.Play();
        activePowerUpCount += 1;

        switch (powerup.Type)
        {
            case PowerupSO.PowerupType.Speed:
                baseSpeed += powerup.ValueChange;
                boostSpeed += powerup.ValueChange;
                break;

            case PowerupSO.PowerupType.Torque:
                torqueAmount += powerup.ValueChange;
                break;
        }
    }

    public void DeactivatePowerUp(PowerupSO powerup)
    {
        activePowerUpCount -= 1;
        if (activePowerUpCount == 0) powerUpParticles.Stop();

        switch (powerup.Type)
        {
            case PowerupSO.PowerupType.Speed:
                baseSpeed -= powerup.ValueChange;
                boostSpeed -= powerup.ValueChange;
                break;

            case PowerupSO.PowerupType.Torque:
                torqueAmount -= powerup.ValueChange;
                break;
        }
    }
}
