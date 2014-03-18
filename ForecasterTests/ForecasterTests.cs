

namespace ForecasterTests
{
    using APIHelper;
    using NUnit.Framework;

    [TestFixture]
    public class ForecasterTests
    {
        [Test]
        public void WhenForeCastIsDoneExpectItReturnsOutput()
        {
            // Arrange
            JSONProvider jp = new JSONProvider();
            //string URL = "AdView/Product/v1?productcategory=TOOTHBRUSH";
            string URL = "RMS/v1/ManufactureDolShr?period=1%20W%2FE%2012%2F14%2F13";

            // Act
            string actual = jp.GetJSON(URL);

            // Assert
            Assert.That(actual.Length > 0);
        }
    }
}
