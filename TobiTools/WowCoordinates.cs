using System;
using System.Drawing;

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
        private PointF GameCoord;
        static public int ClientXOffset = 190;
        static public int ClientYOffset = 190;

        public WowCoordinates(int width = 0, int height = 0)
        {
            if (width > 0 && height > 0)
            {
                ClientXOffset = width / 2;
                ClientYOffset = height / 2;

                xRatio = MapSize / width;
                yRatio = MapSize / height;
            }
            else
            {
                ClientXOffset = ScreenSizeW / 2;
                ClientYOffset = ScreenSizeH / 2;

                xRatio = MapSize / ScreenSizeW;
                yRatio = MapSize / ScreenSizeH;
            }
            SetScreenPosition(new Point(0 , 0));
        }

        public void SetScreenPosition(Point newPos)
        {
            GameCoord.X = -((newPos.Y * xRatio) - MaxMapCoord);
            GameCoord.Y = -((newPos.X * yRatio) - MaxMapCoord);
            screenCoord = newPos;
            screenCoord.X = newPos.X + ClientXOffset;
            screenCoord.Y = -newPos.Y + ClientYOffset;
            //Console.WriteLine("Game x= " + m_currentPosX.ToString() + " y=" + m_currentPosY.ToString());
            SetTile();
        }
        public void SetGamePosition(PointF newPos)
        {
            GameCoord = newPos;
            screenCoord.X = Convert.ToInt32(Math.Truncate((-GameCoord.Y + MaxMapCoord) / yRatio)) + ClientXOffset;
            screenCoord.Y = Convert.ToInt32(Math.Truncate((-GameCoord.X + MaxMapCoord) / xRatio)) + ClientYOffset;
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
