﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        protected DataGridViewRow linkedRow;
        static public int ClientXOffset = 190;
        static public int ClientYOffset = 190;

        public abstract void Render(Graphics g);
        public bool Contain(Point point)
        {
            return BoundingBox.Contains(point);
        }

        public string GetGameCoord()
        {
            if (coord != null)
                return coord.GetGameCoord().ToString();
            return "Invalid";
        }

        public void SetBaseGamePos(float x, float y, float o)
        {
            coord.SetBaseGamePos(x, y, o);
        }

        public void SetLinkedRow(DataGridViewRow row) { linkedRow = row; }
        public DataGridViewRow GetLinkedRow() { return linkedRow; }

        public abstract string GetName();

        protected void _SetScreenPosition(Point pos)
        {
            ScreenStartPos.X = ClientXOffset - (masterImg.Width / 2) + pos.X;
            ScreenStartPos.Y = ClientYOffset - (masterImg.Height / 2) - pos.Y;
            Point rectPos = new Point(pos.X - masterImg.Width / 2, pos.Y - masterImg.Height / 2);
            BoundingBox.Location = rectPos;
            BoundingBox.Height = masterImg.Height;
            BoundingBox.Width = masterImg.Width;
        }
    }

    public abstract class Unit : Object
    {
        public Unit(string fileName, Size screenSize)
        {
            string file = Settings.Default.ImgFolder + Path.DirectorySeparatorChar + fileName;


            masterImg = Image.FromFile(file);

            if (masterImg == null)
                return;

            coord = new WowCoordinates();
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

        public override string GetName() { return "Master"; }

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
        public override string GetName() { return "Slave"; }

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
