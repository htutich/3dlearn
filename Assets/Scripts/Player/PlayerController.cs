using UnityEngine;


public class PlayerController : MonoBehaviour
{

    #region PrivateData

    private Rigidbody _myRB;
    private float _speedMove = 2.0f;
    private float rayLength;
    private bool _playerOnGround = false;

    [SerializeField] private Camera _mainCamera;

    #endregion


    #region Events

    public delegate void PlayerAction();
    public static event PlayerAction PlayerShoot;

    #endregion


    #region Start

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
    }

    #endregion
    

    #region Update

    private void Update()
    {
        // Walk
        float vectorX = Input.GetAxisRaw("Horizontal") * _speedMove;
        float vectorZ = Input.GetAxisRaw("Vertical") * _speedMove;
        _myRB.velocity = new Vector3(vectorX, _myRB.velocity.y, vectorZ);

        // Jump
        if (Input.GetKey(KeyCode.Space) && _playerOnGround)
        {
            _myRB.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            _playerOnGround = false;
        }

        // Look
        var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        var groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            var pointToLook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        // Shooting
        if (Input.GetMouseButton(0))
        {
            PlayerShoot();
        }
    }

    #endregion


    #region OnCollisionEnter

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем на земле ли игрок и ставим что да
        if (collision.gameObject.tag == "Floor")
        {
            _playerOnGround = true;
        }
    }

    #endregion

}
