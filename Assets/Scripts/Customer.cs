using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private CustomerController customerController;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        customerController = FindAnyObjectByType<CustomerController>();

        if(!customerController.customerActive1)
        {
            Debug.Log("Walk1");
            anim.SetTrigger("Walk1");
        }
        else if(customerController.customerActive1)
        {
            Debug.Log("Walk2");
            anim.SetTrigger("Walk2");
        }
    }

    private void Start()
    {
        
            
    }
}
