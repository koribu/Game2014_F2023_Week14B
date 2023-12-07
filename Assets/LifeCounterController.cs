using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeCounterController : MonoBehaviour
{
    public int _currentLifeNumber;
    int _maxLifeNumber;

    Image _lifeCounterUI;

    // Start is called before the first frame update
    void Start()
    {
        _lifeCounterUI = GetComponent<Image>();

        _maxLifeNumber = 4;
        _currentLifeNumber = _maxLifeNumber;
    }

    public void LoseLife()
    {
        _currentLifeNumber--;

        if(_currentLifeNumber <= 0)
        {
            //Game Over

            SceneManager.LoadScene(1);
        }

        _lifeCounterUI.sprite = Resources.Load<Sprite>($"Sprites/LifeCounter/hud-{_currentLifeNumber}");
        FindObjectOfType<Deathplane>().SpawnPlayerToCheckpoint();
        FindObjectOfType<HealthBarController>().ResetHealth();
    }

}
