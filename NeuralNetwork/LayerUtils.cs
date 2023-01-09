using System;
using System.Linq;
using Utils;

namespace NeuralNetwork
{
    internal static class LayerUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcLayer"></param>
        /// <param name="targetLayer"></param>
        /// <param name="random"></param>
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
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="random"></param>
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
        /// 
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="random"></param>
        internal static void MutateLayerRandomly(Layer layer, Random random)
        {
            foreach (var neuron in layer.NeuronList)
            {
                if (RandomUtils.RandomCoinFlip(random))
                    neuron.Bias = RandomUtils.GetRandomNumber(-1.0, 1.0, random);
                foreach (var dendrite in neuron.ConnectedNeurons.Where(dendrite => RandomUtils.RandomCoinFlip(random)))
                {
                    dendrite.Weight = RandomUtils.GetRandomNumber(-1.0, 1.0, random);
                }
            }
        }
    }
}
