using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayerController : MonoBehaviour
{


    // [SerializeField] float PlayerSpeed = 1f;
    [SerializeField] float JumpStrength = 1f;
    [SerializeField] float PlayerHeight = 0.5f;
    [SerializeField] float PlayerLength = 0.5f;
   Animator animator = null;

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
        animator = GetComponentInChildren<Animator>();
    }

    void UpdateAnimationParameters()
    {
        animator.SetBool("Jumping", !raycastResult);
        animator.SetFloat("VerticalVel", MovDirection.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (((Input.GetAxisRaw("Jump") > 0) || Input.touchCount > 0) && raycastResult && !isJumping)
        {
            MovDirection += new Vector3(0, JumpStrength, 0);
            isJumping = true;
        }
        //Debug.Log(m_raycastResult);
        //m_controllerMain.Move(MovDirection * Time.deltaTime);
        //int layermask = 1 << 9;\
        RaycastHit outHitDist1;
        RaycastHit outHitDist2;
        bool tempHit1;
        bool tempHit2;
        tempHit1 = Physics.Raycast(transform.position + new Vector3(0, 0, PlayerLength), Vector3.down, out outHitDist1, PlayerHeight, LayerMask.GetMask("Default"));
        tempHit2 = Physics.Raycast(transform.position - new Vector3(0, 0, PlayerLength), Vector3.down, out outHitDist2, PlayerHeight, LayerMask.GetMask("Default"));
        raycastResult = tempHit1 || tempHit2;
        


        if (raycastResult && MovDirection.y < 0)
        {
            float displaceValue = PlayerHeight - Mathf.Max(outHitDist1.distance, outHitDist2.distance);
            transform.position += new Vector3(0,displaceValue,0);
            MovDirection.y = 0;
            
            isJumping = false;
        }


        if (!raycastResult)
        {
            //bit of a magic number, holdover from earlier mistake
            MovDirection += Physics.gravity *2.88f * Time.deltaTime;

        }


        //stop jumping if player stops holding jump
        if (isJumping && !((Input.GetAxisRaw("Jump") > 0) || Input.touchCount > 0))
        {
            if (MovDirection.y > 0)
            {
                MovDirection.y /=3 ;
            }
            isJumping = false;
        }

        transform.position += MovDirection * Time.deltaTime;
        UpdateAnimationParameters();
        // if(m_colliderMain.)
        // m_controllerMain.Move(new Vector3(0, m_vertVelocity, 0));
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Killbox"))
        gameplayFunctions.SetState(RunnerGameplayFunctions.GameState.Dead);
        if(other.CompareTag("Projectile"))
        {
            if(!hasShield)
            gameplayFunctions.SetState(RunnerGameplayFunctions.GameState.Dead);
            else
            {
                other.GetComponentInParent<ProjectileController>().ProjectileShielded();
                setShielded(false);
                //other.transform.gameObject
            }
        }
        else if (other.CompareTag("Item")) 
        {
            
            switch (other.GetComponentInParent<PickupItem>().CurrentItemType)
            {
                case PickupItem.ItemTypes.Score:
                    gameplayFunctions.ScorePickup();
                    other.GetComponentInParent<PickupItem>().ItemCollected();
                    break;
                case PickupItem.ItemTypes.Shield:
                    other.GetComponentInParent<PickupItem>().ItemCollected();
                    setShielded(true);
                    break;
                default:
                    break;
            }
            

        }
    }

    void setShielded(bool shielded)
    {
        hasShield = shielded;
        transform.GetChild(1).GetComponent<MeshRenderer>().enabled = shielded;
    }

    public void SetGameplayFunctions(RunnerGameplayFunctions gameplayFunctions)
    {
        this.gameplayFunctions = gameplayFunctions;
    }

    void FixedUpdate()
    {
       

    }
}
