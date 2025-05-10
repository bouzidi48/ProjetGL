namespace ProjetNet.Data
{
	public interface InterfaceCRUD<T>
	{
		// Méthodes CRUD de base
		void Add(T entity);
		void Update(T entity);
		void Delete(int id);
		void Delete(string id);
		List<T> GetAll();
		T GetById(int id);
		T GetById(string id);
	}
}