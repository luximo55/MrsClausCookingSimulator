using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private WorkerEfficiency workerEfficiency;
    private GameObject activeCustomer1;
    private GameObject activeCustomer2;
    public int cookieTypeOrdered1 = 0;
    public int cookieTypeOrdered2 = 0;
    public bool customerActive1 = false;
    public bool customerActive2 = false;

    private void Start()
    {
        Invoke("InitializeCustomer", 1);
        Invoke("InitializeCustomer", 5);
    }
    private void InitializeCustomer()
    {
        if(!customerActive1)
        {
            activeCustomer1 = Instantiate(customer);
            cookieTypeOrdered1 = Random.Range(1, 4);
            activeCustomer1.GetComponent<Customer>().ShowOrder(cookieTypeOrdered1);
            customerActive1 = true;
        }
        else if(customerActive1)
        {
            activeCustomer2 = Instantiate(customer);
            cookieTypeOrdered2 = Random.Range(1, 4);
            activeCustomer2.GetComponent<Customer>().ShowOrder(cookieTypeOrdered2);
            customerActive2 = true;
        }
        
    }

    public void CheckOrder(int cookieTypeServed, int customer)
    {
        switch(customer)
        {
            case 1:
                if(cookieTypeServed == cookieTypeOrdered1)
                {
                    workerEfficiency.efficiency += 20f;
                    CustomerWalkAway(customer, 1);
                }
                else if (cookieTypeServed != cookieTypeOrdered1)
                {
                    workerEfficiency.efficiency -= 15f;
                    CustomerWalkAway(customer, 0);
                }
                break;
            case 2:
                if(cookieTypeServed == cookieTypeOrdered2)
                {
                    workerEfficiency.efficiency += 20f;
                    CustomerWalkAway(customer, 1);
                }
                else if (cookieTypeServed != cookieTypeOrdered2)
                {
                    workerEfficiency.efficiency -= 15f;
                    CustomerWalkAway(customer, 0);
                }
                break;
        }
        
    }
    public void RawOrder(int customer)
    {
        workerEfficiency.efficiency -=20f;
        CustomerWalkAway(customer, 0);
    }

    private void CustomerWalkAway(int customer, int satisfaction)
    {
        switch (customer)
        {
            case 1:
                switch (satisfaction)
                {
                    case 0:
                        activeCustomer1.GetComponent<Animator>().SetTrigger("WalkAwayAngry");
                        break;
                    case 1:
                        activeCustomer1.GetComponent<Animator>().SetTrigger("WalkAwayHappy");
                        break;
                }
                activeCustomer1.GetComponent<Customer>().HideOrder();
                break;
            case 2:
                switch (satisfaction)
                {
                    case 0:
                        activeCustomer2.GetComponent<Animator>().SetTrigger("WalkAwayAngry");
                        break;
                    case 1:
                        activeCustomer2.GetComponent<Animator>().SetTrigger("WalkAwayHappy");
                        break;
                }
                activeCustomer2.GetComponent<Customer>().HideOrder();
                break;
        }
        StartCoroutine(DeinitializeCustomer(customer, 5f));
    }

    private IEnumerator DeinitializeCustomer(int customer, float delay)
    {
        yield return new WaitForSeconds(delay);
        switch(customer)
        {
            case 1:
                customerActive1 = false;
                Destroy(activeCustomer1);
                break;
            case 2:
                customerActive2 = false;
                Destroy(activeCustomer2);
                break;
        }
        Invoke("InitializeCustomer", 2f);
    }
}
