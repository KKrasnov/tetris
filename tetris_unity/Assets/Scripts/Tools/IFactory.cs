namespace Tools
{
    public interface IFactory<T>
    {
        T GetNext();
    }
}