using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows;
using System.Windows.Controls;


namespace gesture
{
    class Gesture_Wryneck
    {
        public double headpos_x = 0;
        public double headpos_y = 0;
        public double headpos_z = 0;
        public string poseoutput;

        public bool Update(Skeleton skeleton, long frameTimestamp, KinectSensor KinectDevice, Grid LayoutRoot)
        {
            Joint head = skeleton.Joints[JointType.Head];
            Joint neck = skeleton.Joints[JointType.ShoulderCenter];

            headpos_x = head.Position.X;
            headpos_y = head.Position.Y;
            headpos_z = head.Position.Z;

            if (headpos_x < neck.Position.X-0.1)
            {
                poseoutput = "头向左偏";
                return true;
            }
            else if (headpos_x > neck.Position.X + 0.1)
            {
                poseoutput = "头向右偏";
                return true;
            }
            return false;
        }
    }
}
