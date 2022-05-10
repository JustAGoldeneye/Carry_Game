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
            throw new System.Exception("Null not allowed as an assigned value for previousState, use CarryNodeState(CarryNode node, Vector3 startCords) instead.");
        } else
        {
            DistanceTravelled = Vector3.Distance(node.transform.position, previousState.Node.transform.position) + previousState.DistanceTravelled;
        }
    }

    public CarryNodeState(CarryNode node, Vector3 startCords)
    {
        Node = node;
        PreviousState = null;
        DistanceTravelled = Vector3.Distance(node.transform.position, startCords);
    }
}
