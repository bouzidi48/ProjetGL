using System.Runtime.Intrinsics.Arm;

namespace ProjetGL.Models
{
	public class Ordinateur : Materiel
	{
		private static int cp = 0;
		private int ordinateurId;
		private string ram;
		private string cpu;
		private string disqueDur;
		private string ecran;

		public int OrdinateurId { get => ordinateurId; set => ordinateurId = value; }
		public string Ram { get => ram; set => ram = value; }
		public string Cpu { get => cpu; set => cpu = value; }
		public string DisqueDur { get => disqueDur; set => disqueDur = value; }
		public string Ecran { get => ecran; set => ecran = value; }

		public Ordinateur() : base()
		{
			cp++;
			OrdinateurId = cp;
			Ram = "";
			Cpu = "";
			DisqueDur = "";
			Ecran = "";
		}

		public Ordinateur(int ordinateurId, string ram, string cpu, string disqueDur, string ecran, string marque, TypeMateriel type) : base(marque, type)
		{
			OrdinateurId = ordinateurId;
			Ram = ram;
			Cpu = cpu;
			DisqueDur = disqueDur;
			Ecran = ecran;
		}

		public override string ToString()
		{
			return base.ToString() + $", CPU: {Cpu}, RAM: {Ram}, DisqueDur: {DisqueDur}, Ecran: {Ecran}";
		}
	}
}
