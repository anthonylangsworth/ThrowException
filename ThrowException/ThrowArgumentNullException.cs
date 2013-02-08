using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ThrowException
{
    /// <summary>
    /// Methods to make throwing <see cref="ArgumentNullException"/> easier.
    /// </summary>
    public static class ThrowArgumentNullException
    {
        /// <summary>
        /// Throw an <see cref="ArgumentNullException"/> if any of the 
        /// provided expressions are null, passing the correct argument name
        /// to the exception constructor.
        /// </summary>
        /// <remarks>
        /// Instead of:
        /// <code>
        /// if(argument1 == null) throw new ArgumentNullException("argument1");
        /// if(argument2 == null) throw new ArgumentNullException("argument2");
        /// </code>
        /// at the top of a method to check for null arguments and throw the
        /// appropriate <see cref="ArgumentNullException"/>s, use:
        /// <code>
        /// ThrowArgumentNullException.IfNull(() =&gt; argument1, () =&gt; argument2);
        /// </code>
        /// The ArgumentNullException thrown contains the correct argument name.
        /// </remarks>
        /// <param name="expressions">
        /// One or more lamba functions that each contain the name of an argument to test.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="expressions"/> is null or one of the expressions contained
        /// an argument that evaluated to null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// One of the expressions was not solely a parameter name.
        /// </exception>
        public static void IfNull(params Expression<Func<object>>[] expressions)
        {
            if (expressions == null)
            {
                throw new ArgumentNullException("expressions");
            }

            string argumentName;

            foreach (Expression<Func<object>> expression in expressions)
            {
                argumentName = ExpressionHelper.GetArgumentName(expression);
                if(expression.Compile()() == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }
        }
    }
}
