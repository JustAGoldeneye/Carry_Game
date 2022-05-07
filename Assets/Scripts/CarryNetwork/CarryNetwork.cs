using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNetwork : MonoBehaviour
{
    CarryNode[] Nodes;
    CarryNode TargetNode;
    List<CarryNode> Route;
    float[] DistanceFromStart;

    void Start()
    {
        Nodes = gameObject.GetComponentsInChildren<CarryNode>();

        Debug.Log(FindRoute(-24, 0, Nodes[0])[0].name);
    }

    public CarryNode[] FindRoute(float startX, float startZ, CarryNode targetNode)
    {
        TargetNode = targetNode;
        Route = new List<CarryNode>();

        Route.Add(ClosestNodeToLocation(startX, startZ));

        DistanceFromStart = new float[Nodes.Length];
        for (int i = 0; i < DistanceFromStart.Length; i++)
        {
            DistanceFromStart[i] = float.MaxValue;
            // This will define the distance from the start node to itself as MaxValue but this should not matter as it should never be used
        }

        // TODO

        return Route.ToArray();
        // Returns a list of all nodes on the route including the first one that needs to be reached and the target node
    }

    void AStar(CarryNode current)
    {
        foreach (CarryNode neighbor in current.Neighbors)
        {
            // JATKA TÄÄLTÄ, katso mallia Wikipediasta
        }
    }

    float AStarHeuristic(CarryNode node)
    {
        return Vector2.Distance(node.XZVector, TargetNode.XZVector);
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
