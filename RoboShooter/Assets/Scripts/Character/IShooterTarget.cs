using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Цель попадания пули 
/// </summary>
public interface IShooterTarget
{
    /// <summary>
    /// Пуля игрока попала в объект
    /// </summary>
    /// <param name="point">Точка попадания</param>
    void Shoot(Vector3 point);
}
