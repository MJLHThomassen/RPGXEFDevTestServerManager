using System.Collections.Generic;
using RPGXEFDevTestServerManager.GitHelpers.Model;

namespace RPGXEFDevTestServerManager.Models
{
    public class HomeViewModel
    {
        public string BuildHistoryDir { get; set; }
        public BranchInfo CurrentBranch { get; set; }
        public IEnumerable<BranchInfo> Branches { get; set; }
    }
}
