using System.Threading.Tasks;
using WithMartin.GitCommandBuilder.Extensions;

namespace RPGXEFDevTestServerManager.ExternalHelpers
{
    public class SshHelper
    {
        private readonly string _username;
        private readonly string _keyfilePath;
        private readonly string _hostname;
        private readonly int _port;

        public SshHelper(string username, string keyfilePath, string hostname, int port = 22)
        {
            _username = username;
            _keyfilePath = keyfilePath;
            _hostname = hostname;
            _port = port;
        }

        public void GeneratePrivateKey(string filename = "id_rsa")
        {
            $"ssh-keygen -q -t rsa -b 2048 -f {filename} -N \"\"".Run("");
        }

        public string Run(string cmd)
        {
            return $"ssh -p {_port} -i {_keyfilePath} -o StrictHostKeyChecking=no {_username}@{_hostname} {cmd}".Run("");
        }

        public Task<string> RunAsync(string cmd)
        {
            return $"ssh -p {_port} -i {_keyfilePath} -o StrictHostKeyChecking=no {_username}@{_hostname} {cmd}".RunAsync("");
        }

        public void DownloadFile(string remoteFilePath, string localFilePath)
        {
            $"scp {_username}@{_hostname}:{remoteFilePath} {localFilePath}".Run("");
        }
    }
}
