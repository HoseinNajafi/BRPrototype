using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public PlayerController PlayerTransform;
    public float CameraSpeed;
    Vector3 v= Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, PlayerTransform.CamPoint, ref v, 0.3f);//  CameraSpeed*Time.deltaTime);
    }
    
    /*



    public int Count; //User Input
    private Dictionary<int,int> _array=new Dictionary<int, int>();

    public void GetValues()
    {
        for (int i = 0; i < Count; i++)
        {
            int n = 0; //Get each value from user
            AddValue(n);
        }
        PrintValues(); //Prit values
    }

    private void AddValue(int n)
    {
        if (_array.ContainsKey(n))
            _array[n] = _array[n] + 1;
        else
            _array.Add(n, 1);
    }

    private void PrintValues()
    {
        for (int i = 0; i < Count; i++)
        {
            print("Count of " +
                _array.Keys.ToArray()[i]+ //Value
                " is "+
                _array[_array.Keys.ToArray()[i]]); //Count
        }

    }

    public int CulomnCount;//user input
    public int RowCount;//user input

    void Start()
    {
        
    }




*/
}