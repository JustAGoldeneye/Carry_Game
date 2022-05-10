using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNetwork : MonoBehaviour
{
    CarryNode[] Nodes;
    Dictionary<string, CarryNode> NodeDictionary;

    void Start()
    {
        Nodes = gameObject.GetComponentsInChildren<CarryNode>();

        NodeDictionary = new Dictionary<string, CarryNode>();
        foreach (CarryNode node in Nodes)
        {
            NodeDictionary.Add(node.name, node);
        }

        // TEST AREA
        Vector2[] route1 = FindRouteToSite(-25, 25);
        foreach (Vector2 vector in route1)
        {
            Debug.Log(vector);
        }
        Vector2[] route2 = FindRouteByTargetName(-25, 25, "CarryNode Pointless Side 1");
        foreach (Vector2 vector in route2)
        {
            Debug.Log(vector);
        }
        // TEST AREA
    }

    public Vector2[] FindRouteToSite(float startX, float startZ)
    {
        return FindRouteByTargetName(startX, startZ, "CarryNode Site");
    }

    public Vector2[] FindRouteByTargetName(float startX, float startZ, string name)
    {
        if (NodeDictionary.TryGetValue(name, out CarryNode node))
        {
            return FindRoute(startX, startZ, node);
        }
        throw new System.Exception("No CarryNode with name " + name + " was found.");
    }

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
        // Using an external library (C5) for priority queue
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
