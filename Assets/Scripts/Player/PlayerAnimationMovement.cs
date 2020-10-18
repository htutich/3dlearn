using UnityEngine;


namespace learn3d
{
    public class PlayerAnimationMovement : MonoBehaviour
    {
        #region Fields

        [SerializeField] private LayerMask _floorMask;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameObject _myWeaponHand;
        [SerializeField] private AudioClip _stepMusic;
        [SerializeField] private bool _hasWeapon = false;
        [SerializeField] private bool _isMenu = false;

        private Rigidbody _myRigidbody;
        private Animator _myAnimator;
        private BoxCollider _myBoxCollider;
        private AudioSource _audioSource;
        private RaycastHit _raycastGroundHit;
        private Vector3 _movementVector;
        private Vector3 _lookVector;

        private float _heightJump = 1.0f;
        private float _movementSpeedMultipie = 3.5f;
        private float _rayLength;

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
            _audioSource = GetComponent<AudioSource>();

            _myAnimator.SetBool("hasWeapon", _hasWeapon);
        }

        private void Update()
        {
            if (_hasWeapon)
            {
                Fire();
            }
            Jump();
        }

        private void FixedUpdate()
        {
            Walk();
            Look();
        }

        void OnAnimatorMove()
        {

            var vector = _myRigidbody.position + _movementVector * _movementSpeedMultipie * Time.deltaTime;
            vector.y = _myRigidbody.position.y;
            _myRigidbody.MovePosition(vector);
        }

        #endregion


        #region Methods

        private void Fire()
        {
            if (!_isMenu)
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
        }

        private void Walk()
        {
            if (!_isMenu)
            {
                _movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
                var vector = transform.TransformDirection(_movementVector);

                if (vector.x != 0.0f || vector.z != 0.0f)
                {
                    if (!_audioSource.isPlaying)
                    {
                        _audioSource.clip = _stepMusic;
                        _audioSource.Play();

                    }
                }
                else
                {
                    _audioSource.Stop();
                }

                _myAnimator.SetFloat("Forward", vector.x);
                _myAnimator.SetFloat("Turn", vector.z);
            }
        }

        private void Look()
        {
            if (!_isMenu)
            {
                var cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                if (groundPlane.Raycast(cameraRay, out _rayLength))
                {
                    var pointToLook = cameraRay.GetPoint(_rayLength);
                    _lookVector = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);
                    transform.LookAt(_lookVector);

                    Debug.DrawLine(transform.position, _lookVector, Color.blue);

                }
            }
        }

        private void Jump()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (IsGrounded())
                {
                    if (_hasWeapon)
                    {
                        _myAnimator.Play("Running Jump With Weapon");
                    }
                    else
                    {
                        _myAnimator.Play("Running Jump");
                    }

                    var vector = _myRigidbody.position + _movementVector * _movementSpeedMultipie * Time.deltaTime;
                    vector.y = _myRigidbody.position.y + _heightJump * _movementSpeedMultipie * Time.deltaTime;
                    _myRigidbody.MovePosition(vector);
                }
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