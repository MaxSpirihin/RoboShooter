using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Тип ввода для FpsLook
/// </summary>
public enum FpsLookType
{
    Both,
    Horizontal,
    Vertical,
    Disabled
}


/// <summary>
/// Позволяет управлять направлением персонажа с помощью ввода
/// </summary>
public class FpsLook : MonoBehaviour {

    /// <summary>
    /// Тип ввода
    /// </summary>
    public FpsLookType type = FpsLookType.Both;

    /// <summary>
    /// Менять локальный transform
    /// </summary>
    public bool useLocal = false;

    public float horizontalSpeed = 100f;
    public float verticalSpeed = 100f;

    public float verticalMax = 45f;
    public float verticalMin = -45f;

    private float _rotationX = 0;

    // Use this for initialization
    void Start () {
		var body = GetComponent<Rigidbody>();
        if (body != null)
            body.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        var rotationY = transform.localEulerAngles.y + InputManager.GetLookAxisHorizontal() * horizontalSpeed * Time.deltaTime;
        _rotationX = Mathf.Clamp(_rotationX - InputManager.GetLookAxisVertical() * verticalSpeed * Time.deltaTime,
            verticalMin, verticalMax);

        var rot = Quaternion.Euler(
            type == FpsLookType.Horizontal ? 0 :_rotationX, type == FpsLookType.Vertical ? 0 : rotationY, 0);

        if (useLocal)
            transform.localRotation = rot;
        else
            transform.rotation = rot;
	}
}
