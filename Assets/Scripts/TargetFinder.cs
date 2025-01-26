using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Shared;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    private static ConcurrentDictionary<Damageable, Vector2> _targetPositions;
    
    private void Awake()
    {
        _targetPositions = new ConcurrentDictionary<Damageable, Vector2>();
    }

    private void FixedUpdate()
    {
        UpdateTargetPositions();
    }
    
    private void UpdateTargetPositions()
    {
        foreach (var target in _targetPositions)
        {
            _targetPositions[target.Key] = target.Key.transform.position;
        }
    }

    public static void AddTarget(Damageable target)
    {
        _targetPositions.TryAdd(target, target.transform.position);
    }
    
    public static void RemoveTarget(Damageable target)
    {
        _targetPositions.TryRemove(target, out _);
    }
    
    public static Damageable FindClosestTarget(Vector2 finderPosition, ref Vector3 targetPosition, HashSet<Damageable> excludedTargets = null, float radius = 50f)
    {
        Damageable closestTarget = null;
        float closestDistance = radius;
        foreach (var target in _targetPositions)
        {
            if (excludedTargets != null && excludedTargets.Contains(target.Key))
            {
                continue;
            }
            float distance = Vector2.Distance(finderPosition, target.Value);
            // If the target is closer than the current closest target and is not the finder itself
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target.Key;
                targetPosition = target.Value;
            }
        }
        return closestTarget;
    }

    public static Damageable[] FindTargetsInRadius(Vector2 position, float radius)
    {
        List<Damageable> targets = new List<Damageable>();
        foreach (var target in _targetPositions)
        {
            float distance = Vector2.Distance(position, target.Value);
            if (distance < radius)
            {
                targets.Add(target.Key);
            }
        }

        return targets.ToArray();
    }
    
    
}