# Genome

Namespace: NeuralNetwork.EvolutionaryAlgorithm

Implements a genome representation that encapsulates a fully connect neural network ([FCNeuralNetwork](./neuralnetwork.fcneuralnetwork.md)) combined with a tracked fitness ([Genome.Fitness](./neuralnetwork.evolutionaryalgorithm.genome.md#fitness)).

```csharp
public class Genome
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Genome](./neuralnetwork.evolutionaryalgorithm.genome.md)

## Fields

### **Fitness**

Genome / neural network fitness.

```csharp
public double Fitness;
```

## Constructors

### **Genome(Int32, Int32, Int32, Int32, Random)**

Initializes a new instance of a genome ([FCNeuralNetwork](./neuralnetwork.fcneuralnetwork.md)) with the given properties.

```csharp
public Genome(int inputCount, int hiddenNeuronCount, int outputCount, int hiddenLayerCount, Random random)
```

#### Parameters

`inputCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of input neurons / values for the genome

`hiddenNeuronCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of hidden neurons for the genome

`outputCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of output neurons / values for the genome

`hiddenLayerCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of hidden layers for the genome

`random` [Random](https://docs.microsoft.com/en-us/dotnet/api/system.random)<br>
Random number generator that is used to randomly populate the weights and biases

### **Genome(Genome)**

Initializes a deep copy of the given genomes neural network while initializing the fitness with zero.

```csharp
public Genome(Genome copy)
```

#### Parameters

`copy` [Genome](./neuralnetwork.evolutionaryalgorithm.genome.md)<br>
Genome to be copied

## Methods

### **RandomlyInheritGenes(Genome, Random)**

Randomly inherits (changes) the biases and weights with the ones from the given genome.

```csharp
public Genome RandomlyInheritGenes(Genome otherGenome, Random random)
```

#### Parameters

`otherGenome` [Genome](./neuralnetwork.evolutionaryalgorithm.genome.md)<br>
Bias and weight genome source

`random` [Random](https://docs.microsoft.com/en-us/dotnet/api/system.random)<br>
Random number generator that is used to randomly determine the biases and weights which should be inherited

#### Returns

[Genome](./neuralnetwork.evolutionaryalgorithm.genome.md)<br>
Target genome for daisy chaining operations

**Remarks:**

Source and target genome must have the same topology!

### **MutateRandomly(Random)**

Mutates biases and weights randomly with random values.

```csharp
public Genome MutateRandomly(Random random)
```

#### Parameters

`random` [Random](https://docs.microsoft.com/en-us/dotnet/api/system.random)<br>
Random number generator that is used to randomly determine the biases and weights which should be mutated and if so to generate their new values

#### Returns

[Genome](./neuralnetwork.evolutionaryalgorithm.genome.md)<br>
Target genome for daisy chaining operations

### **Process(Double[])**

Processes an input through the internal neural network.

```csharp
public Double[] Process(Double[] inputs)
```

#### Parameters

`inputs` [Double[]](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
Input values for the neural network

#### Returns

[Double[]](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
Output layer values
