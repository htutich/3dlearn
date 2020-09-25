using UnityEngine;


public class PlayerController : MonoBehaviour
{
    #region Fields
    [SerializeField] private Camera _mainCamera;

    private Rigidbody _myRigidbody;
    private float _speedMove = 2.0f;
    private float _heightJump = 5.0f;
    private float rayLength;
    private bool _playerOnGround = false;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float vectorX = Input.GetAxisRaw("Horizontal") * _speedMove;
        float vectorZ = Input.GetAxisRaw("Vertical") * _speedMove;
        _myRigidbody.velocity = new Vector3(vectorX, _myRigidbody.velocity.y, vectorZ);

        if (Input.GetKey(KeyCode.Space) && _playerOnGround)
        {
            _myRigidbody.AddForce(new Vector3(0, _heightJump, 0), ForceMode.Impulse);
            _playerOnGround = false;
        }

        var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            var pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        if (Input.GetMouseButton(0))
        {
            EventManager.TriggerEvent("PlayerShoot");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _playerOnGround = true;
        }
    }

    #endregion
}
