using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameVariables")]
public class GameVariables : ScriptableObject
{
    public int BasePlayerHealth, BasePlayerDamage, BaseEnemyHealth, BaseEnemyDamage, EnemyIncreasementWithStage, BaseEnemyQuantity;
}
