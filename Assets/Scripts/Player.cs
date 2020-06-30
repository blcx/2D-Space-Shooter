using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private bool _isTrippleShotActive = false;
    private bool _isSpeedBoosterActive = false;
    private bool _isShieldsActive = false;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _Score;
    private UI_Manager _uimanager;

    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;

    //audio handle
    [SerializeField]
    private AudioClip _laserSoundClip;
    [SerializeField]
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _uimanager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        _audioSource.clip = _laserSoundClip;

    }




    /////////////////////////////////////////////////////////////////// Update is called once per frame
    void Update()
    {
        CalculateMovement();



        if (CrossPlatformInputManager.GetButtonDown("Jump") && Time.time > _canFire)
        {
            FireLaser();
        }





    }


    
    /// /////////////////////////////////////////////////////////////////////////////////////////////////////
   



    void CalculateMovement()
    {
        //gets values from controller
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

        //apply the value to translate
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);






        
            transform.Translate(direction * _speed * Time.deltaTime);

       



        //wrap and screen controls
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, transform.position.z);
        }


        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }


    }
    
    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    

    void FireLaser()
    {
            
        
            _canFire = Time.time + _fireRate;


        if (_isTrippleShotActive == true)
        {

            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }


        //play the laser audio clip
        _audioSource.Play();
        
    }



    public void Damage()
    {

        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);

        } else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uimanager.UpdateLives(_lives);

        if (_lives < 1)
        {

            _spawnManager.OnPlayerDeath();
            
            Destroy(this.gameObject);
        
        
        }
    
    
    }



    public void TrippleShotActive()
    {

        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    
    }




    IEnumerator TrippleShotPowerDownRoutine()
    {

        yield return new WaitForSeconds(5.0f);
        _isTrippleShotActive = false;

    
    }





    public void SpeedBoostActivator()
    {
        _isSpeedBoosterActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }



    IEnumerator SpeedBoostPowerDownRoutine()
    {

        yield return new WaitForSeconds(5f);
        _isSpeedBoosterActive = false;
        _speed /= _speedMultiplier;
    
    }



    public void ShieldsActive()
    {

        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
        
    }



    public void AddScore(int points)
    {

        _Score += points;
        _uimanager.UpdateScore(_Score);
    }















    /////////////////////////////////////////////////////////////////////////////////////////////////////////
}
