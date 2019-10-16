namespace OneDimentionWaveMassSpringSimulation
{
	using System;
	using System.Drawing;
	using System.Threading;
	using System.Windows.Forms;
	using OneDimentionWaveMassSpringSimulation.Engine.Engine;

	public partial class PrimaryForm : Form
	{

		private Interface i;
		private Thread thread;

		public PrimaryForm()
		{
			InitializeComponent();
		}

		private void button1Click(object sender, EventArgs e)
		{
			if (thread != null)
			{
				thread.Abort();
			}

			thread = new Thread(new ParameterizedThreadStart(ticktock));


			i = new Interface();
			i.CreateUniformRope(100, 9, -100, 200f);
			i.SetEnds(true, true);
			i.AddWave();


			thread.Start(0.001f);
		}

		private void ticktock(Object param)
		{
			float dt = (float)param;
			while (true)
			{
				i.tick(dt);
				var gp = CreateGraphics();
				gp.FillRectangle(new SolidBrush(Color.White), 0, 0, 1000, 1000);
				i.Draw(gp,
					Color.DarkGoldenrod,
					Color.DarkRed,
					massRad: 2,
					springWidth: 1,
					offsetX: 5,
					offsetY: 100);
				gp.Dispose();
				Thread.Sleep((int)(dt * 1000));
			}
		}
	}
}
