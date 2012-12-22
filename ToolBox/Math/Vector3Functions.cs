using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolBox.Math
{
    public static class Vector3Functions
    {
        public static double Angle(Vector3 a, Vector3 b)
        {
            double dot = (a * b);
            double ab = a.Magnitude() * b.Magnitude();

            return System.Math.Acos(dot / ab);
        }
    }
}
