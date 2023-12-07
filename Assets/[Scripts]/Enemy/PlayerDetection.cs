using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    [SerializeField]
    bool _IsSensedPlayer = false;
    [SerializeField]
    bool LOS = false;

    PlayerBehavior _player;

    [SerializeField]
    LayerMask _layerMask;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_IsSensedPlayer )
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, _player.transform.position, _layerMask);

            Vector2 playerDirectionVector = _player.transform.position - transform.position ;
            float playerDirectionValue = (playerDirectionVector.x > 0) ? 1.0f : -1.0f;  // if the value is 1 that means player is at the left of enemy, else it is at right
            float enemyLookingDirection = (transform.parent.localScale.x > 0) ? -1.0f : 1.0f; // if it is -1 that mean it looks to right else it looks to left

            LOS = (hit.collider.name == "Player") && (playerDirectionValue == enemyLookingDirection);
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _IsSensedPlayer = true;
        }
    }

    private void OnDrawGizmos()
    {
        Color drawColor = (LOS) ? Color.green : Color.red;

        if(_IsSensedPlayer)
        {
            Debug.DrawLine(transform.position, _player.transform.position,drawColor);
        }
    }
}
