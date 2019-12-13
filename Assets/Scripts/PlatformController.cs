using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    // Start is called before the first frame update
    bool m_isBreakable;
    Vector3 MovDirection = Vector3.zero;
    float PlatformSpeed = -8f;
    float PlatformLength = 10f;

    void Start()
    {
        transform.localScale = new Vector3(1,1,PlatformLength);
    }

    void Update()
    {
        MovDirection = new Vector3(MovDirection.x, MovDirection.y, PlatformSpeed);
        transform.position += MovDirection * Time.deltaTime;
    }

    public void SetPlatformSpeed(float Speed = 8f)
    {
        PlatformSpeed = Speed;
    }
    public void SetPlatformLength(float Length = 8f)
    {
        PlatformLength = Length;
    }
    public void SetPlatformBreakable(bool Breakable = false)
    {
        m_isBreakable = Breakable;
    }

    public void SetPlatformProperties(float Length = 10f, float Speed = -8f,  bool Breakable = false)
    {
        PlatformSpeed = Speed;
        PlatformLength = Length;
        transform.localScale = new Vector3(1, 1, PlatformLength);
        m_isBreakable = Breakable;
    }
    public bool PlatformIsOffscreen()
    {
        if (transform.position.z < -(PlatformLength + 10)) return true;
        else return false;
    }
    
}
