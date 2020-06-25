using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private IntReference score;
    [SerializeField] private float marginDistance = 1.2f;
    [SerializeField] private FloatReference movingChance;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float moveBorder = 3f;
    [SerializeField] private AudioClip touchPlaftormSFX;
    
    
    private bool _shouldMove;

    private bool _dirRight = true;
    
    private void OnEnable()
    {
        if (Random.value < movingChance.Value)
        {
            _shouldMove = true;

            if (Random.value > 0.5f)
            {
                _dirRight = true;
            }
            else
            {
                _dirRight = false;
            }
        }
    }

    private void Update()
    {
        MovingPlatform();
    }

    private void MovingPlatform()
    {
        if (!_shouldMove) return;

        var trans = _dirRight ? Vector3.right : -Vector3.right;
        transform.Translate(trans * moveSpeed * Time.deltaTime);

        if (transform.position.x >= moveBorder) _dirRight = false;
        if (transform.position.x <= -moveBorder) _dirRight = true;
    }


    private void OnTriggerExit(Collider other)
    {
        var distance = Mathf.Abs(other.transform.position.x - transform.position.x);
        
        if (distance <= marginDistance)
        {
            score.Value++;
            PlaySound();
            StartCoroutine(DisablePlatform());
        }
        else
        {
            StartCoroutine(EndGame(other));
        }
    }

    private IEnumerator DisablePlatform()
    {
        yield return new WaitForSeconds(2f);
        
        _shouldMove = false;
        gameObject.SetActive(false);
    }

    private void PlaySound()
    {
        AudioSource.PlayClipAtPoint(touchPlaftormSFX, Camera.main.transform.position);
    }
    
    private IEnumerator EndGame(Collider other)
    {
        GameManager.Instance.CheckForHighscore();
        UIManager.Instance.ShowEndGameUI();
        
        other.GetComponent<Rigidbody>().isKinematic = false;
        other.GetComponent<PlayerController>().enabled = false;

        yield return new WaitForSeconds(1f);
        
        if(other != null)
        {
            Destroy(other.gameObject);
        }        
    }
}
