namespace XspecT;

/// <summary>
/// A tag is used to reference or set a value of a given type in the test pipeline.
/// It is similar to A, The, AFirst etc. but with the difference that several instances with 
/// the same type can be differentiated and referred to by the tag instead of by order number.
/// Example: Instead of using `AFirst`, `ASecond` and `AThird` to reference three different ints,
/// you can declare three tags, such as `age`, `length` and `size` and reference the values using 
/// `The(age)`, `The(length)` and `The(size)`.
/// </summary>
/// <typeparam name="TValue">The type of the value to get or set using the tag</typeparam>
public class Tag<TValue>();