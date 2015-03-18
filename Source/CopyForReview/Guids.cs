// Guids.cs
// MUST match guids.h

using System;

namespace CopyForReview
{
    static class GuidList
    {
        public const string guidCopyForReviewPkgString = "193eba43-9462-4945-ba4e-79f04dbadc94";
        public const string guidCopyForReviewCmdSetString = "4ae6ff5a-6e7e-48bd-86b0-37fd9ab20629";

        public static readonly Guid guidCopyForReviewCmdSet = new Guid(guidCopyForReviewCmdSetString);
    };
}