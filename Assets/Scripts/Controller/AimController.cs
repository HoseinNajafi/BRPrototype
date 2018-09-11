using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AimController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 Direction;
    public PlayerController PlayerController;
    private Vector2 _startPos;
    private Vector2 _currentPosition;
    public Image JoyesticImage;
    private Vector3 _joyPosition;
    private bool _isAiming;


    public void Start()
    {
        _joyPosition = JoyesticImage.rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        _startPos = data.position;
        _isAiming = true;
        JoyesticImage.rectTransform.SetPositionAndRotation(new Vector3(_startPos.x, _startPos.y, 0), Quaternion.identity);
    }

    public void OnDrag(PointerEventData data)
    {
        _currentPosition = data.position;
        CalculateAim();
    }

    private void CalculateAim()
    {
        Direction = (_currentPosition - _startPos);

        Direction = Direction.normalized;
    }


    public void OnEndDrag(PointerEventData data)
    {
        _isAiming = false;
        Direction = Vector2.zero;
        JoyesticImage.rectTransform.SetPositionAndRotation(_joyPosition, Quaternion.identity);
        PlayerController.SetAim(false,Vector3.zero);

    }

    public void Update()
    {
        Vector3 dir = new Vector3(Direction.x, 0, Direction.y);
        PlayerController.SetAim(_isAiming,dir);

    }



}
