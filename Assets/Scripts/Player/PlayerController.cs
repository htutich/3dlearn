using UnityEngine;


namespace learn3d
{
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Camera _mainCamera;
        private Rigidbody _myRigidbody;

        private float _speedMove = 2.5f;
        private float _heightJump = 5.0f;
        private float _rayLength;
        private bool _playerOnGround = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _myRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Movement();
            Fire();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                _playerOnGround = true;
            }
        }

        #endregion


        #region Methods

        private void Movement()
        {
            if (_playerOnGround)
            {
                Look();
                Walk();

                if (Input.GetKey(KeyCode.Space))
                {
                    Jump();
                }
            }
        }

        private void Fire()
        {
            if (Input.GetMouseButton(0))
            {
                EventManager.TriggerEvent("PlayerShoot");
            }
        }

        private void Walk()
        {
            float vectorX = Input.GetAxisRaw("Horizontal") * _speedMove;
            float vectorZ = Input.GetAxisRaw("Vertical") * _speedMove;
            _myRigidbody.velocity = new Vector3(vectorX, _myRigidbody.velocity.y, vectorZ);
        }

        private void Jump()
        {
            _myRigidbody.AddForce(new Vector3(0, _heightJump, 0) * _myRigidbody.mass, ForceMode.Impulse);
            _playerOnGround = false;
        }

        private void Look()
        {
            var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(cameraRay, out _rayLength))
            {
                var pointToLook = cameraRay.GetPoint(_rayLength);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }

        #endregion
    }
}