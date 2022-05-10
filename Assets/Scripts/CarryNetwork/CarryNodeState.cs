using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNodeState
{
    public CarryNode Node { get; }
    public CarryNodeState PreviousState { get; }
    public float DistanceTravelled { get; private set; }

    public CarryNodeState(CarryNode node, CarryNodeState previousState)
    {
        Node = node;
        PreviousState = previousState;
        if (previousState == null)
        {
            throw new System.Exception("Null not allowed as an assigned value for previousState, use CarryNodeState(CarryNode node, Vector2 startCords) instead.");
        } else
        {
            DistanceTravelled = Vector2.Distance(node.XZVector, previousState.Node.XZVector) + previousState.DistanceTravelled;
        }
    }

    public CarryNodeState(CarryNode node, Vector2 startCords)
    {
        Node = node;
        PreviousState = null;
        DistanceTravelled = Vector2.Distance(node.XZVector, startCords);
    }
}
