using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows.Controls;

namespace gesture
{
    class Gesture_nadao
    {
        private Pose[] poseLibrary_Basicpose;
        public string basicoutput;

        public Gesture_nadao()
        {
            PopulatePoseLibrary_nadao();
        }
        private void PopulatePoseLibrary_nadao()
        {
            this.poseLibrary_Basicpose = new Pose[1];

            this.poseLibrary_Basicpose[0] = new Pose();
            this.poseLibrary_Basicpose[0].Title = "拿刀";
            this.poseLibrary_Basicpose[0].Angles = new PoseAngle[4];
            this.poseLibrary_Basicpose[0].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary_Basicpose[0].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 90, 20);
            this.poseLibrary_Basicpose[0].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.poseLibrary_Basicpose[0].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 90, 20);

        }

        public bool CheckSinglePoint(Skeleton skeleton, KinectSensor KinectDevice, Grid LayoutRoot)
        {
            if (PoseStuck.IsPose(skeleton, this.poseLibrary_Basicpose[0], KinectDevice, LayoutRoot))
            {
                basicoutput = this.poseLibrary_Basicpose[0].Title;
                return true;
            }               
        
              return false;
         }
    }
}
