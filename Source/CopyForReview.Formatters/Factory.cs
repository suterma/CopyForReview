using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyForReview.Formatters
{
    /// <summary>
    /// A factory for formatters.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Gets the available formatters.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IFormatter> GetFormatters()
        {
            var formatters = new List<IFormatter> {new CSharpToFoswiki()};
            return formatters;
        }
    }
}
