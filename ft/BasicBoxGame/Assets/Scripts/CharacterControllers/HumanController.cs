using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    const string strEnemy = "Enemy";
    const string strPlayer = "Player";

    public LayerMask layerEnemy, layerPlayer;
    public Human character;

    public bool isPlayer;


    void Update()
    {
        character.OnUpdate();
    }

    void LateUpdate()
    {
        character.OnLateUpdate();
    }

    public void AttackEvent()
    {
        character.Attack();
    }

    public void Reset(int health, int damage)
    {
        if(isPlayer) character = new Player(gameObject, layerEnemy);
        else
        {
            character = new Enemy(gameObject, layerPlayer);
        }

        character.OnStart();

        character.Reset(health, damage);
    }
}
