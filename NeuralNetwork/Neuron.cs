using System.Collections.Generic;

namespace NeuralNetwork
{
    /// <summary>
    /// 
    /// </summary>
    public class Neuron
    {
        /// <summary>
        /// 
        /// </summary>
        public Neuron()
        {
            Bias = 0.0;
            ConnectedNeurons = new List<Dendrite>();
        }

        /// <summary>
        /// 
        /// </summary>
        public double Bias;
        /// <summary>
        /// 
        /// </summary>
        public double Value;
        /// <summary>
        /// 
        /// </summary>
        public List<Dendrite> ConnectedNeurons;
    }
}
