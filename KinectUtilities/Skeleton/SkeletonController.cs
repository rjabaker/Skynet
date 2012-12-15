﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;

using Microsoft.Kinect;

using KinectUtilities.Gestures;
using ToolBox.FileUtilities;

namespace KinectUtilities
{
    public class SkeletonController
    {
        #region Private Variables

        private KinectSensor sensor;
        private SkeletonRecognizer skeletonRecognizer;

        private List<ISkeletonCapturingFunction> capturingFunctions;

        private int numberOfSkeletonsToRecognize;

        #endregion

        #region Constructors

        public SkeletonController(KinectSensor sensor)
        {
            this.sensor = sensor;
            this.skeletonRecognizer = new SkeletonRecognizer();

            this.capturingFunctions = new List<ISkeletonCapturingFunction>(); 
            this.numberOfSkeletonsToRecognize = 1;
        }

        #endregion

        #region Properties

        public int NumberOfSkeletonsToRecognize
        {
            get
            {
                return numberOfSkeletonsToRecognize;
            }
            set
            {
                numberOfSkeletonsToRecognize = value;
            }
        }
        public List<ISkeletonCapturingFunction> SkeletonCapturingFunctions
        {
            get
            {
                return capturingFunctions;
            }
        }

        #endregion

        #region Public Methods

        public void CaptureSkeletonData(Skeleton[] skeletonData, DateTime timeStamp)
        {
            List<Skeleton> skeletons = RecognizeSkeletons(skeletonData);
            SkeletonCaptureData data = new SkeletonCaptureData(skeletons, timeStamp);

            foreach (ISkeletonCapturingFunction capturingFunction in capturingFunctions)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(capturingFunction.Execute));
                thread.Start(data);
            }
        }
        public void CaptureSkeletonData(Skeleton[] skeletonData, ColorImageFrame imageFrame, DateTime timeStamp)
        {
            List<Skeleton> skeletons = RecognizeSkeletons(skeletonData);
            SkeletonCaptureData data = new SkeletonCaptureData(skeletons, imageFrame, timeStamp);

            foreach (ISkeletonCapturingFunction capturingFunction in capturingFunctions)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(capturingFunction.Execute));
                thread.Start(data);
            }
        }

        #endregion

        #region Private Methods

        private List<Skeleton> RecognizeSkeletons(Skeleton[] skeletonData)
        {
            List<Skeleton> recognizedSkeletons = new List<Skeleton>();

            List<Skeleton> closestSkeletons = skeletonRecognizer.Recognize(skeletonData, numberOfSkeletonsToRecognize);
            foreach (Skeleton skeleton in closestSkeletons)
            {
                if (skeleton.TrackingState != SkeletonTrackingState.Tracked)
                {
                    sensor.SkeletonStream.ChooseSkeletons(skeleton.TrackingId);
                    if (skeletonRecognizer.TrackedSkeletons.ContainsKey(skeleton.TrackingId))
                    {
                        skeletonRecognizer.TrackedSkeletons[skeleton.TrackingId] = skeleton;
                    }
                    else
                    {
                        skeletonRecognizer.TrackedSkeletons.Add(skeleton.TrackingId, skeleton);
                    }
                }
                else
                {
                    skeletonRecognizer.TrackedSkeletons[skeleton.TrackingId] = skeleton;
                    recognizedSkeletons.Add(skeleton);
                }
            }

            return recognizedSkeletons;
        }

        #endregion
    }
}
