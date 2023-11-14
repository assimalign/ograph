namespace Assimalign.OGraph;

/// <summary>
/// Represents where the error occurred. 
/// <br/>
/// <br/>
/// Errors are broken down into the following layers:
/// <list type="bullet">
///     <item><b>Model Errors:</b><i>are errors that don't meet OGraph specifications. Usually thrown at build time.</i></item>
///     <item><b>Execution Errors:</b><i>are errors that don't meet OGraph specifications. Usually thrown at build time.</i></item>
/// </list>
/// </summary>
public enum OGraphErrorType
{
    None = 0,
    /// <summary>
    /// 
    /// </summary>
    Build,
    /// <summary>
    /// Represents an error at execution
    /// </summary>
    Execution,
    /// <summary>
    /// Represents an error in the OGraph model.
    /// </summary>
    Model
}
