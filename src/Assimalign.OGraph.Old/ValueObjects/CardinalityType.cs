namespace Assimalign.OGraph;

public enum CardinalityType
{
    Unknown = 0,
    OneToOne = 1,
    OneToMany = 2,
    // TODO: Revisit one-to-many cardinality
    //ManyToMany - Not Supported right now
}
