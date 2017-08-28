using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Тип отбрасывания
/// </summary>
public enum PlayerKnockBackType
{
    FromCenterHorizontal,
    FromCenter,
    Up
}


/// <summary>
/// Отбрасывать игрока при столкновении - необходим триггер
/// </summary>
public class PlayerKnockBack : MonoBehaviour
{
    public PlayerKnockBackType type = PlayerKnockBackType.FromCenterHorizontal;
    public float impulseForce = 10;

    void Start() { }

    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<FPSMove>();
        if (player != null)
        {
            Vector3 impulse;
            switch (type)
            {
                case PlayerKnockBackType.FromCenterHorizontal:
                    impulse = impulseForce * new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized;
                    player.AddImpulse(impulse, true);
                    break;
                case PlayerKnockBackType.FromCenter:
                    impulse = impulseForce * (player.transform.position - transform.position).normalized;
                    player.AddImpulse(impulse, true);
                    break;
                case PlayerKnockBackType.Up:
                    player.AddImpulse(impulseForce * Vector3.up, false);
                    break;
            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("OnCollisionEnter");
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    Debug.Log("OnControllerColliderHit");
    //}
}
