using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TobiTools.Properties;
using System.IO;

namespace TobiTools
{
    public abstract class Object
    {
        protected WowCoordinates coord = new WowCoordinates();

        protected Rectangle BoundingBox;

        public abstract void Render(Graphics g);
    }

    public class Unit : Object
    {
        protected Image masterImg = null;

        public Unit(string fileName)
        {
            string file = Settings.Default.ImgFolder + Path.DirectorySeparatorChar + fileName;


            masterImg = Image.FromFile(file);
        }

        public override void Render(Graphics g)
        {
            if (masterImg == null)
                return;

            g.DrawImage(masterImg, coord.GetScreenCoord());
        }
    }

    public class Master : Unit
    {
        public Master()
            : base("master.png")
        {
            if (masterImg == null)
                return;

            coord.SetScreenPosition(new Point(0, 0));
            Point rectPos = new Point(0 - masterImg.Width / 2, 0 - masterImg.Height / 2);
            BoundingBox.Location = rectPos;
            BoundingBox.Height = masterImg.Height;
            BoundingBox.Width = masterImg.Width;
        }
    }

    public class Slave : Unit
    {
        public Slave(Point pos)
            : base("slave.png")
        {
            if (masterImg == null)
                return;

            coord.SetScreenPosition(pos);
            Point rectPos = new Point(pos.X - masterImg.Width / 2, pos.Y - masterImg.Height / 2);
            BoundingBox.Location = rectPos;
            BoundingBox.Height = masterImg.Height;
            BoundingBox.Width = masterImg.Width;
        }

        public void SetScreenPosition(Point newPos)
        {
            coord.SetScreenPosition(newPos);
            Point rectPos = new Point(newPos.X - masterImg.Width / 2, newPos.Y - masterImg.Height / 2);
            BoundingBox.Location = rectPos;
        }

        public void SetGamePosition(PointF newPos)
        {
            coord.SetGamePosition(newPos);
            Point pos = coord.GetScreenCoord();
            Point rectPos = new Point(pos.X - masterImg.Width / 2, pos.Y - masterImg.Height / 2);
            BoundingBox.Location = rectPos;
        }
    }
}
