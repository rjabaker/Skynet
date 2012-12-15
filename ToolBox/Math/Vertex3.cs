using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Math
{
    public struct Vertex3
    {
        #region Private Variables

        double[] coordinates;

        #endregion

        #region Constructors

        public Vertex3(double x, double y)
        {
            this.coordinates = new double[] { x, y, 0 };
        }
        public Vertex3(double x, double y, double z)
        {
            this.coordinates = new double[] { x, y, z };
        }

        #endregion

        #region Properties

        public double X
        {
            get
            {
                return coordinates[0];
            }
        }
        public double Y
        {
            get
            {
                return coordinates[1];
            }
        }
        public double Z
        {
            get
            {
                return coordinates[2];
            }
        }

        #endregion

        #region Operators

        public static Vertex3 operator +(Vertex3 a, Vertex3 b)
        {
            return new Vertex3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vertex3 operator -(Vertex3 a, Vertex3 b)
        {
            return new Vertex3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vertex3 operator *(Vertex3 a, double d)
        {
            return new Vertex3(a.X * d, a.Y *d, a.Z * d);
        }
        public static Vertex3 operator /(Vertex3 a, double d)
        {
            return new Vertex3(a.X / d, a.Y / d, a.Z / d);
        }

        #endregion
    }
}
