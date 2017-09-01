using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Основной публичный компонент Игрока
/// </summary>
public class Player : MonoBehaviour {

    public int startHealth = 100;
    public Weapon gun;
    

    public int health { get; private set; }
    

	void Start () {
        health = startHealth;
        gun.Init(110);
    }
	
	void Update () {
        
	}
    
    /// <summary>
    /// нанести несколько единиц урона игроку
    /// </summary>
    public void Damage(int damage = 1)
    {
        health--;
        Debug.Log("Health: " + health);

        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
