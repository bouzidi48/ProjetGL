using System.ComponentModel.DataAnnotations;

namespace ProjetGL.Models
{
	public class Besoin
	{
		public static int cp = 0;
		private int id;
		private List<Materiel> materiels;
		private Departement departement;
		private Enseignant enseignant;
		private int envoyer=0;


		public int Id { get => id; set => id = value; }
		public List<Materiel> Materiels { get => materiels; set => materiels = value; }
		public Departement Departement { get => departement; set => departement = value; }
		public Enseignant Enseignant { get => enseignant; set => enseignant = value; }
		public int Envoyer { get => envoyer; set => envoyer = value; }

		public Besoin(List<Materiel> materiels, Departement departement, Enseignant enseignant)
		{
			cp = cp + 1;
			Id = cp;
			Materiels = materiels;
			Departement = departement;
			Enseignant = enseignant;
		}

	}
}
