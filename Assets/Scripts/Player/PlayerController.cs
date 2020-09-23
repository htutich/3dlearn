using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    #region PrivateData

    private Rigidbody _myRB;
    private float _speedMove = 2.0f;
    private float rayLength;
    private bool _playerOnGround = false;
    private int _kills = 0;
    private int _maxKills = 10;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Text _score;

    #endregion


    #region Events

    public delegate void PlayerAction();
    public static event PlayerAction onPlayerShoot;
    public static event PlayerAction onPlayerEndGame;

    #endregion


    #region OnEnable

    private void OnEnable()
    {
        EnemyHealthManager.onEnemyDie += EnemyDie;
    }

    #endregion


    #region OnDisable

    private void OnDisable()
    {
        EnemyHealthManager.onEnemyDie -= EnemyDie;
    }

    #endregion


    #region Start

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
        _score.text = $"Score: {_kills}";
    }

    #endregion
    

    #region Update

    private void Update()
    {
        float vectorX = Input.GetAxisRaw("Horizontal") * _speedMove;
        float vectorZ = Input.GetAxisRaw("Vertical") * _speedMove;
        _myRB.velocity = new Vector3(vectorX, _myRB.velocity.y, vectorZ);

        if (Input.GetKey(KeyCode.Space) && _playerOnGround)
        {
            _myRB.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
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
            onPlayerShoot();
        }
    }

    #endregion


    #region OnCollisionEnter

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _playerOnGround = true;
        }
    }

    #endregion


    #region Methods

    private void EnemyDie()
    {
        _kills++;
        _score.text = $"Score: {_kills} / {_maxKills}";

        if (_kills >= _maxKills)
        {
            onPlayerEndGame();
        }
    }

    #endregion
}
