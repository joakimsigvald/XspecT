﻿namespace XspecT.Test.Given;

public record MyModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<int> Values { get; set; }
}