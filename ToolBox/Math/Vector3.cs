using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Math
{
    public struct Vector3
    {
        #region Private Variables

        private Vertex3[] line;

        #endregion

        #region Constructors

        public Vector3(Vertex3 point)
        {
            this.line = new Vertex3[] { new Vertex3(), point };
        }
        public Vector3(Vertex3 startVertex, Vertex3 endVertex)
        {
            this.line = new Vertex3[] { new Vertex3(), endVertex - startVertex };
        }

        #endregion

        #region Properties

        public Vertex3 Point
        {
            get
            {
                return line[1];
            }
        }

        #endregion

        #region Public Methods

        public Vector3 UnitVector()
        {
            return new Vector3(Point / Magnitude());
        }
        public double Magnitude()
        {
            return System.Math.Sqrt(Point.X * Point.X + Point.Y * Point.Y + Point.Z * Point.Z);
        }

        #endregion

        #region Operators

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.Point + b.Point);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.Point - b.Point);
        }
        public static double operator *(Vector3 a, Vector3 b)
        {
            // Scalar dot product of two vectors.
            return (a.Point.X * b.Point.X) + (a.Point.Y * b.Point.Y) + (a.Point.Z * b.Point.Z);
        }
        public static Vector3 operator *(Vector3 a, double d)
        {
            return new Vector3(a.Point * d);
        }
        public static Vector3 operator /(Vector3 a, double d)
        {
            return new Vector3(a.Point / d);
        }

        #endregion
    }
}
