using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows.Controls;


namespace gesture
{
    enum StampPosition
    {
        None = 0,
        Start = 1,
        Last = 2,
    }

    struct StampGestureTracker
    {
        public int IterationCount;
        public GestureState State;
        public long Timestamp;
        public StampPosition StartPosition;
        public StampPosition CurrentPosition;

        public void Reset()
        {
            IterationCount = 0;
            State = GestureState.None;
            Timestamp = 0;
            StartPosition = StampPosition.None;
            CurrentPosition = StampPosition.None;
        }

        public void UpdateState(GestureState state, long timestamp)
        {
            State = state;
            Timestamp = timestamp;
        }

        public void UpdatePosition(StampPosition position, long timestamp)
        {
            if (CurrentPosition != position)
            {
                if (position == StampPosition.Start)
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



    public class Gesture_Stamp
    {
        private const int Stamp_MOVEMENT_TIMEOUT = 5000;
        private const int REQUIRED_ITERATIONS = 2;

        private StampGestureTracker _PlayerStampTracker;

        private Pose[] poseLibrary_S;
        public Gesture_Stamp()
        {
            PopulatePoseLibrary_S();
        }
        private void PopulatePoseLibrary_S()
        {
            this.poseLibrary_S = new Pose[2];

            this.poseLibrary_S[0] = new Pose();
            this.poseLibrary_S[0].Title = "跺脚1";
            this.poseLibrary_S[0].Angles = new PoseAngle[4];
            this.poseLibrary_S[0].Angles[0] = new PoseAngle(JointType.HipLeft, JointType.KneeLeft, 270, 20);
            this.poseLibrary_S[0].Angles[1] = new PoseAngle(JointType.KneeLeft, JointType.AnkleLeft, 270, 20);
            this.poseLibrary_S[0].Angles[2] = new PoseAngle(JointType.HipRight, JointType.KneeRight, 350, 20);
            this.poseLibrary_S[0].Angles[3] = new PoseAngle(JointType.KneeRight, JointType.AnkleRight, 250, 20);
      
            this.poseLibrary_S[1] = new Pose();
            this.poseLibrary_S[1].Title = "跺脚2";
            this.poseLibrary_S[1].Angles = new PoseAngle[4];
            this.poseLibrary_S[1].Angles[0] = new PoseAngle(JointType.HipLeft, JointType.KneeLeft, 270, 20);
            this.poseLibrary_S[1].Angles[1] = new PoseAngle(JointType.KneeLeft, JointType.AnkleLeft, 270, 20);
            this.poseLibrary_S[1].Angles[2] = new PoseAngle(JointType.HipRight, JointType.KneeRight, 270, 20);
            this.poseLibrary_S[1].Angles[3] = new PoseAngle(JointType.KneeRight, JointType.AnkleRight, 270, 20);

        }

        public bool Update(Skeleton skeleton, long frameTimestamp, KinectSensor KinectDevice, Grid LayoutRoot)
        {
            if (skeleton.TrackingState != SkeletonTrackingState.NotTracked)
            {
                if (TrackStamp(skeleton, ref this._PlayerStampTracker, frameTimestamp, KinectDevice, LayoutRoot))
                    return true;
            }
            else
            {
                this._PlayerStampTracker.Reset();
            }
            return false;
        }

        private bool TrackStamp(Skeleton skeleton, ref StampGestureTracker tracker, long timestamp, KinectSensor KinectDevice, Grid LayoutRoot)
        {
            Joint hand = skeleton.Joints[JointType.HandRight];
            Joint elbow = skeleton.Joints[JointType.ElbowRight];

            if (hand.TrackingState != JointTrackingState.NotTracked && elbow.TrackingState != JointTrackingState.NotTracked)
            {

                if (tracker.State == GestureState.InProgress && tracker.Timestamp + Stamp_MOVEMENT_TIMEOUT < timestamp)
                {
                    tracker.UpdateState(GestureState.Failure, timestamp);
                }

                if (PoseStuck.IsPose(skeleton, this.poseLibrary_S[0], KinectDevice, LayoutRoot))
                {
                    tracker.UpdatePosition(StampPosition.Start, timestamp);
                }
                else if (PoseStuck.IsPose(skeleton, this.poseLibrary_S[1], KinectDevice, LayoutRoot))
                {
                    tracker.UpdatePosition(StampPosition.Last, timestamp);
                }

                if (tracker.State != GestureState.Success && tracker.IterationCount >= REQUIRED_ITERATIONS && tracker.CurrentPosition == StampPosition.Last)
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
