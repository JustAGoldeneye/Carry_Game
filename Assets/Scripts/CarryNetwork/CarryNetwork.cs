using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNetwork : MonoBehaviour
{
    CarryNode[] Nodes;

    void Start()
    {
        Nodes = gameObject.GetComponentsInChildren<CarryNode>();

        // TEST AREA
        Vector2[] route = FindRoute(-25, 25, Nodes[0]);
        foreach (Vector2 vector in route)
        {
            Debug.Log(vector);
        }
        // TEST AREA
    }

    // TODO Find route by name (dictionary näille), Find route to start (pohjaa by nameen)

    public Vector2[] FindRoute(float startX, float startZ, CarryNode targetNode)
    // Returns a list of all nodes on the route including the first one that needs to be reached and the target node
    {
        CarryNode startNode = ClosestNodeToLocation(startX, startZ);

        CarryNodeState endState = AStar(startNode, targetNode, new Vector2(startX, startZ));
        Debug.Log(endState.DistanceTravelled);
        List<Vector2> route = new List<Vector2>();
        while (true)
        {
            route.Add(endState.Node.XZVector);
            if (endState.PreviousState == null)
            {
                break;
            }
            endState = endState.PreviousState;
        }
        route.Reverse();

        return route.ToArray();
    }

    CarryNodeState AStar(CarryNode startNode, CarryNode targetNode, Vector2 startCords)
    // Returns the entire route (including the first node), NOTE: I'm not completely sure that A* is working, but I have not been able to show that it is not.
    {
        C5.IntervalHeap<CarryNodeState> stateQueue = new C5.IntervalHeap<CarryNodeState>(new CarryNodeStateComparer(targetNode.XZVector));
        ISet<Vector2> vectorsVisited = new HashSet<Vector2>();

        stateQueue.Add(new CarryNodeState(startNode, startCords));
        vectorsVisited.Add(startNode.XZVector);

        while (stateQueue.Count > 0)
        {
            CarryNodeState currentState = stateQueue.FindMin();
            stateQueue.DeleteMin();
            if (currentState.Node.XZVector == targetNode.XZVector)
            {
                return currentState;
            }
            foreach (CarryNode neighbor in currentState.Node.Neighbors)
            {
                if (vectorsVisited.Contains(neighbor.XZVector))
                {
                    continue;
                }
                vectorsVisited.Add(neighbor.XZVector);
                stateQueue.Add(new CarryNodeState(neighbor, currentState));
            }
        }
        return null;
    }

    CarryNode ClosestNodeToLocation(float x, float z)
    {
        Vector2 startVector = new Vector2(x, z);
        CarryNode closestNode = Nodes[0];
        int i = 1;
        while (i < Nodes.Length)
        {
            if (Vector2.Distance(startVector, Nodes[i].XZVector) < Vector2.Distance(startVector, closestNode.XZVector))
            {
                closestNode = Nodes[i];
            }
            i++;
        }
        return closestNode;
    }
}
