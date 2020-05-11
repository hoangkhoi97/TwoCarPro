using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardController : MonoBehaviour, IPointerDownHandler
{
    public bool isRedBtn = true;
    public Car car;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (GameController.instance.isPause || GameController.instance.isGameOver)
        {
            return;
        }
        else
        {
            car.ChangeTheRoad(isRedBtn);
        }
    }
}
