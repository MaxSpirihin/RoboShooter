using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Пощзволяет двигать персонаж в FPS
/// </summary>
public class FPSMove : MonoBehaviour
{
    public float jumpSpeed = 20;
    public float horizontalSpeed = 10;
    public float gravity = -50f;
    public float maxVertSpeed = -30f;


    [SerializeField] private float _vertSpeed;
    private CharacterController _charController;

    private Vector3 _speed = new Vector3();
    private bool _blockHorizontalMove;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!_blockHorizontalMove) HorizontalMove();
        VerticalMove();
        _charController.Move(_speed * Time.deltaTime);
    }

    void HorizontalMove()
    {
        _speed = new Vector3(InputManager.GetMoveAxisHorizontal() * this.horizontalSpeed, 0, InputManager.GetMoveAxisVertical() * this.horizontalSpeed);
        _speed = Vector3.ClampMagnitude(_speed, this.horizontalSpeed);
        _speed = transform.TransformDirection(_speed);
    }

    void VerticalMove()
    {
        //прыжок
        if (_charController.isGrounded && InputManager.GetJump())
        {
            _vertSpeed = jumpSpeed;
        }
        //вертикальное движение с учетом гравитации
        _vertSpeed += gravity * Time.deltaTime;
        _vertSpeed = Mathf.Max(_vertSpeed, maxVertSpeed);
        _speed.y = _vertSpeed;
    }


    public void KnockBall(Vector3 speed, float time)
    {
        StartCoroutine(KnockBallCoroutine(speed, time));
    }

    private IEnumerator KnockBallCoroutine(Vector3 speed, float time)
    {
        _blockHorizontalMove = true;
        for (int i = 0; i < 5; i++)
        {
            _speed = speed * (5 - i) / 5;
            yield return new WaitForSeconds(time / 5);
        }
        _blockHorizontalMove = false;

    }

}
