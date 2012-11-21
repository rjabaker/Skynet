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
    public class Class1
    {
        public void Test()
        {
            KinectSensor sensor = KinectSensor.KinectSensors[0];
            sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            sensor.ColorFrameReady += FrameReady;
        }

        private void FrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            // Retrive data from frame.
            ColorImageFrame imageFrame = e.OpenColorImageFrame();

            if (imageFrame != null)
            {
                Bitmap bitmap = ImageToBitmap(imageFrame);
                // Do something with the bitmap.
            }
        }

        /// <summary>
        /// Converts the ColorFrameImage into a Bitmap object.
        /// </summary>
        private Bitmap ImageToBitmap(ColorImageFrame image)
        {
            Bitmap bitmap;
            BitmapData bitmapData;
            Rectangle bitmapPortionToLock;
            IntPtr pointer;

            byte[] pixelData = new byte[image.PixelDataLength];
            image.CopyPixelDataTo(pixelData);

            bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppRgb);
            bitmapPortionToLock = new Rectangle(0, 0, image.Width, image.Height);
            bitmapData = bitmap.LockBits(bitmapPortionToLock, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            pointer = bitmapData.Scan0;
            Marshal.Copy(pixelData, 0, pointer, image.PixelDataLength);

            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        /// <summary>
        /// Stops the specified senesor.
        /// </summary>
        private void StopSensor(KinectSensor sensor)
        {
            if (sensor != null) sensor.Stop();
        }

        /// <summary>
        /// Adjusts the specified sensor by dy.
        /// </summary>
        private void AdjustSensorElevation(KinectSensor sensor, int dy)
        {
            if (sensor == null) return;

            int newElevetaionAngle = sensor.ElevationAngle + dy;

            if (newElevetaionAngle <= sensor.MaxElevationAngle && newElevetaionAngle >= sensor.MinElevationAngle)
            {
                sensor.ElevationAngle = newElevetaionAngle;
            }
        }
    }
}
