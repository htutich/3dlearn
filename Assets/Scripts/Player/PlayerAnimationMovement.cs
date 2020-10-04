using UnityEngine;


namespace learn3d
{
    public class PlayerAnimationMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LayerMask _floorMask;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameObject _myWeaponHand;

        private Rigidbody _myRigidbody;
        private Animator _myAnimator;
        private BoxCollider _myBoxCollider;
        private RaycastHit _raycastGroundHit;
        private Vector3 _movementVector;
        private Vector3 _lookVector;

        private float _heightJump = 0.4f;
        private float _rayLength;
        private bool _hasWeapon = false;

        #endregion


        #region Properties

        public GameObject Weapon
        {
            get
            {
                return _myWeaponHand;
            }
        }

        public bool HasWeapon
        {
            set
            {
                _hasWeapon = value;
                _myAnimator.SetBool("hasWeapon", value);
            }
        }

        #endregion


        #region UnityMethods

        private void Start()
        {
            _myRigidbody = GetComponent<Rigidbody>();
            _myAnimator = GetComponent<Animator>();
            _myBoxCollider = GetComponent<BoxCollider>();

            _myAnimator.SetBool("hasWeapon", _hasWeapon);
        }

        private void Update()
        {
            if (_hasWeapon)
            {
                Fire();
            }
        }

        private void FixedUpdate()
        {
            Movement();
        }

        void OnAnimatorMove()
        {
            _myRigidbody.MovePosition(_myRigidbody.position + transform.TransformDirection(_movementVector) * _myAnimator.deltaPosition.magnitude);
        }

        #endregion


        #region Methods

        private void Movement()
        {
            Walk();
            Look();
            if (IsGrounded())
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (_hasWeapon)
                    {
                        _myAnimator.Play("Running Jump With Weapon");
                    }
                    else
                    {
                        _myAnimator.Play("Running Jump");
                    }
                    _myRigidbody.AddForce(new Vector3(0, _heightJump, 0) * _myRigidbody.mass, ForceMode.Impulse);
                }
            }
        }

        private void Fire()
        {
            if (Input.GetMouseButton(0))
            {
                EventManager.TriggerEvent("PlayerShoot");
            }

            if (Input.GetMouseButton(1))
            {
                EventManager.TriggerEvent("PlayerBomb");
            }
        }

        private void Walk()
        {
            _movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            _myAnimator.SetFloat("Forward", _movementVector.x);
            _myAnimator.SetFloat("Turn", _movementVector.z);
        }

        private void Look()
        {
            var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(cameraRay, out _rayLength))
            {
                var pointToLook = cameraRay.GetPoint(_rayLength);
                _lookVector = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
                transform.LookAt(_lookVector);
            }
        }

        private bool IsGrounded()
        {
            Physics.Raycast(_myBoxCollider.bounds.center, Vector3.down, out _raycastGroundHit, _myBoxCollider.bounds.extents.y + 0.5f, _floorMask);
            return _raycastGroundHit.collider != null;
        }

        #endregion
    }
}