using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ThrowException.Test
{
    [TestFixture]
    public class TestThrowArgumentException
    {
        [Test]
        public void TestIf_Valid()
        {
            string foo = string.Empty;
            Assert.That(() => ThrowArgumentException.If(() => foo, a => true, "message"),
                Throws.Nothing);
        }

        [Test]
        public void TestIf_NotValid()
        {
            string foo = string.Empty;
            Assert.That(() => ThrowArgumentException.If(() => foo, a => false, "message"),
                Throws.TypeOf<ArgumentException>()
                    .And.Property("ParamName").EqualTo("foo")
                    .And.Property("Message").StartsWith("message"));
        }

        [Test]
        public void TestIf_NullArgumentExpression()
        {
            Assert.That(() => ThrowArgumentException.If<string>(null, a => true, "message"), 
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("argumentExpression"));
        }

        [Test]
        public void TestIf_NullArgumentValid()
        {
            Assert.That(() => ThrowArgumentException.If(() => "foo", null, "message"),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("argumentValid"));
        }

        [Test]
        public void TestIf_NullMessage()
        {
            Assert.That(() => ThrowArgumentException.If(() => "foo", a => true, null),
                Throws.TypeOf<ArgumentNullException>().And.Property("ParamName").EqualTo("message"));
        }
    }
}
