using System.Runtime.Serialization;

namespace NeuralNetwork
{
    /// <summary>
    /// Implements a dendrite representation in a neural network structure that connects 2 <see cref="NeuralNetwork.Neuron" />s together.
    /// </summary>
    /// <remarks>A dendrite always connects a <see cref="NeuralNetwork.Neuron" />s from 1 <see cref="Layer" /> to a previous <see cref="Layer" />.</remarks>
    internal class Dendrite
    {
        /// <summary>
        /// Initializes a new instance of a dendrite with an initial <see cref="Weight" /> of zero that connects two <see cref="NeuralNetwork.Neuron" />s from two different <see cref="Layer" />s.
        /// </summary>
        /// <param name="connectedLayer">The layer in which the connected neuron resides</param>
        /// <param name="connectedNeuron">The identifier for the connected neuron in the connected layer</param>
        public Dendrite(Layer connectedLayer, int connectedNeuron)
        {
            Weight = 0;
            ConnectedLayer = connectedLayer;
            Neuron = connectedNeuron;
        }
        /// <summary>
        /// The weight between the two <see cref="NeuralNetwork.Neuron" />s.
        /// </summary>
        public double Weight;
        /// <summary>
        /// layer in which the connected <see cref="NeuralNetwork.Neuron" />s resides. 
        /// </summary>
        /// <remarks>This attribute will not get serialized as this would lead to a nested serialization cycle.</remarks>
        [IgnoreDataMember]
        public Layer ConnectedLayer;
        /// <summary>
        /// The identifier for the connected <see cref="NeuralNetwork.Neuron" /> in the connected <see cref="Layer" />.
        /// </summary>
        public int Neuron;
    }
}