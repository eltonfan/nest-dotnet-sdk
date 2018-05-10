using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Elton.Nest
{
    public static partial class Extensions
    {
        public static string GetValueString(this Enum enumVal)
        {
            var memInfo = enumVal.GetType().GetMember(enumVal.ToString())?.FirstOrDefault();
            return memInfo?.GetCustomAttribute<EnumMemberAttribute>()?.Value;
        }

        public static IObservable<NestGlobalEventArgs> WhenGlobalUpdated(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestGlobalEventArgs>(
                handler => nest.Notifier.GlobalUpdated += handler,
                handler => nest.Notifier.GlobalUpdated -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestDeviceEventArgs> WhenDeviceUpdated(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestDeviceEventArgs>(
                handler => nest.Notifier.DeviceUpdated += handler,
                handler => nest.Notifier.DeviceUpdated -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestStructureEventArgs> WhenStructureUpdated(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestStructureEventArgs>(
                handler => nest.Notifier.StructureUpdated += handler,
                handler => nest.Notifier.StructureUpdated -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestThermostatEventArgs> WhenThermostatUpdated(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestThermostatEventArgs>(
                handler => nest.Notifier.ThermostatUpdated += handler,
                handler => nest.Notifier.ThermostatUpdated -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestCameraEventArgs> WhenCameraUpdated(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestCameraEventArgs>(
                handler => nest.Notifier.CameraUpdated += handler,
                handler => nest.Notifier.CameraUpdated -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestSmokeCOAlarmEventArgs> WhenSmokeCOAlarmUpdated(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestSmokeCOAlarmEventArgs>(
                handler => nest.Notifier.SmokeCOAlarmUpdated += handler,
                handler => nest.Notifier.SmokeCOAlarmUpdated -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestMetadataEventArgs> WhenMetadataUpdated(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestMetadataEventArgs>(
                handler => nest.Notifier.MetadataUpdated += handler,
                handler => nest.Notifier.MetadataUpdated -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestAuthFailureEventArgs> WhenAuthFailure(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestAuthFailureEventArgs>(
                handler => nest.Notifier.AuthFailure += handler,
                handler => nest.Notifier.AuthFailure -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestErrorEventArgs> WhenError(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestErrorEventArgs>(
                handler => nest.Notifier.Error += handler,
                handler => nest.Notifier.Error -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<NestAuthRevokedEventArgs> WhenAuthRevoked(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<NestAuthRevokedEventArgs>(
                handler => nest.Notifier.AuthRevoked += handler,
                handler => nest.Notifier.AuthRevoked -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<ValueAddedEventArgs> WhenValueAdded(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<ValueAddedEventArgs>(
                handler => nest.Notifier.ValueAdded += handler,
                handler => nest.Notifier.ValueAdded -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<ValueChangedEventArgs> WhenValueChanged(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<ValueChangedEventArgs>(
                handler => nest.Notifier.ValueChanged += handler,
                handler => nest.Notifier.ValueChanged -= handler)
                .Select(p => p.EventArgs);
        }

        public static IObservable<ValueRemovedEventArgs> WhenValueRemoved(this NestClient nest)
        {
            var progress = new Progress<int>();
            return Observable.FromEventPattern<ValueRemovedEventArgs>(
                handler => nest.Notifier.ValueRemoved += handler,
                handler => nest.Notifier.ValueRemoved -= handler)
                .Select(p => p.EventArgs);
        }
    }
}