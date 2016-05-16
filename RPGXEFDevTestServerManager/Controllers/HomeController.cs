using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using RPGXEFDevTestServerManager.ExternalHelpers;
using RPGXEFDevTestServerManager.GitHelpers.Model;
using RPGXEFDevTestServerManager.Models;

namespace RPGXEFDevTestServerManager.Controllers
{
    public class HomeController : Controller
    {
        public static readonly string BuildHistoryDir = ConfigurationManager.AppSettings["rpgxefbuildhistorypath"];

        private readonly GitHelper _gitHelper;
        private readonly SshHelper _sshHelper;

        public HomeController(GitHelper gitHelper, SshHelper sshHelper)
        {
            _gitHelper = gitHelper;
            _sshHelper = sshHelper;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var currentBranch = _gitHelper.GetCurrentBranch();
            var remoteBranches = _gitHelper.GetRemoteBranches();

            var model = GetCurrentlyRunningViewModel(currentBranch);

            model.Branches = remoteBranches
                .Where(branch => branch.Key != currentBranch.Key)
                .Select(branch => new BranchInfo
                {
                    HeadCommitHash = branch.Key,
                    Name = branch.Value,
                    HasBeenBuild = Directory.Exists(Path.Combine(BuildHistoryDir, branch.Key)),
                    IsBuildLogAvailable =
                        System.IO.File.Exists(Path.Combine(BuildHistoryDir, branch.Key, "binaries", "buildlog.txt")),
                    AreLinuxX86BinariesBuild =
                        System.IO.File.Exists(Path.Combine(BuildHistoryDir, branch.Key, "binaries", "linux_x86.zip")),
                    AreLinuxX64BinariesBuild =
                        System.IO.File.Exists(Path.Combine(BuildHistoryDir, branch.Key, "binaries", "linux_x64.zip")),
                    AreWindowsX86BinariesBuild =
                        System.IO.File.Exists(Path.Combine(BuildHistoryDir, branch.Key, "binaries", "windows_x86.zip")),
                    AreWindowsX64BinariesBuild = false
                });

            return View(model);
        }

        [HttpGet]
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
                BuildHistoryDir = BuildHistoryDir,
                CurrentBranch = new BranchInfo
                {
                    HeadCommitHash = currentBranch.Key,
                    Name = currentBranch.Value,
                    HasBeenBuild = Directory.Exists(Path.Combine(BuildHistoryDir, currentBranch.Key)),
                    IsBuildLogAvailable =
                        System.IO.File.Exists(Path.Combine(BuildHistoryDir, currentBranch.Key, "binaries", "buildlog.txt")),
                    AreLinuxX86BinariesBuild =
                        System.IO.File.Exists(Path.Combine(BuildHistoryDir, currentBranch.Key, "binaries", "linux_x86.zip")),
                    AreLinuxX64BinariesBuild =
                        System.IO.File.Exists(Path.Combine(BuildHistoryDir, currentBranch.Key, "binaries", "linux_x64.zip")),
                    AreWindowsX86BinariesBuild =
                        System.IO.File.Exists(Path.Combine(BuildHistoryDir, currentBranch.Key, "binaries", "windows_x86.zip")),
                    AreWindowsX64BinariesBuild = false
                }
            };

            var statusFilePath = Path.Combine(BuildHistoryDir, currentBranch.Key, "status.txt");

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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}