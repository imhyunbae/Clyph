using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
public class GraphEdge : IEdgeConnectorListener
{
    public void OnDrop(GraphView graphView, Edge edge)
    {
        Debug.Log("31");
        throw new System.NotImplementedException();
    }

    public void OnDropOutsidePort(Edge edge, Vector2 position)
    {
        throw new System.NotImplementedException();
    }
}
