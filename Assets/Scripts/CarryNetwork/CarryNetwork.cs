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
        Vector2[] route1 = FindRouteToSite(new Vector3(-25, 1, 25));
        foreach (Vector2 vector in route1)
        {
            Debug.Log(vector);
        }
        Vector2[] route2 = FindRouteByTargetName(new Vector3(-25, 1, 25), "CarryNode Pointless Side 1");
        foreach (Vector2 vector in route2)
        {
            Debug.Log(vector);
        }
        // TEST AREA
    }

    public Vector2[] FindRouteToSite(Vector3 startVector)
    {
        return FindRouteByTargetName(startVector, "CarryNode Site");
    }

    public Vector2[] FindRouteByTargetName(Vector3 startVector, string name)
    {
        if (NodeDictionary.TryGetValue(name, out CarryNode node))
        {
            return FindRoute(startVector, node);
        }
        throw new System.Exception("No CarryNode with name " + name + " was found.");
    }

    public Vector2[] FindRoute(Vector3 startVector, CarryNode targetNode)
    // Returns a list of all nodes on the route including the first one that needs to be reached and the target node
    // Takes in Vector 3 (Y distance matters for pathfinding algorithm)
    // but outputs Vector2 with X and Z coordinates (Y does not matter for actual movement as it should stay on gorund level) or certain distance above it
    {
        CarryNode startNode = ClosestNodeToLocation(startVector);

        CarryNodeState endState = AStar(startNode, targetNode, startVector);
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

    CarryNodeState AStar(CarryNode startNode, CarryNode targetNode, Vector3 startCords)
    // Returns the entire route (including the first node), NOTE: I'm not completely sure that A* is working, but I have not been able to show that it is not.
    {
        C5.IntervalHeap<CarryNodeState> stateQueue = new C5.IntervalHeap<CarryNodeState>(new CarryNodeStateComparer(targetNode.transform.position));
        // Using an external library (C5) for priority queue
        ISet<Vector3> vectorsVisited = new HashSet<Vector3>();

        stateQueue.Add(new CarryNodeState(startNode, startCords));
        vectorsVisited.Add(startNode.transform.position);

        while (stateQueue.Count > 0)
        {
            CarryNodeState currentState = stateQueue.FindMin();
            stateQueue.DeleteMin();
            if (currentState.Node.transform.position == targetNode.transform.position)
            {
                return currentState;
            }
            foreach (CarryNode neighbor in currentState.Node.Neighbors)
            {
                if (vectorsVisited.Contains(neighbor.transform.position))
                {
                    continue;
                }
                vectorsVisited.Add(neighbor.transform.position);
                stateQueue.Add(new CarryNodeState(neighbor, currentState));
            }
        }
        return null;
    }

    CarryNode ClosestNodeToLocation(Vector3 startVector)
    {
        CarryNode closestNode = Nodes[0];
        int i = 1;
        while (i < Nodes.Length)
        {
            if (Vector3.Distance(startVector, Nodes[i].transform.position) < Vector3.Distance(startVector, closestNode.transform.position))
            {
                closestNode = Nodes[i];
            }
            i++;
        }
        return closestNode;
    }
}
