using UnityEngine;


public class BombExplosionController : MonoBehaviour
{
    #region Fields
    
    private ParticleSystem _explosionParticle;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _explosionParticle = GetComponent<ParticleSystem>();
        _explosionParticle.Play();
    }

    private void Update()
    {
        if (!_explosionParticle.IsAlive())
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
