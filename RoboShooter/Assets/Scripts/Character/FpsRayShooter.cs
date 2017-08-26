using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Компонет для стрельбы в FPS путем рейкастов - ВЕШАТЬ НА КАМЕРУ
/// </summary>
public class FpsRayShooter : MonoBehaviour
{
    public AudioClip shootSound;
    public AudioClip reloadSound;

    private float _rayLength = 100;
    private Camera _camera;
    private AudioSource _audioSource;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _audioSource = GetComponent<AudioSource>();

#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }

    void Update()
    {
        if (InputManager.GetShoot())
        {
            //звук
            if (shootSound != null)
            {
                _audioSource.PlayOneShot(shootSound);
            }

            //выстрел
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _rayLength))
            {
                var target = hit.transform.GetComponent<IShooterTarget>();
                if (target != null)
                {
                    target.Shoot(hit.point);
                }
                else
                {
                    //отметина в месте попадания
                    StartCoroutine(ShootIndicator(hit.point));
                }
            }
        }
    }

    /// <summary>
    /// Индикатор попадания пули в объект
    /// </summary>
    IEnumerator ShootIndicator(Vector3 Point)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = Point;
        sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        yield return new WaitForSeconds(100f);
        GameObject.Destroy(sphere);

    }


    void OnGUI()
    {
        int size = 20;
        float x = _camera.pixelWidth / 2 - size / 4;
        float y = _camera.pixelHeight / 2 - size / 2;
        GUIStyle style = new GUIStyle();
        style.fontSize = size;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(x, y, size, size), "*", style);
    }
}
