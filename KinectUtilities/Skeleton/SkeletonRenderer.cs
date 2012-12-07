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
    public class SkeletonRenderer
    {
        #region Private Variables

        private KinectSensor sensor;

        #endregion

        #region Constructors

        public SkeletonRenderer(KinectSensor sensor)
        {
            this.sensor = sensor;
        }

        #endregion

        #region Properties

        public KinectSensor Sensor
        {
            get
            {
                return sensor;
            }
            set
            {
                sensor = value;
            }
        }

        #endregion

        #region Public Methods

        public Bitmap RenderSkeleton(Bitmap bitmap, Skeleton skeleton)
        {
            Bitmap bitmapClone = (Bitmap)bitmap.Clone();
            Graphics graphics = Graphics.FromImage(bitmapClone);
            DrawSkeleton(skeleton, graphics);

            return bitmapClone;
        }
        public Bitmap RenderSkeleton(Bitmap bitmap, Skeleton skeleton, Pen pen)
        {
            Bitmap bitmapClone = (Bitmap)bitmap.Clone();
            Graphics graphics = Graphics.FromImage(bitmapClone);
            DrawSkeleton(skeleton, graphics, pen);

            return bitmapClone;
        }
        public Bitmap RenderSkeleton(ColorImageFrame image, Skeleton skeleton)
        {
            Bitmap bitmap = ImageToBitmap(image);
            Graphics graphics = Graphics.FromImage(bitmap);
            DrawSkeleton(skeleton, graphics);

            return bitmap;
        }
        public Bitmap RenderSkeleton(ColorImageFrame image, Skeleton skeleton, Pen pen)
        {
            Bitmap bitmap = ImageToBitmap(image);
            Graphics graphics = Graphics.FromImage(bitmap);
            DrawSkeleton(skeleton, graphics, pen);

            return bitmap;
        }

        #endregion

        #region Private Methods

        private void DrawSkeleton(Skeleton skeleton, Graphics graphics)
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
        private void DrawSkeleton(Skeleton skeleton, Graphics graphics, Pen pen)
        {
            // Head, shoulders, spine.
            DrawBone(JointType.Head, JointType.ShoulderCenter, skeleton, graphics, pen);
            DrawBone(JointType.ShoulderCenter, JointType.Spine, skeleton, graphics, pen);
            DrawBone(JointType.Spine, JointType.HipCenter, skeleton, graphics, pen);

            // Left leg.
            DrawBone(JointType.HipCenter, JointType.HipLeft, skeleton, graphics, pen);
            DrawBone(JointType.HipLeft, JointType.KneeLeft, skeleton, graphics, pen);
            DrawBone(JointType.KneeLeft, JointType.AnkleLeft, skeleton, graphics, pen);
            DrawBone(JointType.AnkleLeft, JointType.FootLeft, skeleton, graphics, pen);

            // Right Leg.
            DrawBone(JointType.HipCenter, JointType.HipRight, skeleton, graphics, pen);
            DrawBone(JointType.HipRight, JointType.KneeRight, skeleton, graphics, pen);
            DrawBone(JointType.KneeRight, JointType.AnkleRight, skeleton, graphics, pen);
            DrawBone(JointType.AnkleRight, JointType.FootRight, skeleton, graphics, pen);

            // Left arm.
            DrawBone(JointType.ShoulderCenter, JointType.ShoulderLeft, skeleton, graphics, pen);
            DrawBone(JointType.ShoulderLeft, JointType.ElbowLeft, skeleton, graphics, pen);
            DrawBone(JointType.ElbowLeft, JointType.WristLeft, skeleton, graphics, pen);
            DrawBone(JointType.WristLeft, JointType.HandLeft, skeleton, graphics, pen);

            // Right arm.
            DrawBone(JointType.ShoulderCenter, JointType.ShoulderRight, skeleton, graphics, pen);
            DrawBone(JointType.ShoulderRight, JointType.ElbowRight, skeleton, graphics, pen);
            DrawBone(JointType.ElbowRight, JointType.WristRight, skeleton, graphics, pen);
            DrawBone(JointType.WristRight, JointType.HandRight, skeleton, graphics, pen);
        }
        private void DrawBone(JointType jointTypeA, JointType jointTypeB, Skeleton skeleton, Graphics graphics)
        {
            DrawBone(jointTypeA, jointTypeB, skeleton, graphics, Pens.Red);
        }
        private void DrawBone(JointType jointTypeA, JointType jointTypeB, Skeleton skeleton, Graphics graphics, Pen pen)
        {
            Point pointA = GetJoint(jointTypeA, skeleton);
            Point pointB = GetJoint(jointTypeB, skeleton);
            if (pointA.X < int.MaxValue && pointB.X < int.MaxValue && pointA.Y < int.MaxValue && pointB.Y < int.MaxValue
                && pointA.X > int.MinValue && pointB.X > int.MinValue && pointA.Y > int.MinValue && pointB.Y > int.MinValue)
            {
                graphics.DrawLine(pen, pointA, pointB);
            }
        }
        private Point GetJoint(JointType jointType, Skeleton skeleton)
        {
            SkeletonPoint skeletonPoint = skeleton.Joints[jointType].Position;
            ColorImagePoint colorImagePoint = sensor.CoordinateMapper.MapSkeletonPointToColorPoint(skeletonPoint, ColorImageFormat.RgbResolution640x480Fps30);
            return new Point(colorImagePoint.X, colorImagePoint.Y);
        }
        private Bitmap ImageToBitmap(ColorImageFrame image)
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
