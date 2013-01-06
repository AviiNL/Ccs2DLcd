using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ccs2DLcd
{
    public struct Vector2// : IEquatable<Vector2>
    {
        public float X;
        public float Y;

        //private System.Drawing.Point p;// = new System.Drawing.Point();

        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0, 0);
            }
            private set { }
                
        }

        
        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public System.Drawing.Point ToPoint()
        {
            return new System.Drawing.Point((int)X, (int)Y);
        }

        public override string ToString()
        {
            return "{X=" + X + ",Y=" + Y + "}";
        }

        #region Self Operators
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vector2 operator /(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        }
        #endregion

        #region Operators with diffrent type (float)
        public static Vector2 operator +(Vector2 v1, float v2)
        {
            return new Vector2(v1.X + v2, v1.Y + v2);
        }

        public static Vector2 operator -(Vector2 v1, float v2)
        {
            return new Vector2(v1.X - v2, v1.Y - v2);
        }

        public static Vector2 operator *(Vector2 v1, float v2)
        {
            return new Vector2(v1.X * v2, v1.Y * v2);
        }

        public static Vector2 operator /(Vector2 v1, float v2)
        {
            return new Vector2(v1.X / v2, v1.Y / v2);
        }
        #endregion
    }
}
