using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    float _currentHealth;
    float _maxHealth;

    Slider _slider;

    LifeCounterController _lifeCounterController;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        _maxHealth = _slider.maxValue;
        _currentHealth = _maxHealth;

        _lifeCounterController = FindObjectOfType<LifeCounterController>();
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if( _currentHealth <= 0 )
        {
            //lose life
            _lifeCounterController.LoseLife();

            _currentHealth = _maxHealth;
        }

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        _slider.value = _currentHealth;
        if(_slider.value > 70)
        {
            _slider.image.color = Color.green;
        }
        else if (_slider.value> 40)
        {
            _slider.image.color = Color.yellow;
        }
        else
        {
            _slider.image.color = Color.red;
        }

    }  
    
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;

        UpdateHealthBar();
    }
}
