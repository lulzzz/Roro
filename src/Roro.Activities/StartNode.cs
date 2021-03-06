﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class StartNode : Node
    {
        public StartNode(Activity activity) : base(activity)
        {
            this.Ports.Add(new NextPort());
        }

        public override Guid Execute(IEnumerable<VariableNode> variables)
        {
            return this.Ports.First().NextNodeId;
        }

        public override bool CanEndLink => false;

        public override GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o)
        {
            var leftRect = new Rectangle(r.X, r.Y, r.Height, r.Height);
            var rightRect = new Rectangle(r.Right - r.Height, r.Y, r.Height, r.Height);
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftRect, 90, 180);
            path.AddArc(rightRect, -90, 180);
            path.CloseFigure();
            //
            g.FillPath(o.BackBrush, path);
            g.DrawPath(o.BorderPen, path);
            return path;
        }
    }
}
