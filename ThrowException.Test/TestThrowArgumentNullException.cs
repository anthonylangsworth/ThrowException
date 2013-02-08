using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ThrowException;

namespace ThrowException.Test
{
    [TestFixture]
    public class TestThrowArgumentNullException
    {
        [Test]
        [TestCase(null)]
        [TestCase("foo")]
        public void TestIfNull(object a)
        {
            if (a == null)
            {
                Assert.That(() => ThrowArgumentNullException.IfNull(() => a),
                            Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("a"));
            }
            else
            {
                Assert.That(() => ThrowArgumentNullException.IfNull(() => a), Throws.Nothing);
            }
        }

        [Test]
        public void TestIfNull_EmptyList()
        {
            Assert.That(() => ThrowArgumentNullException.IfNull(), Throws.Nothing);
            
        }

        [Test]
        public void TestIfNull_NullList()
        {
            Assert.That(() => ThrowArgumentNullException.IfNull(null), 
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("expressions"));
        }

        [Test]
        public void TestIfNull_MultipleExpressionsWithDifferentTypes()
        {
            string a = string.Empty;
            object b = null;
            Assert.That(() => ThrowArgumentNullException.IfNull(() => a, () => b),
                        Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("b"));           
        }

        [Test]
        public void TestIfNull_IncorrectNonMemberExpression()
        {
            Assert.That(() => ThrowArgumentNullException.IfNull(() => "a" + "b"),
                        Throws.TypeOf<ArgumentException>()
                            .And.Property("Message").StartsWith("Expression '() => \"ab\"' must contain solely the argument name"));
        }

        [Test]
        public void TestIfNull_NullExpression()
        {
            Assert.That(() => ThrowArgumentNullException.IfNull(() => null),
                        Throws.TypeOf<ArgumentException>()
                            .And.Property("Message").StartsWith("Expression '() => null' must contain solely the argument name"));
        }

    }
}
