using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayerController : MonoBehaviour
{
    

   // [SerializeField] float PlayerSpeed = 1f;
    [SerializeField] float JumpStrength = 1f;
    [SerializeField] float PlayerHeight = 0.5f;
    [SerializeField] float PlayerLength = 0.5f;

    Vector3 MovDirection;
    // Vector3 MovMomentum;

    RunnerGameplayFunctions gameplayFunctions;
    bool raycastResult;
    bool isJumping;
    bool hasShield = false;

    

   // float m_angle;

   // float m_vertVelocity;


    //maybe add double jump

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxisRaw("Jump") > 0) && raycastResult && !isJumping)
        {
            MovDirection += new Vector3(0, JumpStrength, 0);
            isJumping = true;
        }
        //Debug.Log(m_raycastResult);
        //m_controllerMain.Move(MovDirection * Time.deltaTime);
        //int layermask = 1 << 9;
        raycastResult = (Physics.Raycast(transform.position + new Vector3(0,0,PlayerLength), Vector3.down, PlayerHeight, LayerMask.GetMask("Default")) || Physics.Raycast(transform.position - new Vector3(0, 0, PlayerLength), Vector3.down, PlayerHeight, LayerMask.GetMask("Default")));
        
        Debug.DrawRay(transform.position + new Vector3(0, 0, PlayerLength), Vector3.down * PlayerHeight);
        Debug.DrawRay(transform.position - new Vector3(0, 0, PlayerLength), Vector3.down * PlayerHeight);

        if (raycastResult && MovDirection.y < 0)
        {
            MovDirection.y = 0;
            isJumping = false;
        }


        if (!raycastResult)
        {
            MovDirection += Physics.gravity / 50;
        }


        //stop jumping if player stops holding jump
        if (isJumping && !(Input.GetAxisRaw("Jump") > 0))
        {
            if (MovDirection.y > 0)
            {
                MovDirection.y /=3 ;
            }
            isJumping = false;
        }

        transform.position += MovDirection * Time.deltaTime;
        // if(m_colliderMain.)
        // m_controllerMain.Move(new Vector3(0, m_vertVelocity, 0));
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");

        if(other.tag == "Killbox")
        gameplayFunctions.SetState(RunnerGameplayFunctions.GameState.Dead);
        if(other.tag == "Projectile")
           
        { 
            if(!hasShield)
            gameplayFunctions.SetState(RunnerGameplayFunctions.GameState.Dead);
            else
            {
                //other.transform.gameObject
            }
        }
        else if (other.tag == "Item") 
        {
            other.GetComponentsInParent<PickupItem>();
            if (true)
            {
                gameplayFunctions.ScorePickup();
            }
        }
    }

    public void SetGameplayFunctions(RunnerGameplayFunctions gameplayFunctions)
    {
        this.gameplayFunctions = gameplayFunctions;
    }

    void FixedUpdate()
    {
       

    }
}
