namespace Assimalign.OGraph.Gdm;

public enum GdmErrorCode
{
    /// <summary>
    /// Unknown error.
    /// </summary>
    GDM0000,

    #region Type Errors - 0###
    //      Primitive Type Errors   - 01##,
    GDM0101,

    //      Enum Type Errors        - 02##,
    GDM0201,

    //      Complex Type Errors     - 03##
    /// <summary>
    /// Key disallowed. Complex types cannot have a key.
    /// </summary>
    GDM0301,

    //      Entity Type Errors      - 04##,
    /// <summary>
    /// No key defined. A key is required for an entity type.
    /// </summary>
    GDM0401,

    //      Collection Type Errors  - 05##,
    GDM0501,
    #endregion

    #region Vertex Errors - 1###
    /// <summary>
    /// Invalid type reference error. Only Entity Types can be referenced on vertices.
    /// </summary>
    GDM1001,
    /// <summary>
    /// Invalid type reference error. Type reference cannot be null.
    /// </summary>
    GDM1002,

    #endregion

    #region Edge Errors - 2###


    #endregion

    #region Serialization Errors - 3###
    /// <summary>
    /// Invalid content error. Unexpected value disallowed.
    /// </summary>
    GDM3001,

    #endregion
    /* GDM5000 is a custom error to be used for 
     
     */
    /// <summary>
    /// Custom model validation
    /// </summary>
    GDM5000,
    /// <summary>
    /// Invalid label. Allowed characters: [a-z, A-Z, 0-9, _, @].
    /// </summary>
    GDM5001,
}