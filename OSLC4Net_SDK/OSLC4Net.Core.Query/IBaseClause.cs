namespace OSLC4Net.Core.Query
{
    public interface IBaseClause
    {
        bool IsError { get; }
        string ErrorReason { get; }
    }
}
