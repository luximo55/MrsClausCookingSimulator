using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private WorkerEfficiency workerEfficiency;
    private GameObject activeCustomer;
    private Animator anim;
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
        anim = activeCustomer.GetComponent<Animator>();
    }

    public void CheckOrder(int cookieTypeServed)
    {
        if(cookieTypeServed == cookieTypeOrdered)
        {
            workerEfficiency.Efficiency += 20f;
        }
        else if (cookieTypeServed != cookieTypeOrdered)
        {
            workerEfficiency.Efficiency -= 15f;
        }
        CustomerWalkAway();
    }
    public void RawOrder()
    {
        Debug.Log("This shit is raw");
        workerEfficiency.Efficiency -=20f;
        CustomerWalkAway();
    }

    private void CustomerWalkAway()
    {
        anim.SetTrigger("WalkAway");
        Invoke("DeinitializeCustomer", 3f);
    }

    private void DeinitializeCustomer()
    {
        customerActive = false;
        Destroy(activeCustomer);
        Invoke("InitializeCustomer", 2f);
    }
}
