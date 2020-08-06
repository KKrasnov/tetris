namespace UI.MVC
{
    public interface IUIManager
    {
        void OpenWindow(string id)//I prefer to stick to string identifiers instead of enums but I have no strong opinion on it yet.
            ;

        void CloseWindow(string id);
    }
}