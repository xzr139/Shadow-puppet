using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows.Controls;

namespace gesture
{
    enum CirclePosition
    {
        None = 0,
        Start = 1,
        Middle = 2,
        Last = 3,
    }

    struct CircleGestureTracker
    {
        public int IterationCount;
        public GestureState State;
        public long Timestamp;
        public CirclePosition StartPosition;
        public CirclePosition CurrentPosition;

        public void Reset()
        {
            IterationCount = 0;
            State = GestureState.None;
            Timestamp = 0;
            StartPosition = CirclePosition.None;
            CurrentPosition = CirclePosition.None;
        }

        public void UpdateState(GestureState state, long timestamp)
        {
            State = state;
            Timestamp = timestamp;
        }

        public void UpdatePosition(CirclePosition position, long timestamp)
        {
            if (CurrentPosition != position)
            {
                if (position == CirclePosition.Start)
                {
                    if (State != GestureState.InProgress)
                    {
                        State = GestureState.InProgress;
                        IterationCount = 0;
                        StartPosition = position;
                    }

                }
                IterationCount++;
                CurrentPosition = position;
                Timestamp = timestamp;
            }
        }
    }
    public class Gesture_Circle
    {
        private const int CIRCLE_MOVEMENT_TIMEOUT = 5000;
        private const int REQUIRED_ITERATIONS = 3;

        private CircleGestureTracker _PlayerCircleTracker;

        private Pose[] poseLibrary_C;
        public Gesture_Circle()
        {
            PopulatePoseLibrary_C();
        }
        private void PopulatePoseLibrary_C()
        {
            this.poseLibrary_C = new Pose[3];

            this.poseLibrary_C[0] = new Pose();
            this.poseLibrary_C[0].Title = "半圆1";
            this.poseLibrary_C[0].Angles = new PoseAngle[4];
            this.poseLibrary_C[0].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary_C[0].Angles[1] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 180, 20);
            this.poseLibrary_C[0].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight,0, 10);
            this.poseLibrary_C[0].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 135, 20);

            this.poseLibrary_C[1] = new Pose();
            this.poseLibrary_C[1].Title = "半圆2";
            this.poseLibrary_C[1].Angles = new PoseAngle[4];
            this.poseLibrary_C[1].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary_C[1].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 180, 20);
            this.poseLibrary_C[1].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.poseLibrary_C[1].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 60, 20);

            this.poseLibrary_C[2] = new Pose();
            this.poseLibrary_C[2].Title = "半圆3";
            this.poseLibrary_C[2].Angles = new PoseAngle[4];
            this.poseLibrary_C[2].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary_C[2].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 180, 20);
            this.poseLibrary_C[2].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0,20);
            this.poseLibrary_C[2].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 0, 20);
        }

        public bool Update(Skeleton skeleton, long frameTimestamp, KinectSensor KinectDevice, Grid LayoutRoot)
        {
            if (skeleton.TrackingState != SkeletonTrackingState.NotTracked)
            {
                if (TrackCircle(skeleton, ref this._PlayerCircleTracker, frameTimestamp, KinectDevice, LayoutRoot))
                    return true;
            }
            else
            {
                this._PlayerCircleTracker.Reset();
            }
            return false;
        }

        private bool TrackCircle(Skeleton skeleton, ref CircleGestureTracker tracker, long timestamp, KinectSensor KinectDevice, Grid LayoutRoot)
        {
            Joint hand = skeleton.Joints[JointType.HandRight];
            Joint elbow = skeleton.Joints[JointType.ElbowRight];

            if (hand.TrackingState != JointTrackingState.NotTracked && elbow.TrackingState != JointTrackingState.NotTracked)
            {

                if (tracker.State == GestureState.InProgress && tracker.Timestamp + CIRCLE_MOVEMENT_TIMEOUT < timestamp)
                {
                    tracker.UpdateState(GestureState.Failure, timestamp);
                }

                if (PoseStuck.IsPose(skeleton,this.poseLibrary_C[0], KinectDevice, LayoutRoot))
                {
                    tracker.UpdatePosition(CirclePosition.Start, timestamp);
                }
                else if (PoseStuck.IsPose(skeleton,this.poseLibrary_C[1], KinectDevice, LayoutRoot))
                {
                    tracker.UpdatePosition(CirclePosition.Middle, timestamp);
                }
                else if (PoseStuck.IsPose(skeleton, this.poseLibrary_C[2], KinectDevice, LayoutRoot))
                {
                    tracker.UpdatePosition(CirclePosition.Last, timestamp);
                }


                if (tracker.State != GestureState.Success && tracker.IterationCount >= REQUIRED_ITERATIONS && tracker.CurrentPosition == CirclePosition.Last)
                {
                    tracker.UpdateState(GestureState.Success, timestamp);
                    return true;
                }
            }
            else
            {
                tracker.Reset();

            }
            return false;
        }
    }
}
