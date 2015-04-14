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
using System.Collections.Generic;

namespace Codeministry.CopyForReview.Formatters {
    /// <summary>
    ///     A factory for formatters.
    /// </summary>
    public static class Factory {
        /// <summary>
        ///     Gets the available formatters.
        /// </summary>
        /// <returns></returns>
        private static IList<IFormatter> GetFormatters() {
            var formatters = new List<IFormatter>
            {
                new ToText(),
                new ToEmailClient(),
                new ToStackOverflow(),
                new ToFoswiki(),
            };
            return formatters;
        }

        /// <summary>
        ///     Gets the available built-in formatters, plus a custom one for each additional template source.
        /// </summary>
        /// <param name="templateSources">The additional template sources, which is expected to be in dotLiquid markup.</param>
        /// <returns></returns>
        public static IEnumerable<IFormatter> GetFormatters(IEnumerable<String> templateSources) {
            var formatters = GetFormatters();
            foreach (var templateSource in templateSources) {
                formatters.Add(new ToCustomDotLiquidFormatter(templateSource));
            }
            return formatters;
        }
    }
}