using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNodeStateComparer : IComparer<CarryNodeState>
{
    readonly Vector2 TargetVector;

    public CarryNodeStateComparer(Vector2 targetVector)
    {
        TargetVector = targetVector;
    }

    public int Compare(CarryNodeState x, CarryNodeState y)
    {
        return (int) (AStarExpectedTotalDistance(x) - AStarExpectedTotalDistance(y));
    }

    float AStarExpectedTotalDistance(CarryNodeState state)
    {
        return state.DistanceTravelled + AStarHeuristic(state);
    }

    float AStarHeuristic(CarryNodeState state)
    {
        return Vector2.Distance(state.Node.XZVector, TargetVector);
    }
}
