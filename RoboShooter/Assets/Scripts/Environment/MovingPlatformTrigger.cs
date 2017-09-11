using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Вспомогательный сценарий для работы движимых платформ
/// </summary>
/// <remarks>
/// Как использовать:
/// 1. Создаем реальную платформу
/// 2. Создаем пустой объект с триггером, чуть выше той платформы, цепляем к нему это скрипт
/// 3. Оба объекта помещаем в пустой родительский объект (его Scale всегда должен быть (1,1,1))
/// 4. Этот верхний объект используем для движения. Если нужно растягивать, то растягиваем вложенные объекты
/// 
/// Принцип работы:
/// Движение вместе с платформой осуществляется путем привязки игрока к движущемуся объекту, как дочерний элемент.
/// Это происходит, когда игрок попадает в триггер. При этом, чтобы сбросить родителя после приземления на обычную платформу,
/// скрипт игрока сам сбрасывает родителя всегда, когда он на земле и не выставлен флаг, что игрок на движимой платформе. Этот флаг устанавливает этот скрипт
/// </remarks>
public class MovingPlatformTrigger : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
      
	}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<FPSMove>();
        if (player != null)
        {
            player.transform.SetParent(this.transform.parent);//родительским элементом будет пустой объект - родитель триггера
            player.onMovingPlatform = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<FPSMove>();
        if (player != null)
        {
            player.onMovingPlatform = false;
        }
    }

}
