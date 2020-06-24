using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderProcessingApplication;

namespace OderProcessingUnitTest
{
    [TestClass]
    public class OderProcessingUnitTest
    {
        [TestMethod]
        public void ShouldReturnVideoSlipOnly()
        {
            var result = OrderProcessor.ConvertInputToType(new string[] { "video", "Random" });
            Assert.AreEqual("Random", result.ItemName);
            Assert.AreEqual("Generated a packing slip.", result.Operations[0]);
            Assert.AreEqual(1, result.Operations.Count);

        }

        [TestMethod]
        public void ShouldReturnVideoLearningToSkiSlipOnly()
        {
            var result = OrderProcessor.ConvertInputToType(new string[] { "video", "Learning To Ski" });
            Assert.AreEqual("Learning To Ski", result.ItemName);
            Assert.AreEqual("Generated a packing slip.", result.Operations[0]);
            Assert.AreEqual("'First Aid' video added to the packing slip", result.Operations[1]);
            Assert.AreEqual(2, result.Operations.Count);
        }

        [TestMethod]
        public void ShouldReturnUpgradeSlipOnly()
        {
            var result = OrderProcessor.ConvertInputToType(new string[] { "Upgrade", "Random" });
            Assert.IsNull(result.ItemName);
            Assert.AreEqual("Generated a packing slip.", result.Operations[0]);
            Assert.AreEqual("Apply the upgrade", result.Operations[1]);
            Assert.AreEqual("Mail Sent", result.Operations[2]);
            Assert.AreEqual(3, result.Operations.Count);

        }

        [TestMethod]
        public void ShouldReturnMembershipSlip()
        {
            var result = OrderProcessor.ConvertInputToType(new string[] { "Membership", "Random" });
            Assert.IsNull(result.ItemName);
            Assert.AreEqual("Generated a packing slip.", result.Operations[0]);
            Assert.AreEqual("Activate that membership", result.Operations[1]);
            Assert.AreEqual("Mail Sent", result.Operations[2]);
            Assert.AreEqual(3, result.Operations.Count);

        }

        [TestMethod]
        public void ShouldReturnBookSlipOnly()
        {
            var result = OrderProcessor.ConvertInputToType(new string[] { "Book", "Random" });
            Assert.AreEqual("Random", result.ItemName);
            Assert.AreEqual("Generated a packing slip for shipping.", result.Operations[0]);
            Assert.AreEqual("Commission payment to the agent", result.Operations[1]);
            Assert.AreEqual("Create a duplicate packing slip for the royalty department.", result.Operations[2]);
            Assert.AreEqual(3, result.Operations.Count);

        }

        [TestMethod]
        public void ShouldReturnOther()
        {
            var result = OrderProcessor.ConvertInputToType(new string[] { "other", "Random" });
            Assert.AreEqual("Random", result.ItemName);
            Assert.AreEqual("Generated a packing slip for shipping.", result.Operations[0]);
            Assert.AreEqual("Commission payment to the agent", result.Operations[1]);
            Assert.AreEqual(2, result.Operations.Count);

            result = OrderProcessor.ConvertInputToType(new string[] { "random", "Random" });
            Assert.AreEqual("Random", result.ItemName);
            Assert.AreEqual("Generated a packing slip for shipping.", result.Operations[0]);
            Assert.AreEqual("Commission payment to the agent", result.Operations[1]);
            Assert.AreEqual(2, result.Operations.Count);
        }
    }
}
