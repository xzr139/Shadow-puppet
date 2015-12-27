using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Microsoft.Kinect;


namespace gesture
{
    public partial class MainWindow : Window
    {
        private KinectSensor kinectDevice;
        private Skeleton[] frameSkeletons;
        private Gesture gesture;
        private DrawImage drawimage;
        private readonly Dictionary<int, int> trackedSkeletons = new Dictionary<int, int>();

        public KinectSensor KinectDevice
        {
            get { return this.kinectDevice; }
            set
            {
                if (this.kinectDevice != value)
                {
                    if (this.kinectDevice != null)
                    {
                        this.kinectDevice.Stop();
                        this.kinectDevice.SkeletonFrameReady -= KinectDevice_SkeletonFrameReady;
                        this.kinectDevice.SkeletonStream.Disable();
                        SkeletonViewerElement.KinectDevice = null;
                        this.frameSkeletons = null;
                    }

                    this.kinectDevice = value;

                    if (this.kinectDevice != null)
                    {
                        if (this.kinectDevice.Status == KinectStatus.Connected)
                        {
                            this.kinectDevice.SkeletonStream.Enable();
                            this.frameSkeletons = new Skeleton[this.kinectDevice.SkeletonStream.FrameSkeletonArrayLength];
                            this.kinectDevice.Start();

                            SkeletonViewerElement.KinectDevice = this.KinectDevice;
                            this.KinectDevice.SkeletonFrameReady += KinectDevice_SkeletonFrameReady;
                        }
                    }
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.gesture = new Gesture();
            this.drawimage = new DrawImage();

            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);

        }

        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Initializing:
                case KinectStatus.Connected:
                case KinectStatus.NotPowered:
                case KinectStatus.NotReady:
                case KinectStatus.DeviceNotGenuine:
                    this.KinectDevice = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    this.KinectDevice = null;
                    break;
                default:
                    break;
            }
        }

        private void KinectDevice_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    frame.CopySkeletonDataTo(this.frameSkeletons);
                    int num = 0; Skeleton sk1 = null, sk2 = null;

                    foreach (Skeleton skeleton in this.frameSkeletons)
                    {
                        if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            if (num == 0)
                                sk1 = skeleton;
                            else
                                sk2 = skeleton;
                            num++; 
                        }
                    }

                    if (sk1 != null)
                    {
                        if (sk2 == null)
                        {
                            if (!this.trackedSkeletons.ContainsKey(sk1.TrackingId))
                            {
                                this.trackedSkeletons.Add(sk1.TrackingId, 1);
                            }
                        }
                        else
                        {
                            if (!this.trackedSkeletons.ContainsKey(sk1.TrackingId))
                            {
                                if (!this.trackedSkeletons.ContainsKey(sk2.TrackingId))//都无
                                {
                                    this.trackedSkeletons.Add(sk1.TrackingId, 1);
                                    this.trackedSkeletons.Add(sk2.TrackingId, 2);
                                }
                                else//1无2有
                                {
                                    if (this.trackedSkeletons[sk2.TrackingId] == 1)
                                        this.trackedSkeletons.Add(sk1.TrackingId, 2);
                                    else
                                        this.trackedSkeletons.Add(sk1.TrackingId, 1);
                                }
                            }
                            else
                            {
                                if (!this.trackedSkeletons.ContainsKey(sk2.TrackingId))//1有2无
                                {
                                    if (this.trackedSkeletons[sk1.TrackingId] == 1)
                                        this.trackedSkeletons.Add(sk2.TrackingId, 2);
                                    else
                                        this.trackedSkeletons.Add(sk2.TrackingId, 1);
                                }
                            }
                        }
                    }

                    if (sk1 != null)
                    {
                        if (sk2 == null)
                        {
                            drawimage.drawing(sk1, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk1.TrackingId], 0);
                            gesture.CheckGesture(sk1, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk1.TrackingId]);
                        }
                        else
                        {
                            if (sk1.Position.X < sk2.Position.X)
                            {
                                drawimage.drawing(sk1, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk1.TrackingId], 2);
                    //            gesture.CheckGesture(sk1, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk1.TrackingId], 2);
                                drawimage.drawing(sk2, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk2.TrackingId], 1);
                                gesture.CheckGesture(sk2, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk2.TrackingId]);
                            }
                            else
                            {
                                drawimage.drawing(sk1, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk1.TrackingId], 1);
                                gesture.CheckGesture(sk1, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk1.TrackingId]);
                                drawimage.drawing(sk2, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk2.TrackingId], 2);
                     //           gesture.CheckGesture(sk2, frame.Timestamp, this.kinectDevice, this.LayoutRoot, this.trackedSkeletons[sk2.TrackingId], 2);
                            }
                        }
                    }
                }
            }
        }

        private static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;

            if (skeletons != null)
            {
                for (int i = 0; i < skeletons.Length; i++)
                {
                    if (skeletons[i].TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if (skeleton == null)
                        {
                            skeleton = skeletons[i];
                        }
                        else
                        {
                            if (skeleton.Position.Z > skeletons[i].Position.Z)
                            {
                                skeleton = skeletons[i];
                            }
                        }
                    }
                }
            }
            return skeleton;
        }

    }
}
