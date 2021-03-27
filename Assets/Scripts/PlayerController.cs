using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("MoveSpeed")] public float moveSpeed;
    private float _horizontal;
    private float _vertical;
    private float gravity = 20f;
    [FormerlySerializedAs("JumpSpeed")] public float jumpSpeed;
    [FormerlySerializedAs("PController")] public CharacterController pController;
    private Vector3 _playerMove;
    private GameObject _steve;
    public void Start()
    {
        _steve = GameObject.Find("Steve");
        _steve.GetComponent<Renderer>().enabled = false;
    }

    public void Update()
    {
        Cursor.visible = false;
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetAxis("Vertical") > 0)
        {
            moveSpeed = 6f;
            if (Camera.main is { }) Camera.main.fieldOfView = 85f;
        }
        else
        {
            moveSpeed = 3f;
            if (Camera.main is { }) Camera.main.fieldOfView = 75f;
        }

        if (pController.isGrounded)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
            var transform1 = transform;
            _playerMove = (transform1.forward * _vertical + transform1.right * _horizontal) * moveSpeed;
            if (Input.GetKey(KeyCode.Space))
            {
                _playerMove.y = _playerMove.y + jumpSpeed;
            }
        }

        _playerMove.y -= gravity * Time.deltaTime;
        pController.Move(_playerMove * Time.deltaTime);
    }
}