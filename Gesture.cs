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
    class Gesture
    {
        public string output = "Track!";
        public double posx = 0;
        public double posy = 0;
        public double posz = 0;

        MainWindow m = (MainWindow)App.Current.MainWindow;

        public Gesture_Stamp gesture_stamp = new Gesture_Stamp();

        public Gesture_nadao nadao = new Gesture_nadao();

        public string CheckGesture(Skeleton skeleton, long frameTimestamp, KinectSensor KinectDevice, Grid LayoutRoot,int num)
        {
            if (nadao.CheckSinglePoint(skeleton, KinectDevice, LayoutRoot))
            {
                if (num == 1)
                {
                    output = nadao.basicoutput;
                    m.dao.Visibility = Visibility.Visible;
                }
                else
                {
                    output = nadao.basicoutput;
                    m._dao.Visibility = Visibility.Visible;
                }

            }
            if (gesture_stamp.Update(skeleton, frameTimestamp, KinectDevice, LayoutRoot))
            {
                output = "Stamp!";
                m.dao.Visibility = Visibility.Hidden;
            }

            return output;
        }      
    }
}
