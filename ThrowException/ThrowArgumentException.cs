using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ThrowException
{
    /// <summary>
    /// Methods to make throwing <see cref="ArgumentException"/> easier.
    /// </summary>
    public static class ThrowArgumentException
    {
        /// <summary>
        /// Throw an <see cref="ArgumentException"/> if <paramref name="argumentValid"/> is false.
        /// The ArgumentException thrown has the given <paramref name="message"/> for the 
        /// argument named in <paramref name="argumentExpression"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The argument type.
        /// </typeparam>
        /// <param name="argumentExpression">
        /// An expression that specifies the parameter to pass to <paramref name="argumentValid"/>
        /// and whose name will be included in the thrown <see cref="ArgumentException"/> if 
        /// argumentValid evaluates to false. This cannot be null.
        /// </param>
        /// <param name="argumentValid">
        /// Return true if the argument is valid, false otherwise. This cannot be null.
        /// </param>
        /// <param name="message">
        /// The message to include in the thrown <see cref="ArgumentException"/> if 
        /// argumentValid evaluates to false. This cannot be null.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        public static void If<T>(Expression<Func<T>> argumentExpression,
                          Predicate<T> argumentValid, string message)
        {
            ThrowArgumentNullException.IfNull(() => argumentExpression, 
                () => argumentValid, () => message);

            string parameterName;

            parameterName = ExpressionHelper.GetArgumentName(argumentExpression);
            if (!argumentValid(argumentExpression.Compile()()))
            {
                throw new ArgumentException(message, parameterName);
            }
        }

    }
}
