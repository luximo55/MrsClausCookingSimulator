using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Material[] cookieSprite;
    public GameObject speechBubble;
    private CustomerController customerController;
    private MeshRenderer meshRenderer;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        customerController = FindAnyObjectByType<CustomerController>();
        speechBubble.SetActive(false);

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

    public void ShowOrder(int cookieType)
    {
        StartCoroutine(EnableSpeechBubble(cookieType));         
    }

    private IEnumerator EnableSpeechBubble(int cookieType)
    {
        yield return new WaitForSeconds(4f);
        speechBubble.SetActive(true);
        meshRenderer = speechBubble.GetComponent<MeshRenderer>();
        switch(cookieType)
        {
            case 1:
                meshRenderer.material = cookieSprite[0];
                break;
            case 2:
                meshRenderer.material = cookieSprite[1];
                break;
            case 3:
                meshRenderer.material = cookieSprite[2];
                break;
        }   
    }
}
