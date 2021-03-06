// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.BuildTools.Utilities;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Microsoft.AspNetCore.BuildTools
{
#if SDK
    public class Sdk_GetGitCommitInfo : Task
#elif BuildTools
    public class GetGitCommitInfo : Task
#else
#error This must be built either for an SDK or for BuildTools
#endif
    {
        /// <summary>
        /// A folder inside the git project. Does not need to be the top folder.
        /// This task will search upwards for the .git folder.
        /// </summary>
        [Required]
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// The name of the branch HEAD is pointed to. Can be null or empty
        /// for repositories in detached HEAD mode.
        /// </summary>
        [Output]
        public string Branch { get; set; }

        /// <summary>
        /// The full commit SHA of the current commit referenced by HEAD.
        /// </summary>
        [Output]
        public string CommitHash { get; set; }

        /// <summary>
        /// The folder containing the '.git', not the .git folder itself.
        /// </summary>
        [Output]
        public string RepositoryRootPath { get; set; }

        public override bool Execute()
        {
            try
            {
                var repoInfo = GitRepoInfo.Load(WorkingDirectory);
                Log.LogMessage(MessageImportance.Low, "Resolved git repo info to: RootPath = {0}, GitDir = {1}, HEAD = {2}, Branch = {3}, Hash = {4}",
                    repoInfo.RepositoryRootPath,
                    repoInfo.GitDir,
                    repoInfo.HeadFile,
                    repoInfo.Branch,
                    repoInfo.CommitHash);

                if (repoInfo.DetachedHeadMode)
                {
                    Log.LogWarning("The repo in '{0}' appears to be in detached HEAD mode. Unable to determine current git branch.", repoInfo.RepositoryRootPath);
                }

                if (string.IsNullOrEmpty(repoInfo.CommitHash))
                {
                    Log.LogError("Could not determine the commit hash of the current git repo in '{0}'.", repoInfo.RepositoryRootPath);
                    return false;
                }

                Branch = repoInfo.Branch;
                CommitHash = repoInfo.CommitHash;
                RepositoryRootPath = repoInfo.RepositoryRootPath;
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
                return false;
            }
        }
    }
}
