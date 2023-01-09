using System.Runtime.Serialization;

namespace NeuralNetwork
{
    /// <summary>
    /// 
    /// </summary>
    public class Dendrite
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectedLayer"></param>
        /// <param name="connectedNeuron"></param>
        public Dendrite(Layer connectedLayer, int connectedNeuron)
        {
            Weight = 0;
            ConnectedLayer = connectedLayer;
            Neuron = connectedNeuron;
        }

        /// <summary>
        /// 
        /// </summary>
        public double Weight;
        /// <summary>
        /// 
        /// </summary>
        [IgnoreDataMember]
        public Layer ConnectedLayer;
        /// <summary>
        /// 
        /// </summary>
        public int Neuron;
    }
}