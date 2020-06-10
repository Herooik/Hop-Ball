using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformGenerator : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform platformPrefab;
    [SerializeField][Range(1, 10)] private int amountToSpawnAtStart = 5;
    [SerializeField] private float spawnDistanceRange = 30f;
    [SerializeField][Range(-3,0)] private float minPosX = -3f;
    [SerializeField][Range(0,3)] private float maxPosX = 3f;
    
    [Header("Pick Up")]
    [SerializeField] [Range(0, 1)] private float pickUpChance = 0.6f;

    [Header("Scale Changer")] 
    [SerializeField][Range(0.5f, 2)] private float maxScaleDecrease = 1f;
    [SerializeField][Range(0, 0.5f)] private float scaleToSubtract = 0.05f;
    [SerializeField] private IntReference score;

    private bool _changeScale;
    private float _xScale;
    private Vector3 _newScale;

    private void Start()
    {
        SpawnBlocksAtStart();
        _xScale = platformPrefab.localScale.x;
        _newScale = platformPrefab.localScale;
    }

    private void SpawnBlocksAtStart()
    {
        for (int i = 1; i < amountToSpawnAtStart; i++)
        {
            var platform = PlatformDropPool.Instance.GetPooledObject();
            platform.SetActive(true);
            platform.transform.position = new Vector3(0, -0.75f, 2 * i * Mathf.PI);
            transform.position = platform.transform.position;
        }
    }

    private void Update()
    {
        SpawningBlock();

        CheckingForScaleChange();
    }

    private void CheckingForScaleChange()
    {
        if (score.Value % 10 == 0 && score.Value != 0 && !_changeScale)
        {
            _changeScale = true;
            if (_newScale.x <= maxScaleDecrease) return;
            StartCoroutine(ChangingScale());
        }
    }

    private IEnumerator ChangingScale()
    {
        _xScale -= scaleToSubtract;
        _newScale.x = _xScale;
        yield return new WaitForSeconds(2f);
        _changeScale = false;
    }

    private void SpawningBlock()
    {
        if (playerPos == null) return;
        
        if (Vector3.Distance(playerPos.position, transform.position) < spawnDistanceRange)
        {
            var nextPointPos = transform.position.z + 2 * Mathf.PI;
            var newXPos = Random.Range(minPosX, maxPosX);
            transform.position = new Vector3(newXPos, -0.75f, nextPointPos);
            
            var platform = PlatformDropPool.Instance.GetPooledObject();
            platform.transform.localScale = _newScale;
            platform.transform.position = transform.position;
            platform.SetActive(true);

            SpawningPickUp(newXPos, nextPointPos, platform.transform);
        }
    }
    private void SpawningPickUp(float newXPos, float nextPointPos, Transform parent)
    {
        if (Random.value < pickUpChance)
        {
            var pickUp = PickUpDropPool.Instance.GetPooledObject();
            pickUp.transform.position = new Vector3(newXPos, 0, nextPointPos);
            pickUp.SetActive(true);
            pickUp.transform.SetParent(parent);
        }
    }
}
