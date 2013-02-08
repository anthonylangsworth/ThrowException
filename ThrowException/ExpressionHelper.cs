using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ThrowException
{
    /// <summary>
    /// Helper classes for dealing with <see cref="Expression"/>s.
    /// </summary>
    internal static class ExpressionHelper
    {
        /// <summary>
        /// Get the argument (technically parameter) name mentioned in 
        /// <paramref name="argumentExpression"/>. 
        /// </summary>
        /// <remarks>
        /// Technically, this will work with any variable passed into the expression but
        /// the intended use is for extracting argument names.
        /// </remarks>
        /// <typeparam name="T">
        /// The argument's type.
        /// </typeparam>
        /// <param name="argumentExpression">
        /// The expression to extract the argument name from. This cannot be null.
        /// </param>
        /// <returns>
        /// The argument (or variable) name.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="argumentExpression"/> cannot be null.
        /// </exception>
        internal static string GetArgumentName<T>(Expression<Func<T>> argumentExpression)
        {
            // Cannot use ThrowArgumentNullException.IfNull due to the 
            // resulting infinite recursion.
            if (argumentExpression == null)
            {
                throw new ArgumentNullException("argumentExpression");
            }

            MemberExpression memberExpression;

            memberExpression = argumentExpression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException(
                    string.Format("Expression '{0}' must contain solely the argument name", argumentExpression));
            }
            else
            {
                return memberExpression.Member.Name;
            }
        }
    }
}
