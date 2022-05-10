using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNodeStateComparer : IComparer<CarryNodeState>
{
    readonly Vector3 TargetVector;

    public CarryNodeStateComparer(Vector3 targetVector)
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
        return Vector3.Distance(state.Node.transform.position, TargetVector);
    }
}
