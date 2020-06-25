using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] private IntReference pickUp;
    [SerializeField] private AudioClip pickUpSound;
    
    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(pickUpSound, Camera.main.transform.position);
        pickUp.Value++;
        gameObject.SetActive(false);
    }
}
