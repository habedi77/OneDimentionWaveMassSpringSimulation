namespace OneDimentionWaveMassSpringSimulation.Engine
{
	using System;
	using System.Collections.Generic;
	using System.Text;
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
				s.Update();
			}

			foreach (var m in Masses)
			{
				m.UpdateAcceleration();
				m.UpdateSpeed(dt);
				m.UpdatePostion(dt);
			}
		}
	}
}
