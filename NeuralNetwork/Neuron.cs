using System.Collections.Generic;

namespace NeuralNetwork
{
    /// <summary>
    /// Implements a neuron representation in a neural network structure that contains an initial <see cref="Bias" /> and <see cref="Value" /> of zero and a list of <see cref="Dendrite" />s which represent connected neurons in previous <see cref="Layer" />s.
    /// </summary>
    internal class Neuron
    {
        /// <summary>
        /// Initializes a new instance of a neuron with a zero initialized <see cref="Bias" /> and <see cref="Value" /> as well as an empty list of connected neurons <see cref="Dendrite" />s.
        /// </summary>
        public Neuron()
        {
            Bias = 0.0;
            Value = 0.0;
            ConnectedNeurons = new List<Dendrite>();
        }
        /// <summary>
        /// The bias (offset) of the neuron.
        /// </summary>
        public double Bias;
        /// <summary>
        /// The current value of the neuron.
        /// </summary>
        public double Value;
        /// <summary>
        /// The connected neurons (<see cref="Dendrite" />s) in previous layers.
        /// </summary>
        public List<Dendrite> ConnectedNeurons;
    }
}
