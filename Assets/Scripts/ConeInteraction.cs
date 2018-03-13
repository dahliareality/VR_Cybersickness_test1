using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeInteraction : MonoBehaviour
{

    // Use this for initialization
    
   

    
    public void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
        
    }

}



