using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNode : MonoBehaviour
{
    public List<CarryNode> Neighbors;
    [HideInInspector] public Vector2 XZVector;

    private void Start()
    {
        XZVector = new Vector2(transform.position.x, transform.position.z);
    }
}
