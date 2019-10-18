namespace OneDimentionWaveMassSpringSimulation.Engine
{
	using System;
	using System.Numerics;
	using MassSpringLibrary;

	public class Core
	{
		private Mass[] Masses { get; }

		/// <summary>
		/// spring[i] represens the spring between mass[i] and mass[i+1]
		/// </summary>
		public Spring[] Springs { get; }

		public Core(Mass[] masses, Spring[] springs)
		{
			this.Masses = masses ?? throw new ArgumentNullException(nameof(masses));
			this.Springs = springs ?? throw new ArgumentNullException(nameof(springs));
		}

		public void Tick(float dt)
		{
			foreach (var s in Springs)
			{
				s.AddForcesToMasses();
			}

			foreach (var m in Masses)
			{
				m.UpdateAcceleration(resetForce:true);
				m.UpdateSpeed(dt);
				m.UpdatePostion(dt);
			}
		}
	}
}
