using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // UI Elements 
    private Image _heartImage;
    private TextMeshProUGUI _coinText;
    private GameObject _menuPanel;
    private Slider _sfxSlider;
    private Slider _musicSlider;

    // Vars 
    private int _coinCount = 0;
    
    // Components 
    private AudioSourceController _audioSourceController;


    // Start is called before the first frame update
    void Start()
    {
        // Connect UI
        if (GameObject.Find(Structs.UI.heartImage))
        {
            _heartImage = GameObject.Find(Structs.UI.heartImage).GetComponent<Image>(); HeartImageUpdate(1);
            HeartImageUpdate(1);
        }

        if (GameObject.Find(Structs.UI.coinText))
        {
            _coinText = GameObject.Find(Structs.UI.coinText).GetComponent<TextMeshProUGUI>();
        }

        if (GameObject.Find(Structs.UI.coins))
        {
            // Get Count 
            _coinCount = GameObject.Find(Structs.UI.coins).transform.childCount;
            CoinTextUpdate(0);
        }

        _sfxSlider = GameObject.Find(Structs.UI.sfxSlider).GetComponent<Slider>();
        _musicSlider = GameObject.Find(Structs.UI.musicSlider).GetComponent<Slider>();
        _menuPanel = GameObject.Find(Structs.UI.panel);

        // Get Component 
        _audioSourceController = GameObject.FindAnyObjectByType<AudioSourceController>();

        // Update UI 
        _menuPanel.SetActive(false);
        SetSliders();
    }

    // Checks if player action on menu 
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // Turns menu on/off 
            _menuPanel.SetActive(!_menuPanel.active);
        }
    }

    // Turns off menu 
    public void BackButton() {  _menuPanel.SetActive(false); }

    // Turns off menu 
    public void OptionsButton() { _menuPanel.SetActive(true); }

    // Sends player to main menu 
    public void MenuButton()  {   SceneManager.LoadScene(Structs.Scenes.menu);}

    // Sends player to first level 
    public void FirstLevelButton() { SceneManager.LoadScene(Structs.Scenes.firstLevel); }

    // Updates Heart image 
    public void HeartImageUpdate(float newAmount){ _heartImage.fillAmount = newAmount;}

    // Updates Coin Text 
    public void CoinTextUpdate(int newAmount) {_coinText.text = newAmount + " / " + _coinCount;}

    public void SetSliders()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat(Structs.Mixers.sfxVolume);
        _musicSlider.value = PlayerPrefs.GetFloat(Structs.Mixers.musicVolume);
    }
    
}
