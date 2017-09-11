using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображает параметры игрока на Canvas
/// </summary>
public class PlayerInfoCanvas : MonoBehaviour {

    public Text ammoText;
    public Text healthText;

    private Player _player;

	void Start () {
        _player = GameObject.FindObjectOfType<Player>();
	}
	
	void Update () {
        healthText.text = _player.health.ToString();
        ammoText.text = _player.gun.bulletsInShop + "/" + _player.gun.bulletsReserve;
    }
}
