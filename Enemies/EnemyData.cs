using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{     
    public int health;   
    public int attack;    
    public float spawnChance;   
    public GameObject enemyPrefab;  
    public int exp;
    public int damageKnee;
    public int damageLeg;
    public int damageKneeAndLeg;
}
