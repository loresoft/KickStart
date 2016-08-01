// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
#if PORTABLE
namespace KickStart.Portability
{
    using System;

    // This class uses high-resolution performance counter if installed hardware 
    // does not support it. Otherwise, the class will fall back to DateTime class
    // and uses ticks as a measurement.

    /// <summary>
    /// Provides a set of methods and properties that you can use to accurately measure elapsed time.
    /// </summary>
    public class Stopwatch
    {
        private const long TicksPerMillisecond = 10000;
        private const long TicksPerSecond = TicksPerMillisecond * 1000;
        
        // performance-counter frequency, in counts per ticks.
        // This can speed up conversion from high frequency performance-counter 
        // to ticks. 
        private static readonly double s_tickFrequency;

        private long _elapsed;
        private long _startTimeStamp;
        private bool _isRunning;

        // "Frequency" stores the frequency of the high-resolution performance counter, 
        // if one exists. Otherwise it will store TicksPerSecond. 
        // The frequency cannot change while the system is running,
        // so we only need to initialize it once. 

        /// <summary>
        /// The frequency
        /// </summary>
        public static readonly long Frequency;
        /// <summary>
        /// The is high resolution
        /// </summary>
        public static readonly bool IsHighResolution;

        /// <summary>
        /// Initializes the <see cref="Stopwatch"/> class.
        /// </summary>
        static Stopwatch()
        {
            bool succeeded = QueryPerformanceFrequency(out Frequency);

            if (!succeeded)
            {
                IsHighResolution = false;
                Frequency = TicksPerSecond;
                s_tickFrequency = 1;
            }
            else
            {
                IsHighResolution = true;
                s_tickFrequency = TicksPerSecond;
                s_tickFrequency /= Frequency;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Stopwatch"/> class.
        /// </summary>
        public Stopwatch()
        {
            Reset();
        }

        /// <summary>
        /// Starts, or resumes, measuring elapsed time for an interval.
        /// </summary>
        public void Start()
        {
            // Calling start on a running Stopwatch is a no-op.
            if (!_isRunning)
            {
                _startTimeStamp = GetTimestamp();
                _isRunning = true;
            }
        }

        /// <summary>
        /// Initializes a new Stopwatch instance, sets the elapsed time property to zero, and starts measuring elapsed time.
        /// </summary>
        /// <returns>A Stopwatch that has just begun measuring elapsed time.</returns>
        public static Stopwatch StartNew()
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            return s;
        }

        /// <summary>
        /// Stops measuring elapsed time for an interval.
        /// </summary>
        public void Stop()
        {
            // Calling stop on a stopped Stopwatch is a no-op.
            if (_isRunning)
            {
                long endTimeStamp = GetTimestamp();
                long elapsedThisPeriod = endTimeStamp - _startTimeStamp;
                _elapsed += elapsedThisPeriod;
                _isRunning = false;

                if (_elapsed < 0)
                {
                    // When measuring small time periods the StopWatch.Elapsed* 
                    // properties can return negative values.  This is due to 
                    // bugs in the basic input/output system (BIOS) or the hardware
                    // abstraction layer (HAL) on machines with variable-speed CPUs
                    // (e.g. Intel SpeedStep).

                    _elapsed = 0;
                }
            }
        }

        /// <summary>
        /// Stops time interval measurement and resets the elapsed time to zero.
        /// </summary>
        public void Reset()
        {
            _elapsed = 0;
            _isRunning = false;
            _startTimeStamp = 0;
        }

        /// <summary>
        /// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
        /// </summary>
        public void Restart()
        {
            _elapsed = 0;
            _startTimeStamp = GetTimestamp();
            _isRunning = true;
        }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </value>
        public bool IsRunning
        {
            get { return _isRunning; }
        }

        /// <summary>
        /// Gets the total elapsed time measured by the current instance.
        /// </summary>
        /// <value>
        /// The elapsed.
        /// </value>
        public TimeSpan Elapsed
        {
            get { return new TimeSpan(GetElapsedDateTimeTicks()); }
        }

        /// <summary>
        /// Gets the total elapsed time measured by the current instance, in milliseconds.
        /// </summary>
        /// <value>
        /// The elapsed milliseconds.
        /// </value>
        public long ElapsedMilliseconds
        {
            get { return GetElapsedDateTimeTicks() / TicksPerMillisecond; }
        }

        /// <summary>
        /// Gets the total elapsed time measured by the current instance, in timer ticks.
        /// </summary>
        /// <value>
        /// The elapsed ticks.
        /// </value>
        public long ElapsedTicks
        {
            get { return GetRawElapsedTicks(); }
        }

        /// <summary>
        /// Gets the current number of ticks in the timer mechanism.
        /// </summary>
        /// <returns></returns>
        public static long GetTimestamp()
        {
            if (IsHighResolution)
            {
                long timestamp = 0;
                QueryPerformanceCounter(out timestamp);
                return timestamp;
            }
            else
            {
                return DateTime.UtcNow.Ticks;
            }
        }

        // Get the elapsed ticks.        
        private long GetRawElapsedTicks()
        {
            long timeElapsed = _elapsed;

            if (_isRunning)
            {
                // If the StopWatch is running, add elapsed time since
                // the Stopwatch is started last time. 
                long currentTimeStamp = GetTimestamp();
                long elapsedUntilNow = currentTimeStamp - _startTimeStamp;
                timeElapsed += elapsedUntilNow;
            }
            return timeElapsed;
        }

        // Get the elapsed ticks.        
        private long GetElapsedDateTimeTicks()
        {
            long rawTicks = GetRawElapsedTicks();
            if (IsHighResolution)
            {
                // convert high resolution perf counter to DateTime ticks
                double dticks = rawTicks;
                dticks *= s_tickFrequency;
                return unchecked((long)dticks);
            }
            else
            {
                return rawTicks;
            }
        }


        private static bool QueryPerformanceFrequency(out long frequency)
        {
            frequency = 1;
            return false;
        }

        private static void QueryPerformanceCounter(out long timestamp)
        {
            timestamp = DateTime.UtcNow.Ticks;
        }
    }
}
#endif