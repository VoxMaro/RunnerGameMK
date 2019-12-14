using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{

    protected bool isBreakable;
    protected Vector3 MovDirection = Vector3.zero;
    protected float objectSpeed = -8f;
    protected float objectLength = 10f;
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MovDirection = new Vector3(MovDirection.x, MovDirection.y, objectSpeed);
        transform.position += MovDirection * Time.deltaTime;
    }

    public void SetSpeed(float Speed = 8f)
    {
        objectSpeed = Speed;
    }
    public void SetLength(float Length = 8f)
    {
        objectLength = Length;
        transform.localScale = new Vector3(1, 1, objectLength);
    }
    public bool IsOffscreen()
    {
        if (transform.position.z < -(objectLength + 10)) return true;
        else return false;
    }
}
