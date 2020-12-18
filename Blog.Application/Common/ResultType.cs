namespace Blog.Application.Common
{
    public enum ResultType
    {
        Unknown = 0,
        Ok = 1,
        Failed = 2,
        SourceNotFound = 3,
        SourceCreated = 4,
        SourceDeleted = 5
    }
}