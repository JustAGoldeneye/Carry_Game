using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryNode : MonoBehaviour
{
    public List<CarryNode> Neighbors;
    [HideInInspector] public Vector2 XZVector { get; private set; }

    void Awake()
    {
        XZVector = new Vector2(transform.position.x, transform.position.z);
    }
}
