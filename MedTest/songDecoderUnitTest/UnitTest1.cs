using MedTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace songDecoderUnitTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            songDecoder songDecoder = new songDecoder();

            Assert.AreEqual("A B C", songDecoder.strDecoder("AWUBBWUBC"));
        }
        [TestMethod]
        public void TestMethod2()
        {
            songDecoder songDecoder = new songDecoder();

            Assert.AreEqual("A B C", songDecoder.strDecoder("AWUBWUBWUBBWUBWUBWUBC"));
        }


        [TestMethod]
        public void TestMethod3()
        {
            songDecoder songDecoder = new songDecoder();

            Assert.AreEqual("A B C", songDecoder.strDecoder("WUBAWUBBWUBCWUB"));
        }
    }
}
