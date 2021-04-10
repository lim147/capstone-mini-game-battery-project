using System;
using System.Diagnostics;
using Unity.PerformanceTesting.Exceptions;
using Unity.PerformanceTesting.Measurements;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.PerformanceTesting
{
    public static class Measure
    {
        public static void Custom(SampleGroup sampleGroup, double value)
        {
            VerifyValue(sampleGroup.Name, value);

            var activeSampleGroup = PerformanceTest.GetSampleGroup(sampleGroup.Name);
            if (activeSampleGroup == null)
            {
                PerformanceTest.AddSampleGroup(sampleGroup);
                activeSampleGroup = sampleGroup;
            }

            activeSampleGroup.Samples.Add(value);
        }

        public static void Custom(string name, double value)
        {
            VerifyValue(name, value);

            var activeSampleGroup = PerformanceTest.GetSampleGroup(name);
            if (activeSampleGroup == null)
            {
                activeSampleGroup = new SampleGroup(name);
                PerformanceTest.AddSampleGroup(activeSampleGroup);
            }

            activeSampleGroup.Samples.Add(value);
        }

        static void VerifyValue(string name, double value)
        {
            if (double.IsNaN(value))
                throw new PerformanceTestException(
                    $"Trying to record value which is not a number for sample group: {name}");
        }

        public static ScopeMeasurement Scope(string name = "Time")
        {
            return new ScopeMeasurement(name);
        }

        public static ProfilerMeasurement ProfilerMarkers(params string[] profilerMarkerLabels)
        {
            return new ProfilerMeasurement(profilerMarkerLabels);
        }

        public static MethodMeasurement Method(Action action)
        {
            return new MethodMeasurement(action);
        }

        public static FramesMeasurement Frames()
        {
            return new FramesMeasurement();
        }
    }

    public struct ScopeMeasurement : IDisposable
    {
        private readonly SampleGroup m_SampleGroup;
        private readonly long m_StartTicks;

        public ScopeMeasurement(string name)
        {
            m_SampleGroup = PerformanceTest.GetSampleGroup(name);
            if (m_SampleGroup == null)
            {
                m_SampleGroup = new SampleGroup(name);
                PerformanceTest.Active.SampleGroups.Add(m_SampleGroup);
            }

            m_StartTicks = Stopwatch.GetTimestamp();
            PerformanceTest.Disposables.Add(this);
        }

        public void Dispose()
        {
            var elapsedTicks = Stopwatch.GetTimestamp() - m_StartTicks;
            PerformanceTest.Disposables.Remove(this);
            var delta = TimeSpan.FromTicks(elapsedTicks).TotalMilliseconds;

            Measure.Custom(m_SampleGroup, delta);
        }
    }

    public struct ProfilerMeasurement : IDisposable
    {
        private readonly ProfilerMarkerMeasurement m_Test;

        public ProfilerMeasurement(string[] profilerMarkers)
        {
            if (profilerMarkers == null)
            {
                m_Test = null;
                return;
            }

            if (profilerMarkers.Length == 0)
            {
                m_Test = null;
                return;
            }

            var go = new GameObject("Recorder");
            if (Application.isPlaying) Object.DontDestroyOnLoad(go);
            go.hideFlags = HideFlags.HideAndDontSave;
            m_Test = go.AddComponent<ProfilerMarkerMeasurement>();
            m_Test.AddProfilerSample(profilerMarkers);
            PerformanceTest.Disposables.Add(this);
        }

        public void Dispose()
        {
            PerformanceTest.Disposables.Remove(this);
            if (m_Test == null) return;
            m_Test.StopAndSampleRecorders();
            Object.DestroyImmediate(m_Test.gameObject);
        }
    }
}