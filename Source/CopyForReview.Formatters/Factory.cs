using System.Collections.Generic;

namespace CopyForReview.Formatters
{
    /// <summary>
    ///     A factory for formatters.
    /// </summary>
    public static class Factory
    {
        /// <summary>
        ///     Gets the available formatters.
        /// </summary>
        /// <returns></returns>
        public static IList<IFormatter> GetFormatters()
        {
            var formatters = new List<IFormatter> { new CSharpToStackOverflow(), new CSharpToFoswiki(), };
            return formatters;
        }
    }
}