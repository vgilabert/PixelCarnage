/*using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

public class CrowdController : MonoSingleton<CrowdController>
{
    [SerializeField] private float refreshRate = 1f;
    [SerializeField] bool useRefreshRate = true; 
        
    private KdTree<EnemyBase> EnemyList { get; } = new (true);
    
    public void AddEnemy(EnemyBase enemy)
    {
        EnemyList.Add(enemy);
    }
    
    public void RemoveEnemy(EnemyBase enemy)
    {
        EnemyList.Remove(enemy);
    }
    
    public EnemyBase[] GetEnemiesInRange(Vector3 position, float range)
    {
        return EnemyList.FindWithinRadius(position, range).ToArray();
    }
    
    private void Update()
    {
        if (useRefreshRate)
        {
            if (Time.time % refreshRate == 0)
            {
                EnemyList.UpdatePositions();
            }
        }
        else
        {
            EnemyList.UpdatePositions();
        }
    }
}*/