using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityGraph = UnityEditor.Experimental.GraphView;

namespace Wolfikan.GraphView
{
    public class GraphView : UnityGraph.GraphView
    {
        private SearchProvider provider;
        public override EventPropagation DeleteSelection()
        {
            Debug.Log("1");
            return base.DeleteSelection();
        }
        public GraphView()
        {
            SetupZoom(UnityGraph.ContentZoomer.DefaultMinScale, UnityGraph.ContentZoomer.DefaultMaxScale);

			this.AddManipulator(new UnityGraph.ContentDragger());
			this.AddManipulator(new UnityGraph.SelectionDragger());
			this.AddManipulator(new UnityGraph.RectangleSelector());
          //  this.AddManipulator(new UnityGraph.EdgeConnector<Edge>(new GraphEdge()));
			var grid = new UnityGraph.GridBackground();
			Insert(0, grid);
			grid.StretchToParentSize();

            AddElement(GenerateGraphNode());
            AddElement(GenerateGraphNode());
        }

        private GraphNode GenerateGraphNode()
        {
            var asset = Resources.Load<GraphNodeAsset>("GraphNode");

            GraphNode node = new GraphNode(asset);

            node.title = node.Name;
            node.GUID = GUID.Generate().ToString();

            foreach (var input in node.Port.inputs)
            {
                var port = GeneratePort(node, UnityGraph.Direction.Input, UnityGraph.Port.Capacity.Single, input.Type);
                port.portName = input.Name;
               
                node.inputContainer.Add(port);
            }

            foreach (var output in node.Port.outputs)
            {
                var port = GeneratePort(node, UnityGraph.Direction.Output, UnityGraph.Port.Capacity.Multi, output.Type);
                port.portName = output.Name;
                port.ConnectTo((Port)node.inputContainer[0]);
                node.outputContainer.Add(port);
            }

            node.RefreshExpandedState();
            node.RefreshPorts();

            return node;
        }

        private UnityGraph.Port GeneratePort(GraphNode node, UnityGraph.Direction direction, UnityGraph.Port.Capacity capacity, Type type)
        {
         
            return node.InstantiatePort(UnityGraph.Orientation.Horizontal, direction, capacity, type);
        }
    }
}