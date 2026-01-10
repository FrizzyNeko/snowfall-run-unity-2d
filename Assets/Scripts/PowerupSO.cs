using UnityEngine;

[CreateAssetMenu(fileName = "Powerup", menuName = "PowerupSO")]
public class PowerupSO : ScriptableObject
{
    public enum PowerupType
    {
        Speed,
        Torque
    }

    [SerializeField] PowerupType type;     
    [SerializeField] float valueChange;
    [SerializeField] float duration;

    public PowerupType Type => type;
    public float ValueChange => valueChange;
    public float Duration => duration;

}
