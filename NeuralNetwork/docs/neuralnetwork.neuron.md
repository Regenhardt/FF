# Neuron

Namespace: NeuralNetwork

Implements a neuron representation in a neural network structure that contains an initial [Neuron.Bias](./neuralnetwork.neuron.md#bias) and [Neuron.Value](./neuralnetwork.neuron.md#value) of zero and a list of [Dendrite](./neuralnetwork.dendrite.md)s which represent connected neurons in previous [Layer](./neuralnetwork.layer.md)s.

```csharp
public class Neuron
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Neuron](./neuralnetwork.neuron.md)

## Fields

### **Bias**

The bias (offset) of the neuron.

```csharp
public double Bias;
```

### **Value**

The current value of the neuron.

```csharp
public double Value;
```

### **ConnectedNeurons**

The connected neurons ([Dendrite](./neuralnetwork.dendrite.md)s) in previous layers.

```csharp
public List<Dendrite> ConnectedNeurons;
```

## Constructors

### **Neuron()**

Initializes a new instance of a neuron with a zero initialized [Neuron.Bias](./neuralnetwork.neuron.md#bias) and [Neuron.Value](./neuralnetwork.neuron.md#value) as well as an empty list of connected neurons [Dendrite](./neuralnetwork.dendrite.md)s.

```csharp
public Neuron()
```
