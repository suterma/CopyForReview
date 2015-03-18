// 
//     Copy for review, code sharing made simple.
//     Copyright (C) 2015 by marcel suter, marcel@codeministry.ch
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace Codeministry.CopyForReview
{
    internal static class GuidList
    {
        public const string GuidCopyForReviewPkgString = "193eba43-9462-4945-ba4e-79f04dbadc94";
        public const string GuidCopyForReviewCmdSetString = "4ae6ff5a-6e7e-48bd-86b0-37fd9ab20629";

        public static readonly Guid GuidCopyForReviewCmdSet = new Guid(GuidCopyForReviewCmdSetString);
    };
}