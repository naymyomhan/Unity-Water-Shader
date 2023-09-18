using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{   
    private bool havePlayer = false;

    private void OnTriggerEnter(Collider other)
    {   
        Debug.Log("Water Trigger Enter");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Enter Water");
            havePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Water Trigger Exit");
        if (other.CompareTag("Player"))
        {   
            Debug.Log("Player Exit Water");
            havePlayer = false;
        }
    }
}
