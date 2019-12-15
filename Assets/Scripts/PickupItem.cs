using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    // Start is called before the first frame update


    Transform platformPosition;
    float itemHeight;
    [SerializeField] Material shieldPickupMaterial;
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
    public void RefreshMaterials()
    {
        if (currType == ItemTypes.Shield)
        {
            foreach (Renderer rend in gameObject.GetComponentsInChildren<Renderer>())
            {
                rend.material = shieldPickupMaterial;
            }
        }
    }

    public void SetItemPosAndHeight(Transform pos, float height)
    {
        platformPosition = pos;
        itemHeight = height;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (platformPosition == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Rotate(new Vector3(0, 180 * Time.deltaTime, 0));
            transform.position = platformPosition.position + new Vector3(0, itemHeight, 0);
        }
    }

    public void ItemCollected()
    {
       foreach(Collider col in gameObject.GetComponentsInChildren<Collider>())
       {
           col.enabled = false;
       }
        foreach (MeshRenderer mr in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = false;
        }
        
        //gameObject.GetComponent<>
    }
   
}
