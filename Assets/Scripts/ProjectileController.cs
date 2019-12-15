using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MovingObjectController
{

    
    // Start is called before the first frame update
    protected override void Start()
    {
        SetSpeed(-18f);
        base.Start();
    }

    public void ProjectileShielded()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

}
