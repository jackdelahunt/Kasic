namespace kasic.Kasic
{
    public interface IReturnObject
    {
        double AsNumber();
        bool AsBool();
        string AsString();
        string AsAny();
    }
}