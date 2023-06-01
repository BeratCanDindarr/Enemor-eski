using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    GameManager GM;
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
        GM = FindObjectOfType<GameManager>();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("Level", 1) == 1)
        {
            GM.TutMoveDialog();
        }
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
}