using System;
using System.Diagnostics;
using System.IO;

namespace WithMartin.Extensions
{
    /// <summary>
    /// String Extensions for running strings in a cmd.exe environment.
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// Runs the string cmd in a cmd.exe environment and returns the result.
        /// </summary>
        /// <param name="cmd">The cmd to execute.</param>
        /// <param name="workingDir">The working directory to execute the cmd in.</param>
        /// <returns>The result of running the cmd. Does not return if the command spawns a new process that does not end.</returns>
        public static string RunInCmd(this string cmd, string workingDir)
        {
            string command, args;

            var commandLength = cmd.IndexOf(' ');

            if (commandLength == -1)
            {
                command = cmd;
                args = "";
            }
            else
            {
                command = cmd.Substring(0, commandLength);
                args = cmd.Substring(commandLength + 1);
            }

            if (!Path.IsPathRooted(command))
            {
                command = Where(command);
            }

            if (!string.IsNullOrEmpty(workingDir) && !Path.IsPathRooted(workingDir))
            {
                throw new Exception($"{nameof(workingDir)} must be rooted.");
            }

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDir
                }
            };

            try
            {
                proc.Start();

                var output = proc.StandardOutput.ReadToEnd();

                proc.WaitForExit();

                if (proc.ExitCode != 0)
                {
                    throw new Exception($"The process {proc.StartInfo.FileName} {proc.StartInfo.Arguments} has exited with code {proc.ExitCode}: {proc.StandardError.ReadToEnd()}");
                }

                return output;
            }
            finally
            {
                proc.Close();
            }
        }

        /// <summary>
        /// Runs "where cmd" in a cmd.exe environment and returns the results.
        /// </summary>
        /// <param name="cmd">The cmd to "where".</param>
        /// <returns>The result of the "where cmd" e.g. the location on disk where the cmd program is located if it is added to the PATH.</returns>
        public static string Where(string cmd)
        {
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C where " + cmd,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    Verb = "runas"
                }
            };

            try
            {
                proc.Start();

                var output = proc.StandardOutput.ReadToEnd();

                proc.WaitForExit();

                if (proc.ExitCode != 0)
                {
                    throw new Exception($"The process {proc.StartInfo.FileName} {proc.StartInfo.Arguments} has exited with code {proc.ExitCode}: {proc.StandardError.ReadToEnd()}");
                }

                // Remove trailing\r\n
                output = output.TrimEnd('\r','\n');

                return output;
            }
            finally
            {
                proc.Close();
            }
        }
    }
}