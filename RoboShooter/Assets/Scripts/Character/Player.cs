using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Основной публичный компонент Игрока
/// </summary>
public class Player : MonoBehaviour {

    public int startHealth = 5;

    private int _health = 5;

	void Start () {
        _health = startHealth;
    }
	
	void Update () {
		
	}
    
    /// <summary>
    /// нанести несколько единиц урона игроку
    /// </summary>
    public void Damage(int damage = 1)
    {
        _health--;
        Debug.Log("Health: " + _health);

        if (_health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
