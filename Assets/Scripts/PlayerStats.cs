using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    // Componets 
    private Rigidbody2D _rigidbody2D;
    private AudioSourceController _audioSourceController;
    private UIController _uIController;

    // Resapwn 
    public Transform _respawnPoint;

    // Counters 
    public int _playerLife = 3;
    private float _maxHealth = 3.0f; 
    public int _playerCoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSourceController = GameObject.FindAnyObjectByType<AudioSourceController>();
        _uIController = GameObject.FindAnyObjectByType<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gets what the tag is 
        string colTag = collision.tag;


        // Switches between diffrent actions 
        switch (colTag)
        {
            // Player dies 
            case Structs.Tags.deathTag:
                {
                    // Stops player from moving, moves them to the new positon and takes away one life 
                    _rigidbody2D.velocity = Vector2.zero;
                    transform.position = _respawnPoint.position;
                    _playerLife--;
                    _uIController.HeartImageUpdate(_playerLife / _maxHealth);
                    _audioSourceController.PlaySFX(Structs.SoundEffects.death);
                    // If the player has 0 or less lives reset the level 
                    if ( _playerLife <= 0)
                    {
                        string SceneName = SceneManager.GetActiveScene().name;
                        SceneManager.LoadScene(SceneName);
                    }
                    return;
                }
            // Player gains health 
            case Structs.Tags.healthTag:
                {
                    if(_playerLife >= 3) { return; }
                    // Gain one health and destory the object 
                    _playerLife++;
                    _uIController.HeartImageUpdate(_playerLife / _maxHealth);
                    _audioSourceController.PlaySFX(Structs.SoundEffects.heart);
                    Destroy(collision.gameObject);
                    return;
                }
            // Gain Coin 
            case Structs.Tags.coinTag:
                {
                    // Gain one coin and destory the object 
                    _playerCoin++;
                    _uIController.CoinTextUpdate(_playerCoin);
                    _audioSourceController.PlaySFX(Structs.SoundEffects.coin);
                    Destroy(collision.gameObject);
                    return;
                }
            // Update Respawn Point 
            case Structs.Tags.respawnTag:
                {
                    // Saves the collison points location to the respawn transform 
                    _audioSourceController.PlaySFX(Structs.SoundEffects.checkpoint);
                    _respawnPoint = collision.gameObject.transform.Find("Point").transform;
                    return;
                }
            // Player Ends Level 
            case Structs.Tags.finishTag:
                {
                    // Gets level name from the object and gets moved there 
                    string nextLevel = collision.GetComponent<EndLevel>().nextLevel;
                    SceneManager.LoadScene(nextLevel);
                    return;
                }
        }
    }


}
