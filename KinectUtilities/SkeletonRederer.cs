using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using Microsoft.Kinect;

namespace KinectUtilities
{
    public static class SkeletonRederer
    {
        #region Delegates

        public delegate void SkeletonRenderedEventHandler(Bitmap image);

        #endregion

        #region Public Static Variables

        public static KinectSensor Sensor;
        public static Bitmap defaultBitamp = CreateDefaultBitmap(new Size(100, 100), Color.Black);

        #endregion

        #region Public Static Methods

        public static Bitmap CreateDefaultBitmap(Size size, Color color)
        {
            defaultBitamp = new Bitmap(size.Width, size.Height);

            for (int row = 0; row < size.Width; row++)
            {
                for (int column = 0; column < size.Height; column++)
                {
                    defaultBitamp.SetPixel(row, column, color);
                }
            }

            return defaultBitamp;
        }

        public static Bitmap RenderSkeleton(ColorImageFrame image, Skeleton skeleton)
        {
            Bitmap bitmap = ImageToBitmap(image);
            Graphics graphics = Graphics.FromImage(bitmap);
            DrawSkeleton(skeleton, graphics);

            return bitmap;
        }

        public static Bitmap RenderSkeleton(Bitmap bitmap, Skeleton skeleton)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            DrawSkeleton(skeleton, graphics);

            return bitmap;
        }

        public static Bitmap RenderSkeleton(Skeleton skeleton)
        {
            Bitmap bitmap = (Bitmap)defaultBitamp.Clone();
            Graphics graphics = Graphics.FromImage(bitmap);
            DrawSkeleton(skeleton, graphics);

            return bitmap;
        }

        public static void DrawSkeleton(Skeleton skeleton, Graphics graphics)
        {
            // Head, shoulders, spine.
            DrawBone(JointType.Head, JointType.ShoulderCenter, skeleton, graphics);
            DrawBone(JointType.ShoulderCenter, JointType.Spine, skeleton, graphics);
            DrawBone(JointType.Spine, JointType.HipCenter, skeleton, graphics);

            // Left leg.
            DrawBone(JointType.HipCenter, JointType.HipLeft, skeleton, graphics);
            DrawBone(JointType.HipLeft, JointType.KneeLeft, skeleton, graphics);
            DrawBone(JointType.KneeLeft, JointType.AnkleLeft, skeleton, graphics);
            DrawBone(JointType.AnkleLeft, JointType.FootLeft, skeleton, graphics);

            // Right Leg.
            DrawBone(JointType.HipCenter, JointType.HipRight, skeleton, graphics);
            DrawBone(JointType.HipRight, JointType.KneeRight, skeleton, graphics);
            DrawBone(JointType.KneeRight, JointType.AnkleRight, skeleton, graphics);
            DrawBone(JointType.AnkleRight, JointType.FootRight, skeleton, graphics);

            // Left arm.
            DrawBone(JointType.ShoulderCenter, JointType.ShoulderLeft, skeleton, graphics);
            DrawBone(JointType.ShoulderLeft, JointType.ElbowLeft, skeleton, graphics);
            DrawBone(JointType.ElbowLeft, JointType.WristLeft, skeleton, graphics);
            DrawBone(JointType.WristLeft, JointType.HandLeft, skeleton, graphics);

            // Right arm.
            DrawBone(JointType.ShoulderCenter, JointType.ShoulderRight, skeleton, graphics);
            DrawBone(JointType.ShoulderRight, JointType.ElbowRight, skeleton, graphics);
            DrawBone(JointType.ElbowRight, JointType.WristRight, skeleton, graphics);
            DrawBone(JointType.WristRight, JointType.HandRight, skeleton, graphics);
        }

        public static void DrawBone(JointType jointTypeA, JointType jointTypeB, Skeleton skeleton, Graphics graphics)
        {
            DrawBone(jointTypeA, jointTypeB, skeleton, Pens.Red, graphics);
        }

        public static void DrawBone(JointType jointTypeA, JointType jointTypeB, Skeleton skeleton, Pen pen, Graphics graphics)
        {
            Point pointA = GetJoint(jointTypeA, skeleton);
            Point pointB = GetJoint(jointTypeB, skeleton);
            if (pointA.X < int.MaxValue && pointB.X < int.MaxValue && pointA.Y < int.MaxValue && pointB.Y < int.MaxValue
                && pointA.X > int.MinValue && pointB.X > int.MinValue && pointA.Y > int.MinValue && pointB.Y > int.MinValue)
            {
                graphics.DrawLine(pen, pointA, pointB);
            }
        }

        public static Point GetJoint(JointType jointType, Skeleton skeleton)
        {
            SkeletonPoint skeletonPoint = skeleton.Joints[jointType].Position;
            ColorImagePoint colorImagePoint = Sensor.CoordinateMapper.MapSkeletonPointToColorPoint(skeletonPoint, ColorImageFormat.RgbResolution640x480Fps30);
            return new Point(colorImagePoint.X, colorImagePoint.Y);
        }

        public static Bitmap ImageToBitmap(ColorImageFrame image)
        {
            byte[] pixeldata = new byte[image.PixelDataLength];
            image.CopyPixelDataTo(pixeldata);
            Bitmap bmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppRgb);
            BitmapData bmapdata = bmap.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, bmap.PixelFormat);
            IntPtr ptr = bmapdata.Scan0;
            Marshal.Copy(pixeldata, 0, ptr, image.PixelDataLength);
            bmap.UnlockBits(bmapdata);

            return bmap;
        }

        #endregion
    }
}
