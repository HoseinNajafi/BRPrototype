using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Animator _anim;
    public bool IsAtacking, IsMoving = false;
    public float Speed;

    private Vector3 _speed = new Vector3(0, 0, 0);
    private bool _isIdle;
    private Transform _camParrent;

    public Transform CameraParrent
    {
        get { return _camParrent ?? (_camParrent = GameObject.FindGameObjectWithTag("PlayCamera").transform); }
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {

        IsAtacking = Input.GetKey("m");

        IsMoving = (Math.Abs(_speed.magnitude) > .01) && !IsAtacking;

        _isIdle = (Math.Abs(_speed.magnitude) < .01) && !IsAtacking;

        _anim.SetBool("isAttacking", IsAtacking);
        _anim.SetBool("isMoving", IsMoving);
        _anim.SetBool("isStandig", _isIdle);

        _speed = Quaternion.Euler(0, CameraParrent.eulerAngles.y, 0) * _speed;

        Vector3 desirePosition = transform.position + _speed;

        if (IsMoving)
            transform.LookAt(desirePosition);

        transform.position = Vector3.MoveTowards(transform.position, desirePosition, _speed.magnitude * Time.deltaTime * (!IsAtacking ? 3 : 1));
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
}