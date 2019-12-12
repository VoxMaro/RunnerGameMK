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
        transform.localScale = new Vector3(0,0,PlatformLength);
    }

    void Update()
    {
        MovDirection = new Vector3(MovDirection.x, MovDirection.y, PlatformSpeed);
        transform.position += MovDirection * Time.deltaTime;
    }

    void SetPlatformProperties(float Speed, float Length = 10f, bool Breakable = false)
    {
        PlatformSpeed = Speed;
        PlatformLength = Length;
        transform.localScale = new Vector3(0, 0, PlatformLength);
        m_isBreakable = Breakable;
    }
    public bool PlatformIsOffscreen()
    {
        if (transform.position.z < -((PlatformLength / 2) + 10)) return true;
        else return false;
    }
    
}
