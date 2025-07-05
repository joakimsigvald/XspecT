namespace XspecT;

/// <summary>
/// A tag is an object that can be associated with a value of any type, given by the generic parameter.
/// The associated value can be defined and/or referenced in a similar manner to A, The, AFirst, ASecond etc.
/// Tags are useful to make tests more expressive and readable.
/// Example: Instead of using `AFirst`, `ASecond` and `AThird` to reference three different ints,
/// you can declare three tags, such as `age`, `length` and `size` and reference the values using 
/// `The(age)`, `The(length)` and `The(size)`.
/// </summary>
/// <typeparam name="TValue">The type of the value associated to the tag</typeparam>
public class Tag<TValue>();