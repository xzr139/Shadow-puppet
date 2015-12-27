using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows;
using System.Windows.Controls;

namespace gesture
{
    class Gesture_Down
    {
        public double headpos_x = 0;
        public double headpos_y = 0;
        public double headpos_z = 0;
             
        public bool Update(Skeleton skeleton, long frameTimestamp, KinectSensor KinectDevice, Grid LayoutRoot)
        {
            Joint head = skeleton.Joints[JointType.Head];
            Joint leftfoot = skeleton.Joints[JointType.FootLeft];
            Joint rightfoot = skeleton.Joints[JointType.FootRight];

            //Point headPoint = PoseStuck.GetJointPoint(KinectDevice, head, LayoutRoot.RenderSize, new Point());

            //DepthImagePoint head_point = KinectDevice.MapSkeletonPointToDepth(head.Position, KinectDevice.DepthStream.Format);
            //DepthImagePoint left_point = KinectDevice.MapSkeletonPointToDepth(leftfoot.Position, KinectDevice.DepthStream.Format);
            //DepthImagePoint right_point = KinectDevice.MapSkeletonPointToDepth(rightfoot.Position, KinectDevice.DepthStream.Format);
            //head_point.X = (int)(head_point.X * LayoutRoot.Width / KinectDevice.DepthStream.FrameWidth);
            //head_point.Y = (int)(head_point.Y * LayoutRoot.Height / KinectDevice.DepthStream.FrameHeight);

            headpos_x = head.Position.X;
            headpos_y = head.Position.Y;
            headpos_z = head.Position.Z;

            if (headpos_y<0.4)
            {
                return true;
            }
            return false;
        }
    }
}
