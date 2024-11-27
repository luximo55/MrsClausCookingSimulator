using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    private GameObject activeCustomer;
    public int cookieTypeOrdered = 0;
    public bool customerActive = false;

    private void Start()
    {
        Invoke("InitializeCustomer", 1);
    }
    private void InitializeCustomer()
    {
        customerActive = true;
        activeCustomer = Instantiate(customer);
        cookieTypeOrdered = Random.Range(1, 3);
        Debug.Log(cookieTypeOrdered);
    }

    public void CheckOrder(int cookieTypeServed)
    {
        if(cookieTypeServed == cookieTypeOrdered)
        {
            Debug.Log("CORRECT");
        }
        else if (cookieTypeServed != cookieTypeOrdered)
        {
            Debug.Log("WRONG");
        }
        DeinitializeCustomer();
    }
    public void RawOrder()
    {
        Debug.Log("This shit is raw");
        DeinitializeCustomer();
    }

    private void DeinitializeCustomer()
    {
        customerActive = false;
        Destroy(activeCustomer);
        Invoke("InitializeCustomer", 3);
    }
}
