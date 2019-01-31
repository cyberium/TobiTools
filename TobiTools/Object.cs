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
        protected Image masterImg = null;
        protected WowCoordinates coord;
        protected Rectangle BoundingBox;
        protected Point ScreenStartPos;

        public abstract void Render(Graphics g);
        public bool Contain(Point point)
        {
            return BoundingBox.Contains(point);
        }

        protected void _SetScreenPosition(Point pos)
        {
            ScreenStartPos.X = WowCoordinates.ClientXOffset - (masterImg.Width / 2) + pos.X;
            ScreenStartPos.Y = WowCoordinates.ClientYOffset - (masterImg.Height / 2) - pos.Y;
            Point rectPos = new Point(pos.X - masterImg.Width / 2, pos.Y - masterImg.Height / 2);
            BoundingBox.Location = rectPos;
            BoundingBox.Height = masterImg.Height;
            BoundingBox.Width = masterImg.Width;
        }
    }

    public class Unit : Object
    {
        public Unit(string fileName, Size screenSize)
        {
            string file = Settings.Default.ImgFolder + Path.DirectorySeparatorChar + fileName;


            masterImg = Image.FromFile(file);

            if (masterImg == null)
                return;

            coord = new WowCoordinates(screenSize.Width, screenSize.Height);
        }

        public override void Render(Graphics g)
        {
            if (masterImg == null)
                return;
            g.DrawImage(masterImg, ScreenStartPos);

            /*Rectangle rect = BoundingBox;
            rect.X += WowCoordinates.ClientXOffset;
            rect.Y = WowCoordinates.ClientYOffset - rect.Y - BoundingBox.Height;
            g.DrawRectangle(Pens.Pink, rect);*/
        }
    }

    public class Master : Unit
    {
        public Master(Size screenSize)
            : base("master.png", screenSize)
        {
            if (masterImg == null)
                return;
            _SetScreenPosition(new Point(0, 0));
        }
    }

    public class Slave : Unit
    {
        public Slave(Size screenSize)
            : base("slave.png", screenSize)
        {
            if (masterImg == null)
                return;
            _SetScreenPosition(new Point(0, 0));
        }

        public void SetScreenPosition(Point newPos)
        {
            coord.SetScreenPosition(newPos);
            _SetScreenPosition(newPos);
        }

        public void SetGamePosition(PointF pos)
        {
            coord.SetGamePosition(pos);
            Point newPos = coord.GetScreenCoord();
            _SetScreenPosition(new Point(0, 0));
        }
    }
}
