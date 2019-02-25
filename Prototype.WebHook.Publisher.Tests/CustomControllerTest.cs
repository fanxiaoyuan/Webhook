using System;
using System.Threading;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using Prototype.Webhook.Publisher;

namespace Prototype.WebHook.Publisher.Tests
{
    [TestFixture]
    public class CustomControllerTest
    {
        private CustomController _objectToTest;
        private Mock<IHookSender> _customSenderMock;

        [SetUp]
        public void SetUp()
        {
            _customSenderMock = new Mock<IHookSender>();
            SingletonSender.Instance.SetSender(_customSenderMock.Object);
            _objectToTest = new CustomController();
        }

        [Test]
        public void WhenPostThenSendHook()
        {
            var resetEvent = new ManualResetEventSlim(false);
            _customSenderMock
                .Setup(x => x.LaunchHook(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()))
                .Callback<string, string, string, object>((u, s, a, d) => resetEvent.Set());
            var callbackUri = "http://192.168.1.1:1986/api/webhooks/incoming/custom";
            var secret = Guid.NewGuid().ToString();
            var result = _objectToTest.Post(new Request {CallbackUri = callbackUri, Secret = secret});
            resetEvent.Wait(10000);
            Assert.That(((OkNegotiatedContentResult<string>) result).Content, Is.EqualTo("processing"));
            _customSenderMock.Verify(x => x.LaunchHook(It.Is<string>(i => i == callbackUri), It.Is<string>(i => i == secret), It.Is<string>(i => i == "process completed"), It.Is<object>(i => (i as ProcessResult).Equals(new ProcessResult{FirstName = "Toto", LastName = "Bean"}))));
        }
    }
}
