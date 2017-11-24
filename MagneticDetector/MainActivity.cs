using Android.App;
using Android.Widget;
using Android.OS;

using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;

namespace MagneticDetector
{
    [Activity(Label = "MagneticDetector", MainLauncher = true)]
    public class MainActivity : Activity
    {
        double maxX = 0;
        double maxY = 0;
        double maxZ = 0;

        double minX = 0;
        double minY = 0;
        double minZ = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var progressBarX = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            var progressBarY = FindViewById<ProgressBar>(Resource.Id.progressBar2);
            var progressBarZ = FindViewById<ProgressBar>(Resource.Id.progressBar3);

            CrossDeviceMotion.Current.Start(MotionSensorType.Magnetometer);

            CrossDeviceMotion.Current.SensorValueChanged += (sender, e) =>
            {
                switch (e.SensorType)
                {
                    case MotionSensorType.Magnetometer:
                        var value = (MotionVector)e.Value;
                        LimitsTestFunction(value);
                        progressBarX.Progress = (int)value.X;
                        progressBarY.Progress = (int)value.Y;
                        progressBarZ.Progress = (int)value.Z;
                        break;
                    default:
                        break;
                }
            };
        }

        void LimitsTestFunction (MotionVector a)
        {
            var x = a.X;
            var y = a.Y;
            var z = a.Z;

            if(x > maxX)
            {
                maxX = x;
                Console.WriteLine("x Max: " + maxX);
            }
            if (y > maxY)
            {
                maxY = x;
                Console.WriteLine("x Max: " + maxX);
            }
            if (z > maxZ)
            {
                maxZ = x;
                Console.WriteLine("x Max: " + maxX);
            }

            if(x < minX)
            {
                minX = x;
                Console.WriteLine("x Min: " + minX);
            }
            if (y < minY)
            {
                minY = y;
                Console.WriteLine("y Min: " + minY);
            }
            if (z < minZ)
            {
                minZ = z;
                Console.WriteLine("z Min: " + minZ);
            }
        }
    }
}

