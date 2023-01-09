using NeuralNetwork;
using NUnit.Framework;


namespace UnitTests
{
    public class Tests
    {
        [Test]
        public void SerializationTest()
        {
            string originalJSON = FCNeuralNetwork.Serialize(new FCNeuralNetwork(2, 2, 2));
            FCNeuralNetwork copy = FCNeuralNetwork.Deserialize(originalJSON);
            string copyJSON = FCNeuralNetwork.Serialize(copy);
            Assert.AreEqual(originalJSON, copyJSON, "Neural nets could not be saved and reimported!");
        }
    }
}