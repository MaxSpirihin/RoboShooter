using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Оружие - все параметры оружия здесь, расширять - этот класс, модель и анимация контролируется отдельно
/// </summary>
[Serializable]
public class Weapon
{
    public int bulletsInShopMax = 10;
    public int bulletsMax = 200;

    public float shootBoxSize = 0.3f; //относительно высоты экрана
    public float shootBoxAimSize = 0.02f;//относительно высоты экрана


    public int bulletsInShop { get; private set; }
    public int bulletsReserve { get; private set; }


    /// <summary>
    /// Инициализировать оружие на старте
    /// </summary>
    /// <param name="bullets">Стартовое число патронов</param>
    public void Init(int bullets)
    {
        if (bullets >= bulletsInShopMax)
        {
            bulletsInShop = bulletsInShopMax;
            bulletsReserve = bullets - bulletsInShopMax;
        }
        else
        {
            bulletsInShop = bullets;
            bulletsReserve = 0;
        }
    }

    /// <summary>
    /// Выстрел
    /// </summary>
    public bool Shoot()
    {
        if (bulletsInShop == 0) return false;

        bulletsInShop--;
        return true;
    }


    public bool CanReload()
    {
        return (bulletsReserve > 0 && bulletsInShop < bulletsInShopMax);
    }

    /// <summary>
    /// Перезарядка
    /// </summary>
    public bool Reload()
    {
        if (!CanReload()) return false;

        bulletsReserve += bulletsInShop;
        bulletsInShop = Math.Min(bulletsReserve, bulletsInShopMax);
        bulletsReserve -= bulletsInShop;
        return true;
    }

}
