using UnityEngine;


public class BulletController : MonoBehaviour
{

    #region PrivateData

    private float _speed = 10f;
    private float _lifeTime = 1f;
    private int _damageToGive;

    #endregion


    #region Start

    private void Start()
    {
        _damageToGive = Random.Range(20, 30);
    }

    #endregion


    #region Update

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        Destroy(gameObject, _lifeTime);
    }

    #endregion


    #region OnCollisionEnter

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "World")
        {
            Destroy(gameObject);
        }
    }

    #endregion

}
