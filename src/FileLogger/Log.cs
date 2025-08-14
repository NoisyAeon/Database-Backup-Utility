using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Database_Backup_Utility.FileLogger
{

    /// <summary>
    /// This class writes all log messages to the specific log file.
    /// It separates between Debug, Info and Error logs.
    /// </summary>
    class Log
    {
        private static readonly Logger InfoLogger;
        private static readonly Logger DebugLogger;
        private static readonly Logger ErrorLogger;
        private static readonly string OutputDir = @"..\..\..\log";
        private static readonly string OutputTemplate = "{Timestamp:dd.MM.yyyy HH:mm:ss.ff ->} {Message:lj}{NewLine}";
        private static readonly string ErrorTemplate = "{Timestamp:dd.MM.yyyy HH:mm:ss.ff} [{Level:u7}] {Message:lj}{Exception}";


        /// <summary>
        /// static constructor that sets up each File Logger once
        /// </summary>
        static Log()
        {
            ErrorLogger = SetUpErrorLogger();
            InfoLogger = SetUpInfoLogger();
            DebugLogger = SetUpDebugLogger();
        }

        private static Logger SetUpInfoLogger()
        {
            var dir = OutputDir + @"\Info\";
            Directory.CreateDirectory(dir);

            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(dir + ".log", rollingInterval: RollingInterval.Year, rollOnFileSizeLimit: true, retainedFileCountLimit: 9, outputTemplate: OutputTemplate)
                .CreateLogger();
        }
        private static Logger SetUpDebugLogger()
        {
            var dir = OutputDir + @"\Debug\";
            Directory.CreateDirectory(dir);

            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(dir + ".log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, retainedFileCountLimit: 20, outputTemplate: OutputTemplate)
                .CreateLogger();
        }
        private static Logger SetUpErrorLogger()
        {
            var dir = OutputDir + @"\Error\";
            Directory.CreateDirectory(dir);

            return new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error, outputTemplate: ErrorTemplate)
                .WriteTo.File(dir + ".log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, retainedFileCountLimit: 9, outputTemplate: ErrorTemplate)
                .CreateLogger();
        }

        /// <summary>
        /// Writes the given message to the Debug log file
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            DebugLogger.Debug(message);
        }


        /// <summary>
        /// Writes the given message to the Info log file
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            InfoLogger.Information(message);
        }


        /// <summary>
        /// Writes the given message to the Error log file and marks the log level as "Warning"
        /// </summary>
        /// <param name="message"></param>
        public static void Warning(string message)
        {
            ErrorLogger.Warning(message + "\n");
        }

        /// <summary>
        /// Writes the given message to the Error log file and marks the log level as "Error"
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            ErrorLogger.Error(message + "\n");
        }

        /// <summary>
        /// Writes the given Exception with traceback the Error log file and marks the log level as "Error"
        /// </summary>
        /// <param name="message"></param>
        public static void Error(Exception exception)
        {
            ErrorLogger.Error(exception, "");
        }


        /// <summary>
        /// Writes the given message to the Error log file and marks the log level as "Fatal"
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(string message)
        {
            ErrorLogger.Fatal(message + "\n");
        }

        /// <summary>
        /// Writes the given Exception with traceback the Error log file and marks the log level as "Fatal"
        /// </summary>
        /// <param name="message"></param>
        public static void Fatal(Exception exception)
        {
            ErrorLogger.Fatal(exception, "");
        }

        /// <summary>
        /// Writes the given message to the console and the corresponding log file
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ToConsole(LogLevel level, string message)
        {

            switch (level)
            {
                case LogLevel.Debug: Debug(message); break;
                case LogLevel.Info: Info(message); break;
                case LogLevel.Warning: Warning(message); break;
                case LogLevel.Error: Error(message); return;
                case LogLevel.Fatal: Fatal(message); return;
                default:
                    throw new ArgumentException($"{level} is not a known Log level!");

            }
            Console.WriteLine(message);

        }
    }
}

/// <summary>
/// Enum that describes the different levels accepted by <see cref="Log"/>
/// </summary>
public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error,
    Fatal
}