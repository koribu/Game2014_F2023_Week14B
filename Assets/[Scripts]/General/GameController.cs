using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject _onScreenController;
    [SerializeField]
    GameObject _miniMap,_miniMapOpenButton,_miniMapCloseButton,_toggleButton
        ;

    SoundManager _soundManager;

    [SerializeField]
    bool _isTesting;

    // Start is called before the first frame update
    void Start()
    {
        if(!_isTesting)
        {
            _onScreenController = GameObject.Find("OnScreenController");
            _onScreenController.SetActive(Application.platform != RuntimePlatform.WindowsEditor &&
                                          Application.platform != RuntimePlatform.WindowsPlayer);
        }

        _soundManager = FindObjectOfType<SoundManager>();


        if(SceneManager.GetActiveScene().Equals(SceneManager.GetSceneAt(0)))
        {
            _soundManager.PlayMusic(Sound.MAIN_MUSIC);
        }
        else if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneAt(1)))
        {
            _soundManager.PlayMusic(Sound.GAMEOVER_MUSIC);
        }


        BulletManager.Instance();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOverScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleMap()
    {
        _miniMap.SetActive(!_miniMap.active);

        if(_miniMap.active)
        {
            _toggleButton.transform.position = _miniMapOpenButton.transform.position;
        }
        else
        {
            _toggleButton.transform.position = _miniMapCloseButton.transform.position;
        }
    }

}
