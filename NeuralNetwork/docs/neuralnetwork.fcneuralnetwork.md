# FCNeuralNetwork

Namespace: NeuralNetwork

Implements a fully connected neural network structure.

```csharp
public class FCNeuralNetwork
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FCNeuralNetwork](./neuralnetwork.fcneuralnetwork.md)

## Fields

### **Name**

An optional name attribute to make it easier to debug and prettier for export and import.

```csharp
public string Name;
```

### **InputLayer**

The input layer of the neural network.

```csharp
public Layer InputLayer;
```

### **HiddenLayer**

The hidden layers of the neural network.

```csharp
public List<Layer> HiddenLayer;
```

### **OutputLayer**

The output layer of the neural network.

```csharp
public Layer OutputLayer;
```

## Constructors

### **FCNeuralNetwork(Int32, Int32, Int32, Int32)**

Initializes a new instance of a fully connected neural network with the given properties.

```csharp
public FCNeuralNetwork(int inputNeuronCount, int hiddenNeuronCount, int outputNeuronCount, int hiddenLayerCount)
```

#### Parameters

`inputNeuronCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of input neurons / values

`hiddenNeuronCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of hidden neurons

`outputNeuronCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of output neurons / values

`hiddenLayerCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Sets the amount of hidden layers

#### Exceptions

[Exception](https://docs.microsoft.com/en-us/dotnet/api/system.exception)<br>

## Methods

### **Deserialize(String)**

Creates an instance of the supplied JSON representation.

```csharp
public static FCNeuralNetwork Deserialize(string objJson)
```

#### Parameters

`objJson` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
JSON Instance representation

#### Returns

[FCNeuralNetwork](./neuralnetwork.fcneuralnetwork.md)<br>
Reconstructed instance

### **Serialize(FCNeuralNetwork)**

Serializes the given instance into a json representation string.

```csharp
public static string Serialize(FCNeuralNetwork fcNeuralNetwork)
```

#### Parameters

`fcNeuralNetwork` [FCNeuralNetwork](./neuralnetwork.fcneuralnetwork.md)<br>
Instance to be serialized

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
JSON Object representation as a string

### **Clone()**

Creates a deep copy of the given neural network.

```csharp
public FCNeuralNetwork Clone()
```

#### Returns

[FCNeuralNetwork](./neuralnetwork.fcneuralnetwork.md)<br>
