using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayerController : MonoBehaviour
{
    [SerializeField] Collider m_colliderMain = null;

   // [SerializeField] float PlayerSpeed = 1f;
    [SerializeField] float JumpStrength = 1f;
    [SerializeField] float PlayerHeight = 0.5f;

    Vector3 MovDirection;
   // Vector3 MovMomentum;


    bool m_raycastResult;
    bool m_jumping;
   // float m_angle;

   // float m_vertVelocity;


    //maybe add double jump

    // Start is called before the first frame update
    void Start()
    {
      
        //transform.SetPositionAndRotation()
        //transform.SetParent(m_centralPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetAxis("Horizontal") != 0)
        //{
            
       //}
        //MovDirection = new Vector3(MovDirection.x, MovDirection.y, PlayerSpeed);


        if ((Input.GetAxisRaw("Jump") > 0) && m_raycastResult && !m_jumping)
        {
            MovDirection += new Vector3(0, JumpStrength, 0);
            m_jumping = true;
        }
        //Debug.Log(m_raycastResult);
        //m_controllerMain.Move(MovDirection * Time.deltaTime);


        
            //int layermask = 1 << 9;
        m_raycastResult = Physics.Raycast(transform.position, Vector3.down, PlayerHeight, LayerMask.GetMask("Default"));
        //m_raycastResult = Physics.Raycast(transform.position+transform.localScale, Vector3.down, PlayerHeight, LayerMask.GetMask("Default"));
        Debug.DrawRay(transform.position, Vector3.down * PlayerHeight);
        if (m_raycastResult && MovDirection.y < 0)
        {
            MovDirection.y = 0;
            m_jumping = false;
        }


        if (!m_raycastResult)
        {
            MovDirection += Physics.gravity / 50;
        }


        //stop jumping if player stops holding jump
        if (m_jumping && !(Input.GetAxisRaw("Jump") > 0))
        {
            if (MovDirection.y > 0)
            {
                MovDirection.y /=3 ;
            }
            m_jumping = false;
        }

        transform.position += MovDirection * Time.deltaTime;
        // if(m_colliderMain.)
        // m_controllerMain.Move(new Vector3(0, m_vertVelocity, 0));




    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
    }


    void FixedUpdate()
    {
       

    }
}
