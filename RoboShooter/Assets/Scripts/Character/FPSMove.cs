using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Пощзволяет двигать персонаж в FPS
/// </summary>
public class FPSMove : MonoBehaviour
{

    //движение
    public float horizontalMoveSpeed = 10;
    public float gravity = -50f;
    public float maxVertSpeed = -30f;

    //прыжок
    public float jumpSpeed = 20;
    public float jumpSpeedMax = 30;//максимальная сила прыжка (когда зажимаем кнопку)
    public float jumpAddTime = 0.15f;//время после начала прыжка, когда нажатая кнопка придает ускорение
    public float jumpAddIgnoreTime = 0.15f;//время после начала прыжка, когда нажатая кнопка еще не придает ускорение, чтобы было легко совершать минимальный прыжок


    //гашение импульса
    public float horizontalDeceleration = 200f;//торможение при гашении импульса
    public float timeBeforeStopToMove = 0.2f;//Время до полного торможения, когда блок движения снимается
    
    public bool onMovingPlatform { get; set; }


    private CharacterController _charController;
    [SerializeField] private Vector3 _speed = new Vector3();
    private bool _blockHorizontalMove;
    private bool _jumpStarted;


    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }


    void Update()
    {
        MovingPlatformDetach();//отсоединение движущейся платформы
        if (!_blockHorizontalMove) HorizontalMove();
        VerticalMove();
    }

    private void LateUpdate()
    {
        _charController.Move(_speed * Time.deltaTime);
    }


    void HorizontalMove()
    {
        var horSpeed = new Vector3(InputManager.GetMoveAxisHorizontal() * this.horizontalMoveSpeed, 0, InputManager.GetMoveAxisVertical() * this.horizontalMoveSpeed);
        horSpeed = Vector3.ClampMagnitude(horSpeed, this.horizontalMoveSpeed);
        horSpeed = transform.TransformDirection(horSpeed);

        _speed.x = horSpeed.x;
        _speed.z = horSpeed.z;
    }

    public int a = 0;
    void VerticalMove()
    {
        //начало прыжка
        if (_charController.isGrounded && InputManager.GetJumpDown())
        {
            _speed.y = jumpSpeed;
            StartCoroutine(JumpStartCoroutine());
        }

        //обработка длительного прыжка
        if (_jumpStarted && InputManager.GetJump())
        {
            a++;
            _speed.y += (jumpSpeedMax - jumpSpeed) * Time.deltaTime / jumpAddTime;
        }


        //вертикальное движение с учетом гравитации
        _speed.y += gravity * Time.deltaTime;
        _speed.y = Mathf.Max(_speed.y, maxVertSpeed);
    }

    IEnumerator JumpStartCoroutine()
    {
        yield return new WaitForSeconds(jumpAddIgnoreTime);
        _jumpStarted = true;
        yield return new WaitForSeconds(jumpAddTime);
        _jumpStarted = false;
    }


    /// <summary>
    /// Придать игроку импульс извне 
    /// </summary>
    /// <param name="blockHorizontalMove">Блокировать горизонтальное движение, пока импульс гасится</param>
    /// <param name="impulse">Величина и направление импульса</param>
    public void AddImpulse(Vector3 impulse, bool blockHorizontalMove)
    {
        _speed.y = impulse.y;
        _speed.x = impulse.x;
        _speed.z = impulse.z;

        StartCoroutine(ExtinguishHorizontalImpulse(blockHorizontalMove));
    }

    /// <summary>
    /// Погасить горизонтальный импульс
    /// </summary>
    private IEnumerator ExtinguishHorizontalImpulse(bool blockHorizontalMove)
    {
        if (blockHorizontalMove) _blockHorizontalMove = true;
        float horSpeedMagintude = new Vector3(_speed.x, 0, _speed.z).magnitude;
        Vector3 horSpeedDirection = new Vector3(_speed.x, 0, _speed.z).normalized;

        while (horSpeedMagintude > horizontalDeceleration* timeBeforeStopToMove)//чтобы тормозить не до конца
        {
            horSpeedMagintude -= horizontalDeceleration * Time.deltaTime;
            _speed.x = horSpeedDirection.x * horSpeedMagintude;
            _speed.z = horSpeedDirection.z * horSpeedMagintude;
            yield return null;
        }

        if (blockHorizontalMove) _blockHorizontalMove = false;

    }

    void MovingPlatformDetach()
    {
        if (_charController.isGrounded && !onMovingPlatform)
            transform.parent = null;
    }

}
