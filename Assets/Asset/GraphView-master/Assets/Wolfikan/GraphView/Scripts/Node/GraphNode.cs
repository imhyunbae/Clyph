using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Wolfikan.GraphView
{
    public class GraphNode : Node
    {
        public string GUID;
        public string Name;
       
        public GraphPort Port;
        
        public GraphNode(GraphNodeAsset asset)
        {
            GUID = asset.GUID;
            Name = asset.Name;
            Port = asset.Port;
        }

        public GraphNode(string GUID, string Name, GraphPort port)
        {
            this.GUID = GUID;
            this.Name = Name;
            this.Port = port;
        }

        public override Port InstantiatePort(Orientation orientation, Direction direction, Port.Capacity capacity, Type type)
        {
            
            return base.InstantiatePort(orientation, direction, capacity, type);
        }
    }
}