namespace CodePad.Shared;
using System;

public interface IMetaData
{
    string? PropertyName { get; }
    string? FormatString { get; set; }
    bool DoNotFormat => string.IsNullOrEmpty(FormatString);
    string? SortExpression { get; set; }
    bool DoNotSort => string.IsNullOrEmpty(FormatString);
    bool IsHidden { get; set; }
    bool IsReadOnly { get; set; }
    int NumberOfDecimalPlaces { get; set; }
    string? List { get; set; }
    bool IsValid { get; set; }
    bool ValidationMessage { get; set; }
    int? Minimum { get; set; }
    int? Maximum { get; set; }
    bool IsRequired { get; set; }

}
public class MetaData : IMetaData
{
    public MetaDataDisplayFormat DisplayFormat { get; set; }
    public MetaDataDataType DataType { get; set; }
    public string? FormatString { get; set; }
    public string? SortExpression { get; set; }
    public bool IsHidden { get; set; }
    public bool IsReadOnly { get; set; }
    public int NumberOfDecimalPlaces { get; set; }
    public string? List { get; set; }
    public string? PropertyName { get; }
    public bool IsValid { get; set; }
    public bool ValidationMessage { get; set; }
    public int? Minimum { get; set; }
    public int? Maximum { get; set; }
    public bool IsRequired { get; set; }
}
public class MetaDataAttribute : Attribute, IMetaData
{
    public string? FormatString { get; set; }
    public string? SortExpression { get; set; }
    public bool IsHidden { get; set; }
    public bool IsReadOnly { get; set; }
    public int NumberOfDecimalPlaces { get; set; }
    public string? List { get; set; }
    public string? PropertyName { get; }
    public bool IsValid { get; set; }
    public bool ValidationMessage { get; set; }
    public int? Minimum { get; set; }
    public int? Maximum { get; set; }
    public bool IsRequired { get; set; }
}

public enum MetaDataDataType
{
    NotSet = 0,
    Text = 10,
    Date = 20,
    Int = 30,
    Real = 40,
    Bool = 50,
}
public enum MetaDataDisplayFormat
{
    NotSet = 0,
    Text = 10,
    Date = 20,
    DateTime = 21,
    Time = 22,
    Int = 30,
    Real = 40,
    Currency = 41,
    Bool = 50,
    PhoneNumber = 100
}
