using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathplane : MonoBehaviour
{
    Vector3 _spawnPoint = new Vector3(0, 5, 0);

    SoundManager _soundManager;

    private void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            collision.transform.position = _spawnPoint;

            _soundManager.PlaySound(Channel.PLAYER_DYING_CHANNEL, Sound.PLAYER_DYING_SFX);

            FindObjectOfType<LifeCounterController>().LoseLife();
        }
    }

    public void UpdateSpawnPoint(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;
    }

    public void SpawnPlayerToCheckpoint()
    {
        FindObjectOfType<PlayerBehavior>().transform.position = _spawnPoint;
    }
}
