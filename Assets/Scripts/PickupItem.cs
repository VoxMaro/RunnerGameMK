using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    // Start is called before the first frame update
    public enum ItemTypes
    {
        Score,
        Shield
    }

    ItemTypes currType;
    public ItemTypes CurrentItemType
    {
        get { return currType; }
        set { currType = value; }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemCollected()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        //gameObject.GetComponent<>
    }
   
}
