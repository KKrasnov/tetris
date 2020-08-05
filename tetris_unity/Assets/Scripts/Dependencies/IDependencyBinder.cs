namespace Dependencies
{
    public interface IDependencyBinder
    {
        void ConfigureDependencies(DependencyResolver dependencies);
    }
}