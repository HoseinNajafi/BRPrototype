using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Animator _anim;
    public bool IsAtacking, IsMoving = false;
    public float Speed;
    public float CamDistance = 4;
    private Vector3 _speed = new Vector3(0, 0, 0);
    private bool _isIdle;
    private Transform _camParrent;
    private Vector3 _aimDirection;
    private Vector3 _desirePosition;


    public Transform CameraParrent
    {
        get { return _camParrent ?? (_camParrent = GameObject.FindGameObjectWithTag("PlayCamera").transform); }
    }

    public Vector3 CamPoint
    {
        get
        {
            if (IsMoving)
                return -(CamDistance-1) * transform.position + _desirePosition * CamDistance;
            if (IsAtacking)
                return transform.position + _aimDirection.normalized * CamDistance;
            
                return transform.position;
        }
    }


    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {


        IsMoving = (Math.Abs(_speed.magnitude) > .01) && !IsAtacking;

        _isIdle = (Math.Abs(_speed.magnitude) < .01) && !IsAtacking;

        _anim.SetBool("isAttacking", IsAtacking);
        _anim.SetBool("isMoving", IsMoving);
        _anim.SetBool("isStandig", _isIdle);

        _speed = Quaternion.Euler(0, CameraParrent.eulerAngles.y, 0) * _speed;
        _aimDirection = Quaternion.Euler(0, CameraParrent.eulerAngles.y, 0) * _aimDirection;

        _desirePosition = transform.position + _speed;

        if (IsMoving)
            transform.LookAt(_desirePosition);

        else if (IsAtacking)
            transform.LookAt(transform.position + _aimDirection);


        transform.position = Vector3.MoveTowards(transform.position, _desirePosition, _speed.magnitude * Time.deltaTime * (!IsAtacking ? 3 : 1));
        Vector3 move = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * _speed;

        if (!_isIdle)
        {
            _anim.SetFloat("Vel X", move.x);
            _anim.SetFloat("Vel Y", move.z);
            _anim.SetFloat("Speed", _speed.magnitude);
        }

    }


    public void SetMovementSpeed(Vector3 direction)
    {
        _speed = direction * Speed;
    }

    public void SetAim(bool aim, Vector3 dir)
    {
        IsAtacking = aim;
        _aimDirection = dir;
    }
}