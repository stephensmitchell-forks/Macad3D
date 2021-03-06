﻿using System;
using Macad.Common;
using Macad.Occt;

namespace Macad.Core
{
    public static class gpExtensions
    {
        #region gp_Pnt

        public static Pnt Round(this Pnt pnt)
        {
            return new Pnt(pnt.X.Round(), pnt.Y.Round(), pnt.Z.Round());
        }

        //--------------------------------------------------------------------------------------------------

        #endregion

        #region Pln

        public static Quaternion Rotation(this Pln plane)
        {
            var mat = new Mat(
                plane.XAxis.Direction.Coord,
                plane.YAxis.Direction.Coord,
                plane.Axis.Direction.Coord);
            return new Quaternion(mat);
        }

        //--------------------------------------------------------------------------------------------------

        public static bool IsEqual(this Pln pln1, Pln pln2)
        {
            return pln1.Location.IsEqual(pln2.Location, Double.Epsilon)
                   && pln1.Rotation().IsEqual(pln2.Rotation());
        }

        //--------------------------------------------------------------------------------------------------

        public static Pnt2d Parameters(this Pln pln, Pnt pnt)
        {
            double u = 0, v = 0;
            ElSLib.Parameters(pln, pnt, ref u, ref v);
            return new Pnt2d(u, v);
        }

        //--------------------------------------------------------------------------------------------------
        
        public static Pnt Value(this Pln pln, Pnt2d uv)
        {
            return ElSLib.Value(uv.X, uv.Y, pln);
        }

        //--------------------------------------------------------------------------------------------------

        #endregion

        #region Quaternion

        public static Ax3 ToAx3(this Quaternion rotation, Pnt location)
        {
            return new Ax3(location,
                rotation.Multiply(new Vec(0, 0, 1)).ToDir(),
                rotation.Multiply(new Vec(1, 0, 0)).ToDir());
        }

        //--------------------------------------------------------------------------------------------------

        public static (double yaw, double pitch, double roll) ToEuler(this Quaternion rotation)
        {
            double y = 0, p = 0, r = 0;
            rotation.GetEulerAngles(ref y, ref p, ref r);
            return (y, p, r);
        }

        //--------------------------------------------------------------------------------------------------

        #endregion

        #region gp_Ax22d

        public static int Sense(this Ax22d ax)
        {
            return ax.YAxis.Angle(ax.XAxis) > 0 ? 1 : -1;
        }

        //--------------------------------------------------------------------------------------------------

        #endregion
    }
}