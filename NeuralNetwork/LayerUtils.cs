using System;
using System.Linq;
using Utils;

namespace NeuralNetwork
{
    internal static class LayerUtils
    {
        /// <summary>
        /// Randomly inherits (changes) the <see cref="Neuron.Bias" />s and <see cref="Dendrite.Weight" />s of the <see cref="Neuron" />s in the srcLayer to the targetLayer.
        /// </summary>
        /// <param name="srcLayer">Bias and weight (Neurons) source</param>
        /// <param name="targetLayer">Bias and weight (Neurons) target</param>
        /// <param name="random">Random number generator that is used to randomly determine the biases and weights which should be inherited</param>
        internal static void InheritLayerBiasAndWeightsRandomly(Layer srcLayer, Layer targetLayer, Random random)
        {
            for (var i = 0; i < srcLayer.NeuronList.Count; i++)
            {
                if (RandomUtils.RandomCoinFlip(random))
                {
                    srcLayer.NeuronList[i].Bias = targetLayer.NeuronList[i].Bias;
                }

                for (var j = 0; j < srcLayer.NeuronList[i].ConnectedNeurons.Count; j++)
                {
                    if (RandomUtils.RandomCoinFlip(random))
                    {
                        srcLayer.NeuronList[i].ConnectedNeurons[j].Weight = targetLayer.NeuronList[i].ConnectedNeurons[j].Weight;
                    }
                }
            }
        }
        /// <summary>
        /// Randomly initialize the <see cref="Neuron.Bias" />s and <see cref="Dendrite.Weight" />s of the <see cref="Neuron" />s in the layer with values in the range [-1.0;1.0].
        /// </summary>
        /// <param name="layer">Layer to be initialized</param>
        /// <param name="random">Random number generator that is used to randomly initialize the biases and weights of a layer</param>
        internal static void PopulateLayerRandomly(Layer layer, Random random)
        {
            foreach (var neuron in layer.NeuronList)
            {
                neuron.Bias = RandomUtils.GetRandomNumber(-1.0, 1.0, random);
                foreach (var dendrite in neuron.ConnectedNeurons)
                {
                    dendrite.Weight = RandomUtils.GetRandomNumber(-1.0, 1.0, random);
                }
            }
        }
        /// <summary>
        /// Randomly reinitialize the <see cref="Neuron.Bias" />s and <see cref="Dendrite.Weight" />s of a layer with values in the range [-1.0;1.0].
        /// </summary>
        /// <param name="layer">Layer to be mutated</param>
        /// <param name="random">Random number generator that is used to randomly initialize the biases and weights of a layer</param>
        internal static void MutateLayerRandomly(Layer layer, Random random)
        {
            foreach (var neuron in layer.NeuronList)
            {
                if (RandomUtils.RandomCoinFlip(random))
                    neuron.Bias = RandomUtils.GetRandomNumber(-1.0, 1.0, random);

                foreach (var dendrite in neuron.ConnectedNeurons.Where(_ => RandomUtils.RandomCoinFlip(random)))
                {
                    dendrite.Weight = RandomUtils.GetRandomNumber(-1.0, 1.0, random);
                }
            }
        }
    }
}
