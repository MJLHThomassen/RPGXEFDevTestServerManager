using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WithMartin.GitCommandBuilder.Extensions
{
    public static class StringExtensions
    {
        public static string Run(this string cmd, string workingDir)
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

            if (command == "git")
            {
                command = "C:\\Program Files\\Git\\bin\\git.exe";
            }

            if (command == "ssh")
            {
                command = "C:\\Program Files\\Git\\usr\\bin\\ssh.exe";
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

        public static Task<string> RunAsync(this string cmd, string workingDir)
        {
            return Task.Run(() => Run(cmd, workingDir));
        }
    }
}