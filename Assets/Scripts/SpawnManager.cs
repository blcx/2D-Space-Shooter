﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] _powerUps;




    public void StartSpawning()
    {

        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }







    // Update is called once per frame
    void Update()
    {



        
    }




    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (_stopSpawning == false)
        {
            
            Vector3 posToSpawn = new Vector3(Random.Range(-8f,8f), 7,0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        
        
        }

    
    
    }


    IEnumerator PowerUpSpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {


            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int randomPowerUp = Random.Range(0, 3);

            Instantiate(_powerUps[randomPowerUp], posToSpawn, Quaternion.identity);


            yield return new WaitForSeconds(Random.Range(3,8));


        }



    }




    public void OnPlayerDeath()
    {

        _stopSpawning = true;
    
    }


























}
