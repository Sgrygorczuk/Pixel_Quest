using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    private InGameFadeOut fadeOut;
    private AudioSourceController _controller;

    void Start()
    {
        fadeOut = GetComponent<InGameFadeOut>(); 
        _controller = GetComponent<AudioSourceController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Gets what the tag is 
        string colTag = collision.tag;


        // Switches between different actions 
        switch (colTag)
        {
            // Player dies 
            case Structs.Tags.deathTag:
                {
                    _controller.PlaySFX(Structs.SoundEffects.death);
                    int sceneIndex = SceneManager.GetActiveScene().buildIndex;

                    // Stops player from moving, moves them to the new position and takes away one life 
                    fadeOut.StartFadeIn(() => {
                        Debug.Log("Reloading Scene Index: " + sceneIndex);
                        SceneManager.LoadScene(sceneIndex);
                    });
                    return;
                }
            case Structs.Tags.coinTag:
                {
                    _controller.PlaySFX(Structs.SoundEffects.coin);
                    Destroy(collision.gameObject);
                    return;
                }
            // Player Ends Level 
            case Structs.Tags.finishTag:
                {
                    _controller.PlaySFX(Structs.SoundEffects.win);
                    string levelName = collision.GetComponent<EndLevel>().nextLevel;
                    // Gets level name from the object and gets moved there 
                    fadeOut.StartFadeIn(() => 
                    {
                        Debug.Log(levelName);
                        SceneManager.LoadScene(levelName);
                    });
                    return;
                }
        }
    }


}
