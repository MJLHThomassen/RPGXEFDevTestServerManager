using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using RPGXEFDevTestServerManager.ExternalHelpers;
using RPGXEFDevTestServerManager.GitHelpers.Model;
using RPGXEFDevTestServerManager.Models;

namespace RPGXEFDevTestServerManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly GitHelper _gitHelper;
        private readonly SshHelper _sshHelper;
        public readonly string _buildHistoryDir;

        public ApplicationUserManager UserManager { get; private set; }

        public HomeController(
            GitHelper gitHelper, 
            SshHelper sshHelper, 
            string buildHistoryDir,
            ApplicationUserManager userManager)
        {
            _gitHelper = gitHelper;
            _sshHelper = sshHelper;
            _buildHistoryDir = buildHistoryDir;

            UserManager = userManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (!UserManager.Users.Any())
            {
                return RedirectToAction("RegisterFirstUser", "Account"); ;
            }

            var currentBranch = _gitHelper.GetCurrentBranch();
            var remoteBranches = _gitHelper.GetRemoteBranches();

            var model = GetCurrentlyRunningViewModel(currentBranch);

            model.Branches = remoteBranches
                .Where(branch => branch.Key != currentBranch.Key)
                .Select(branch => new BranchInfo
                {
                    HeadCommitHash = branch.Key,
                    Name = branch.Value,
                    HasBeenBuild = Directory.Exists(Path.Combine(_buildHistoryDir, branch.Key)),
                    IsBuildLogAvailable =
                        System.IO.File.Exists(Path.Combine(_buildHistoryDir, branch.Key, "binaries", "buildlog.txt")),
                    AreLinuxX86BinariesBuild =
                        System.IO.File.Exists(Path.Combine(_buildHistoryDir, branch.Key, "binaries", "linux_x86.zip")),
                    AreLinuxX64BinariesBuild =
                        System.IO.File.Exists(Path.Combine(_buildHistoryDir, branch.Key, "binaries", "linux_x64.zip")),
                    AreWindowsX86BinariesBuild =
                        System.IO.File.Exists(Path.Combine(_buildHistoryDir, branch.Key, "binaries", "windows_x86.zip")),
                    AreWindowsX64BinariesBuild = false
                });

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Activate(string hash)
        {
            // TODO: How to give users feedback on this?

            // Switch branch locally
            _gitHelper.SwitchToClean(hash);

            // Run the build script on the server
            _sshHelper.Run("daemon /vagrant/sshBuildAndRun.sh");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult GetCurrentlyRunning()
        {
            var currentBranch = _gitHelper.GetCurrentBranch();

            var model = GetCurrentlyRunningViewModel(currentBranch);

            return PartialView("CurrentlyRunning", model);
        }

        private HomeViewModel GetCurrentlyRunningViewModel(KeyValuePair<string, string> currentBranch)
        {
            var model = new HomeViewModel
            {
                BuildHistoryDir = _buildHistoryDir,
                CurrentBranch = new BranchInfo
                {
                    HeadCommitHash = currentBranch.Key,
                    Name = currentBranch.Value,
                    HasBeenBuild = Directory.Exists(Path.Combine(_buildHistoryDir, currentBranch.Key)),
                    IsBuildLogAvailable =
                        System.IO.File.Exists(Path.Combine(_buildHistoryDir, currentBranch.Key, "binaries", "buildlog.txt")),
                    AreLinuxX86BinariesBuild =
                        System.IO.File.Exists(Path.Combine(_buildHistoryDir, currentBranch.Key, "binaries", "linux_x86.zip")),
                    AreLinuxX64BinariesBuild =
                        System.IO.File.Exists(Path.Combine(_buildHistoryDir, currentBranch.Key, "binaries", "linux_x64.zip")),
                    AreWindowsX86BinariesBuild =
                        System.IO.File.Exists(Path.Combine(_buildHistoryDir, currentBranch.Key, "binaries", "windows_x86.zip")),
                    AreWindowsX64BinariesBuild = false
                }
            };

            var statusFilePath = Path.Combine(_buildHistoryDir, currentBranch.Key, "status.txt");

            if (System.IO.File.Exists(statusFilePath))
            {
                var statusFileContent = System.IO.File.ReadAllText(statusFilePath);

                using (var reader = new StringReader(statusFileContent))
                {
                    model.CurrentBranch.Status = reader.ReadLine();
                }
            }
            else
            {
                model.CurrentBranch.Status = "Checking out Commit";
            }

            return model;
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}