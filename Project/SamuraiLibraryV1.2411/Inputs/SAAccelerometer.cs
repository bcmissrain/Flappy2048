using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Devices.Sensors;

namespace Samurai
{
    public static class SAAccelerometer
    {
        static Accelerometer accelerometer;
        public static float X { get; set; }     //X左右 
        public static float Y { get; set; }     //Y顶底
        public static float Z { get; set; }     //Z正反

        /// <summary>
        /// 开启加速计
        /// </summary>
        public static void StartAccelerometer()
        {
            accelerometer = new Accelerometer();
            accelerometer.CurrentValueChanged += accelerometer_CurrentValueChanged;
            accelerometer.Start();
        }

        /// <summary>
        /// 重启，对于Stop而言
        /// </summary>
        public static void RestartAccelerometer()
        {
            if (accelerometer != null)
            {
                accelerometer.Start();
            }
        }

        /// <summary>
        /// 暂停，可以重启
        /// </summary>
        public static void StopAccelerometer()
        {
            accelerometer.Stop();
            X = Y = Z = 0;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public static void ReleaseAccelerometer()
        {
            if (accelerometer != null)
            {
                accelerometer.Stop();
                accelerometer = null;
            }
            X = Y = Z = 0;
        }

        static void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => NewReading(e));
        }

        private static void NewReading(SensorReadingEventArgs<AccelerometerReading> e)
        {
            X = e.SensorReading.Acceleration.X;
            Y = e.SensorReading.Acceleration.Y;
            Z = e.SensorReading.Acceleration.Z;
        }
    }
}
