using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using System.Windows.Media;
using System.Windows.Controls;

namespace gesture
{
    public class DrawImage
    {
        MainWindow m = (MainWindow)App.Current.MainWindow;
        ObservableCollection<BitmapImage> bmlist;

        public DrawImage()
        {
            InitList();
        }

        public void InitList()
        {
            bmlist = new ObservableCollection<BitmapImage>();
            string applicationPath = AppDomain.CurrentDomain.BaseDirectory;

            BitmapImage bmImg0 = new BitmapImage(new Uri(applicationPath + "images\\头.png"));
            bmlist.Add(bmImg0);
            BitmapImage bmImg1 = new BitmapImage(new Uri(applicationPath + "images\\上身.png"));
            bmlist.Add(bmImg1);
            BitmapImage bmImg2 = new BitmapImage(new Uri(applicationPath + "images\\大臂.png"));
            bmlist.Add(bmImg2);
            BitmapImage bmImg3 = new BitmapImage(new Uri(applicationPath + "images\\小臂.png"));
            bmlist.Add(bmImg3);
            BitmapImage bmImg4 = new BitmapImage(new Uri(applicationPath + "images\\大腿.png"));
            bmlist.Add(bmImg4);
            BitmapImage bmImg5 = new BitmapImage(new Uri(applicationPath + "images\\小腿.png"));
            bmlist.Add(bmImg5);
        }

        public void drawing(Skeleton skeleton, long frameTimestamp, KinectSensor KinectDevice, Grid LayoutRoot,int num,int dir)
        {
            Joint head = skeleton.Joints[JointType.Head];
            Point headposition = PoseStuck.GetJointPoint(KinectDevice, head, LayoutRoot.RenderSize, new Point());
            Joint neck = skeleton.Joints[JointType.ShoulderCenter];
            Point neckposition = PoseStuck.GetJointPoint(KinectDevice, neck, LayoutRoot.RenderSize, new Point());
            Joint leftelbow = skeleton.Joints[JointType.ElbowLeft];
            Point leftelbowposition = PoseStuck.GetJointPoint(KinectDevice, leftelbow, LayoutRoot.RenderSize, new Point());
            Joint rightelbow = skeleton.Joints[JointType.ElbowRight];
            Point rightelbowposition = PoseStuck.GetJointPoint(KinectDevice, rightelbow, LayoutRoot.RenderSize, new Point());
            Joint leftwrist = skeleton.Joints[JointType.WristLeft];
            Point leftwristposition = PoseStuck.GetJointPoint(KinectDevice, leftwrist, LayoutRoot.RenderSize, new Point());
            Joint rightwrist = skeleton.Joints[JointType.WristRight];
            Point rightwristposition = PoseStuck.GetJointPoint(KinectDevice, rightwrist, LayoutRoot.RenderSize, new Point());
            Joint hip = skeleton.Joints[JointType.HipCenter];
            Point hipposition = PoseStuck.GetJointPoint(KinectDevice, hip, LayoutRoot.RenderSize, new Point());
            Joint leftknee = skeleton.Joints[JointType.KneeLeft];
            Point leftkneeposition = PoseStuck.GetJointPoint(KinectDevice, leftknee, LayoutRoot.RenderSize, new Point());
            Joint rightknee = skeleton.Joints[JointType.KneeRight];
            Point rightkneeposition = PoseStuck.GetJointPoint(KinectDevice, rightknee, LayoutRoot.RenderSize, new Point());
            Joint leftankle = skeleton.Joints[JointType.AnkleLeft];
            Point leftankleposition = PoseStuck.GetJointPoint(KinectDevice, leftankle, LayoutRoot.RenderSize, new Point());
            Joint rightankle = skeleton.Joints[JointType.AnkleRight];
            Point rightankleposition = PoseStuck.GetJointPoint(KinectDevice, rightankle, LayoutRoot.RenderSize, new Point());
            Joint leftfoot = skeleton.Joints[JointType.FootLeft];
            Point leftfootposition = PoseStuck.GetJointPoint(KinectDevice, leftfoot, LayoutRoot.RenderSize, new Point());
            Joint rightfoot = skeleton.Joints[JointType.FootRight];
            Point rightfootposition = PoseStuck.GetJointPoint(KinectDevice, rightfoot, LayoutRoot.RenderSize, new Point());
            Joint spine = skeleton.Joints[JointType.Spine];
            Point spineposition = PoseStuck.GetJointPoint(KinectDevice, spine, LayoutRoot.RenderSize, new Point());

            if (num == 1)
            {
                this.m.tou.Visibility = Visibility.Visible;
                this.m.shangsheng.Visibility = Visibility.Visible;
                this.m.dabi.Visibility = Visibility.Visible;
                this.m.xiaobi.Visibility = Visibility.Visible;
                this.m.datui.Visibility = Visibility.Visible;
                this.m.xiaotui.Visibility = Visibility.Visible;
                this.m.dabi2.Visibility = Visibility.Visible;
                this.m.xiaobi2.Visibility = Visibility.Visible;
                this.m.datui2.Visibility = Visibility.Visible;
                this.m.xiaotui2.Visibility = Visibility.Visible;

                double z = skeleton.Position.Z;

                bool pos = false;
                if (dir == 1)
                    pos = true;

                drawingjoint(headposition, neckposition, ref m.tou, 0.9, false, m.tou.Width / 2, m.tou.Height, z,pos);
                drawingjoint(neckposition, hipposition, ref m.shangsheng, 1.3, true, m.shangsheng.Width / 2, 0, z,pos);
                drawingjoint(neckposition, leftelbowposition, ref m.dabi, 1.5, false, m.dabi.Width / 2, m.dabi.Height, z, pos);
                drawingjoint(neckposition, rightelbowposition, ref m.dabi2, 1.5, false, m.dabi2.Width / 2, m.dabi2.Height, z, pos);
                drawingjoint(leftelbowposition, leftwristposition, ref m.xiaobi, 1.4, true, m.xiaobi.Width / 2, m.xiaobi.Height / 10, z, pos);
                drawingjoint(rightelbowposition, rightwristposition, ref m.xiaobi2, 1.4, true, m.xiaobi2.Width / 2, m.xiaobi2.Height / 10, z, pos);
                drawingjoint(hipposition, leftkneeposition, ref m.datui, 1.3, true, m.datui.Width / 2 + 10, m.datui.Height / 10, z, pos);
                drawingjoint(hipposition, rightkneeposition, ref m.datui2, 1.3, true, m.datui2.Width / 2 - 10, m.datui2.Height / 10, z, pos);
                drawingjoint(leftkneeposition, leftankleposition, ref m.xiaotui, 2, true, m.xiaotui.Width / 2, m.xiaotui.Height / 3, z, pos);
                drawingjoint(rightkneeposition, rightankleposition, ref m.xiaotui2, 2, true, m.xiaotui2.Width / 2, m.xiaotui2.Height / 3, z, pos);

                drawingjoint(leftwristposition, rightwristposition, ref m.dao, 0.8, true, m.dao.Width / 2, m.dao.Height / 8, z, pos);    //拿刀
            }
            else
            {
                this.m._tou.Visibility = Visibility.Visible;
                this.m._shangsheng.Visibility = Visibility.Visible;
                this.m._dabi.Visibility = Visibility.Visible;
                this.m._xiaobi.Visibility = Visibility.Visible;
                this.m._datui.Visibility = Visibility.Visible;
                this.m._xiaotui.Visibility = Visibility.Visible;
                this.m._dabi2.Visibility = Visibility.Visible;
                this.m._xiaobi2.Visibility = Visibility.Visible;
                this.m._datui2.Visibility = Visibility.Visible;
                this.m._xiaotui2.Visibility = Visibility.Visible;

                double z = skeleton.Position.Z;

                bool pos = false;
                if (dir == 2)
                    pos = true;

                drawingjoint(headposition, neckposition, ref m._tou, 0.9, false, m._tou.Width / 2, m._tou.Height, z, pos);
                drawingjoint(neckposition, hipposition, ref m._shangsheng, 1.3, true, m._shangsheng.Width / 2, 0, z,pos);
                drawingjoint(neckposition, leftelbowposition, ref m._dabi, 1.5, false, m._dabi.Width / 2, m._dabi.Height, z, pos);
                drawingjoint(neckposition, rightelbowposition, ref m._dabi2, 1.5, false, m._dabi2.Width / 2, m._dabi2.Height, z, pos);
                drawingjoint(leftelbowposition, leftwristposition, ref m._xiaobi, 1.4, true, m._xiaobi.Width / 2, m._xiaobi.Height / 10, z, pos);
                drawingjoint(rightelbowposition, rightwristposition, ref m._xiaobi2, 1.4, true, m._xiaobi2.Width / 2, m._xiaobi2.Height / 10, z, pos);
                drawingjoint(hipposition, leftkneeposition, ref m._datui, 1.3, true, m._datui.Width / 2 + 10, m._datui.Height / 10, z, pos);
                drawingjoint(hipposition, rightkneeposition, ref m._datui2, 1.3, true, m._datui2.Width / 2 - 10, m._datui2.Height / 10, z, pos);
                drawingjoint(leftkneeposition, leftankleposition, ref m._xiaotui, 2, true, m._xiaotui.Width / 2, m._xiaotui.Height / 3, z, pos);
                drawingjoint(rightkneeposition, rightankleposition, ref m._xiaotui2, 2, true, m._xiaotui2.Width / 2, m._xiaotui2.Height / 3, z, pos);

                drawingjoint(leftwristposition, rightwristposition, ref m._dao, 0.9, true, m._dao.Width / 2, m._dao.Height / 8, z, pos);    //拿刀
            }

        }

        public void drawingjoint(Point one, Point two, ref Image img, double scale, bool shangxia, double x,double y,double z,bool pos)
        {

            double temp = System.Math.Atan2(one.Y - two.Y, one.X - two.X);
            temp *= 180 / 3.14159; temp += 90;//用来确定旋转度数

            TransformGroup transformGroup = new TransformGroup();

            ScaleTransform scaletrans;

            if(pos)
                scaletrans = new ScaleTransform(-4/z*scale,4/z*scale, img.Width / 2, img.Height);
            else
                scaletrans = new ScaleTransform(4 / z * scale, 4 / z * scale, img.Width / 2, img.Height);

            RotateTransform rotatetrans = new RotateTransform(temp, img.Width / 2, img.Height);

            transformGroup.Children.Add(scaletrans);
            transformGroup.Children.Add(rotatetrans);

            Point transpoint1 = new Point(x, y);
            Point transpoint2 = new Point();
            transformGroup.TryTransform(transpoint1, out transpoint2);


            if (shangxia)
            {
                TranslateTransform translatetrans = new TranslateTransform(one.X - transpoint2.X, one.Y - transpoint2.Y);
                transformGroup.Children.Add(translatetrans);
            }
            else
            {
                TranslateTransform translatetrans = new TranslateTransform(two.X - img.Width / 2, two.Y - img.Height);
                transformGroup.Children.Add(translatetrans);
            }

            img.RenderTransform = transformGroup;

        }

    }
}
