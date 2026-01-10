using UnityEngine;

public class SnowTrail : MonoBehaviour
{

    [SerializeField] ParticleSystem snowTrailParticle;
    string floorLayerName = "Floor";
    int floorLayer;

    private void Awake()
    {
        floorLayer = LayerMask.NameToLayer(floorLayerName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == floorLayer)
            snowTrailParticle.Play();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == floorLayer)
            snowTrailParticle.Stop();
    }
}


