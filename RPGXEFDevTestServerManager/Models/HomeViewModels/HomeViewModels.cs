using System.Collections.Generic;

namespace RPGXEFDevTestServerManager.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public string BuildHistoryDir { get; set; } = "";
        public BranchInfo CurrentBranch { get; set; } = new BranchInfo();
        public IEnumerable<BranchInfo> Branches { get; set; } = new List<BranchInfo>(); 
    }
}
