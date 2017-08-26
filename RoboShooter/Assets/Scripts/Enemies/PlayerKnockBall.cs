using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockBall : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<FPSMove>();
        if (player != null)
        {
            Vector3 speed = 10 * new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z).normalized;
            player.KnockBall(speed, 0.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("OnControllerColliderHit");
    }
}
