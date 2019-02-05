using System;
using System.Drawing;
using System.Numerics;

namespace TobiTools
{
    public class WowCoordinates
    {
        static public int TileNum = 64; // square 64 * 64
        static public float TileGameSize = 533.3333333f;
        static public float MapSize = TileNum * TileGameSize;
        static public float MaxMapCoord = MapSize / 2.0F;

        static public int ScreenSizeW = 380;
        static public int ScreenSizeH = 380;

        private float xRatio = 1;
        private float yRatio = 1;
        private int m_currentTx = 0;
        private int m_currentTy = 0;
        private Point screenCoord;
        private Point VirtualCoord;
        private PointF GameCoord;
        private PointF BasePoint;
        private float BasePointO = 0;

        public int ClientRatio = 10;

        public WowCoordinates()
        {
            BasePoint = new Point(0, 0);
            SetScreenPosition(new Point(0 , 0));
        }

        public void SetBaseGamePos(float x, float y, float o)
        {
            BasePoint.X = x;
            BasePoint.Y = y;
            BasePointO = o;
            RefreshGamePos();
        }

        /// <summary>
        /// Rotates one point around another
        /// </summary>
        /// <param name="pointToRotate">The point to rotate.</param>
        /// <param name="centerPoint">The center point of rotation.</param>
        /// <param name="angleInDegrees">The rotation angle in degrees.</param>
        /// <returns>Rotated point</returns>
        static PointF RotatePoint(PointF pointToRotate, PointF centerPoint, float angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new PointF
            {
                X =
                (float)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                (float)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        private void RefreshGamePos()
        {
            PointF pos = new PointF(0, 0);
            pos.X = (VirtualCoord.Y / (float)ClientRatio) + BasePoint.X;
            pos.Y = (-VirtualCoord.X / (float)ClientRatio) + BasePoint.Y;

            PointF res = pos;
            if (BasePointO != 0)
                res = RotatePoint(pos, BasePoint, BasePointO);
            GameCoord.X = res.X;
            GameCoord.Y = res.Y;
        }

        public void SetScreenPosition(Point newPos)
        {
            screenCoord = newPos;
            VirtualCoord = newPos;
            screenCoord.X = newPos.X;
            screenCoord.Y = -newPos.Y;
            RefreshGamePos();
            //Console.WriteLine("Game x= " + m_currentPosX.ToString() + " y=" + m_currentPosY.ToString());
            SetTile();
        }
        public void SetGamePosition(PointF newPos)
        {
            GameCoord = newPos;
            screenCoord.X = Convert.ToInt32(Math.Truncate((-GameCoord.Y + MaxMapCoord) / yRatio));
            screenCoord.Y = Convert.ToInt32(Math.Truncate((-GameCoord.X + MaxMapCoord) / xRatio));
            VirtualCoord.X = screenCoord.X;
            VirtualCoord.Y = -screenCoord.Y;
            SetTile();
        }

        private void SetTile()
        {
            double yTile = (MaxMapCoord - GameCoord.X) / TileGameSize;
            double xTile = (MaxMapCoord - GameCoord.Y) / TileGameSize;

            m_currentTx = Convert.ToInt32(Math.Truncate(xTile));
            m_currentTy = Convert.ToInt32(Math.Truncate(yTile));
        }

        public Point GetScreenCoord()
        {
            return screenCoord;
        }

        public PointF GetGameCoord()
        {
            return GameCoord;
        }

        public void GetCurrentTile(ref int tx, ref int ty)
        {
            tx = m_currentTx;
            ty = m_currentTy;
        }
    }
}
