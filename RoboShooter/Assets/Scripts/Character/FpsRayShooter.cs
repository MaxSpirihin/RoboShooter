using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Компонет для стрельбы в FPS путем рейкастов - ВЕШАТЬ НА КАМЕРУ
/// </summary>
public class FpsRayShooter : MonoBehaviour
{
    private float TIME_BEFORE_RELOADSOUND = 0.2f;

    public AudioClip shootSound;
    public AudioClip reloadSound;

    public float shootBoxSize = 0.3f; //относительно высоты
    public float shootBoxAimSize = 0.02f;//относительно высоты
    public bool drawBox = false;

    private float _rayLength = 100;
    private Camera _camera;
    private AudioSource _audioSource;
    private Animator _playerAnimator;
    private bool _isReloading;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _audioSource = GetComponent<AudioSource>();
        _playerAnimator = GameObject.FindObjectOfType<Player>().GetComponent<Animator>();
#if !UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }

    void Update()
    {
        if (_isReloading) return;

        _playerAnimator.SetBool("Aim", InputManager.GetAim());

        if (InputManager.GetReloadDown())
        {
            StartCoroutine(Reload());
        }

        if (InputManager.GetShootDown())
        {
            Shoot();
        }
    }


    void Shoot()
    {
        _playerAnimator.SetTrigger("Shoot");

        //звук
        if (shootSound != null)
        {
            _audioSource.PlayOneShot(shootSound);
        }

        //выстрел
        float boxSize = _camera.pixelHeight * (InputManager.GetAim() ? shootBoxAimSize : shootBoxSize);
        Vector3 point = new Vector3(Random.Range((_camera.pixelWidth - boxSize) / 2 , (_camera.pixelWidth + boxSize) / 2),
            Random.Range((_camera.pixelHeight - boxSize) / 2, (_camera.pixelHeight + boxSize) / 2), 0);
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



    /// <summary>
    /// перезарядка
    /// </summary>
    IEnumerator Reload()
    {
        _isReloading = true;
        _playerAnimator.SetBool("Aim", false);
        _playerAnimator.SetTrigger("Reload");
        yield return new WaitForSeconds(TIME_BEFORE_RELOADSOUND);
        _audioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(_playerAnimator.GetCurrentAnimatorStateInfo(0).length - TIME_BEFORE_RELOADSOUND);
        _isReloading = false;
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

#if UNITY_EDITOR

    void OnGUI()
    {
        if (!drawBox) return;

        //метка прицела
        int size = 20;
        float x = _camera.pixelWidth / 2 - size / 4;
        float y = _camera.pixelHeight / 2 - size / 2;
        GUIStyle style = new GUIStyle();
        style.fontSize = size;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(x, y, size, size), "*", style);

        //квадрат разброса
        float boxSize = _camera.pixelHeight * (InputManager.GetAim() ? shootBoxAimSize : shootBoxSize);
        style.normal.background = MakeTex(2, 2, new Color(1f, 0f, 0f, 0.5f));
        GUI.Box(new Rect((_camera.pixelWidth - boxSize) / 2, (_camera.pixelHeight - boxSize) / 2,
            boxSize, boxSize), "", style);
        
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

#endif
}
