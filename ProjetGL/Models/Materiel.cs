namespace ProjetGL.Models
{
	public class Materiel
	{
		public static int cp=0;
		private int materielId;
		private string marque;
		private TypeMateriel type;

		public Materiel()
		{
			cp++;
			MaterielId = cp;
			Marque = "";
			Type = TypeMateriel.Ordinateur;

		}

		public Materiel( string marque, TypeMateriel type)
		{
			cp++;
			MaterielId = cp;
			Marque = marque;
			Type = type;
		}

		public int MaterielId { get => materielId; set => materielId = value; }
		public string Marque { get => marque; set => marque = value; }
		public TypeMateriel Type { get => type; set => type = value; }

		public override string ToString()
		{
			return $"Id: {MaterielId}, Marque: {Marque}, Type: {Type}";
		}
	}
}
