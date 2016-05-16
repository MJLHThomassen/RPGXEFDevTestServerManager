using RPGXEFDevTestServerManager.Attributes;

namespace RPGXEFDevTestServerManager.GitHelpers.Model
{
    public class BranchInfo
    {
        public string Name { get; set; }
        public string HeadCommitHash { get; set; }
        public string Status { get; set; }

        public bool HasBeenBuild { get; set; }
        public bool IsBuildLogAvailable { get; set; }

        public bool AreWindowsX86BinariesBuild { get; set; }
        public bool AreWindowsX64BinariesBuild { get; set; }
        public bool AreLinuxX86BinariesBuild { get; set; }
        public bool AreLinuxX64BinariesBuild { get; set; }
    }
}
