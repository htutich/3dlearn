using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _myRB;
    private float _speedMove = 2.0f;
    private float _turnMove = 3.5f;
    private bool _playerOnGround = false;
    [SerializeField] private Camera _mainCamera;

    public delegate void PlayerAction();
    public static event PlayerAction PlayerShoot;

    private void Start()
    {
        _myRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Движение
        float vectorX = Input.GetAxisRaw("Horizontal") * _speedMove;
        float vectorZ = Input.GetAxisRaw("Vertical") * _speedMove;
        _myRB.velocity = new Vector3(vectorX, _myRB.velocity.y, vectorZ);

        //Прыжок
        if (Input.GetKey(KeyCode.Space) && _playerOnGround)
        {
            _myRB.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            _playerOnGround = false;
        }

        //Куда смотреть

        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }


        if (Input.GetMouseButton(0))
        {
            PlayerShoot();
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        //Проверяем на земле ли игрок и ставим что да
        if (collision.gameObject.tag == "Floor")
        {
            _playerOnGround = true;
        }
    }
}
