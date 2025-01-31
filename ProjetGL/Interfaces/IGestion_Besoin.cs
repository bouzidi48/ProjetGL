using ProjetGL.Models;

namespace ProjetGL.Interfaces
{
	public interface IGestion_Besoin
	{
		void AddBesoin(Besoin besoin);
		void DelBesoin(int id);
		bool ExistBesoin(int id);
		Besoin FindBesoin(int id);
		List<Besoin> GetBesoins();
		void UpdateBesoin(int id, Besoin newBesoin);
	}
}
