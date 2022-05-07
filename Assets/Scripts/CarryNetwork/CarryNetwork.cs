using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNetwork : MonoBehaviour
{
    CarryNode[] Nodes;

    void Start()
    {
        Nodes = gameObject.GetComponentsInChildren<CarryNode>();

        Debug.Log(FindRoute(-24, 0, Nodes[0])[0].name);
    }

    CarryNode[] FindRoute(float startX, float startZ, CarryNode targetNode)
    {
        List<CarryNode> route = new List<CarryNode>();

        route.Add(ClosestNodeToLocation(startX, startZ));

        float[] distanceTravelled = new float[Nodes.Length];

        // TODO

        return route.ToArray();
        // Returns a list of all nodes on the route including the first one that needs to be reached and the target node
        // Uses A*
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
