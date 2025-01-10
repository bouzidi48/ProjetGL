namespace ProjetGL.Data
{
    public interface IData
    {
        void Add();
        void Delete();
        bool Exist();
        void Find(string login);
        void GetAccounts();
        void Update();
    }
}