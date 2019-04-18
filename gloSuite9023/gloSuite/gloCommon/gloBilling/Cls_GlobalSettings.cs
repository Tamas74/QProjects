using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace gloBilling
{
    class Cls_GlobalSettings
    {
        public static Boolean IsPaymentVoided { get; set; }

        public static void ControlMover(Control control, Control container)
        {
            bool Dragging = false;
            Point DragStart = Point.Empty;
            control.MouseDown += delegate(object sender, MouseEventArgs e)
            {
                Dragging = true;
                DragStart = new Point(e.X, e.Y);
                control.Capture = true;
            };
            control.MouseUp += delegate(object sender, MouseEventArgs e)
            {
                Dragging = false;
                control.Capture = false;
            };
            control.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (Dragging)
                {
                    //if (direction != Direction.Vertical)
                    container.Left = Math.Max(0, e.X + container.Left - DragStart.X);
                    //if (direction != Direction.Horizontal)
                    container.Top = Math.Max(0, e.Y + container.Top - DragStart.Y);
                }
            };
        }
    }
}
