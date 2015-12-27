using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows.Controls;

namespace gesture
{
    class Gesture_Basicpose
    {
        private Pose[] poseLibrary_Basicpose;
        public string basicoutput;

        public Gesture_Basicpose()
        {
            PopulatePoseLibrary_Basicpose();
        }
        private void PopulatePoseLibrary_Basicpose()
        {
            this.poseLibrary_Basicpose = new Pose[7];

            this.poseLibrary_Basicpose[0] = new Pose();
            this.poseLibrary_Basicpose[0].Title = "举起手来";
            this.poseLibrary_Basicpose[0].Angles = new PoseAngle[4];
            this.poseLibrary_Basicpose[0].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary_Basicpose[0].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 90, 20);
            this.poseLibrary_Basicpose[0].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.poseLibrary_Basicpose[0].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 90, 20);

            this.poseLibrary_Basicpose[1] = new Pose();
            this.poseLibrary_Basicpose[1].Title = "把手放下来";
            this.poseLibrary_Basicpose[1].Angles = new PoseAngle[4];
            this.poseLibrary_Basicpose[1].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary_Basicpose[1].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 270, 20);
            this.poseLibrary_Basicpose[1].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.poseLibrary_Basicpose[1].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 270, 20);

            this.poseLibrary_Basicpose[2] = new Pose();
            this.poseLibrary_Basicpose[2].Title = "举起左手";
            this.poseLibrary_Basicpose[2].Angles = new PoseAngle[4];
            this.poseLibrary_Basicpose[2].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            this.poseLibrary_Basicpose[2].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 90, 20);
            this.poseLibrary_Basicpose[2].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 280, 20);
            this.poseLibrary_Basicpose[2].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 270, 20);

            this.poseLibrary_Basicpose[3] = new Pose();
            this.poseLibrary_Basicpose[3].Title = "举起右手";
            this.poseLibrary_Basicpose[3].Angles = new PoseAngle[4];
            this.poseLibrary_Basicpose[3].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 270, 20);
            this.poseLibrary_Basicpose[3].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 270, 20);
            this.poseLibrary_Basicpose[3].Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            this.poseLibrary_Basicpose[3].Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 90, 20);

            this.poseLibrary_Basicpose[4] = new Pose();
            this.poseLibrary_Basicpose[4].Title = "上";
            this.poseLibrary_Basicpose[4].Angles = new PoseAngle[2];
            //this.poseLibrary_Basicpose[3].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 270, 20);
            //this.poseLibrary_Basicpose[3].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 270, 20);
            this.poseLibrary_Basicpose[4].Angles[0] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 45, 10);
            this.poseLibrary_Basicpose[4].Angles[1] = new PoseAngle(JointType.ElbowRight, JointType.WristRight,45, 10);

            this.poseLibrary_Basicpose[5] = new Pose();
            this.poseLibrary_Basicpose[5].Title = "中";
            this.poseLibrary_Basicpose[5].Angles = new PoseAngle[2];
            //this.poseLibrary_Basicpose[3].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 270, 20);
            //this.poseLibrary_Basicpose[3].Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 270, 20);
            this.poseLibrary_Basicpose[5].Angles[0] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 10);
            this.poseLibrary_Basicpose[5].Angles[1] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 0, 10);

            this.poseLibrary_Basicpose[6] = new Pose();
            this.poseLibrary_Basicpose[6].Title = "下";
            this.poseLibrary_Basicpose[6].Angles = new PoseAngle[2];
            //this.poseLibrary_Basicpose[3].Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 270, 20);
            //this.poseLibrary_Basicpose[3].Angles[1] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 270, 20);
            this.poseLibrary_Basicpose[6].Angles[0] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 315, 10);
            this.poseLibrary_Basicpose[6].Angles[1] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 315, 10);
        }

        public bool CheckSinglePoint(Skeleton skeleton, KinectSensor KinectDevice, Grid LayoutRoot)
        {
             for (int i = 0; i < this.poseLibrary_Basicpose.Length; i++)
             {
                   if (PoseStuck.IsPose(skeleton, this.poseLibrary_Basicpose[i], KinectDevice, LayoutRoot))
                   {
                            basicoutput = this.poseLibrary_Basicpose[i].Title;
                            return true;
                    }                 
              } 
              return false;
         }
    }
}
