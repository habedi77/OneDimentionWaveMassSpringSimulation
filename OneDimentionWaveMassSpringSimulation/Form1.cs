namespace OneDimentionWaveMassSpringSimulation
{
	using System;
	using System.Drawing;
	using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
	using OneDimentionWaveMassSpringSimulation.Engine.Engine;

	public partial class PrimaryForm : Form
	{
		private Interface i;
		private Thread thread;
		private Graphics graphics;
		private bool end;

		public PrimaryForm()
		{
			InitializeComponent();
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			graphics = CreateGraphics();
			end = false;
		}

		private void button1Click(object sender, EventArgs e)
		{
			if (thread != null)
			{
				end = true;
				thread.Join();
				end = false;
			}

			thread = new Thread(new ParameterizedThreadStart(ticktock));


			i = new Interface();
			i.CreateUniformRope(80, 5, 1000, 100f);
			i.SetEnds(false, true);
			i.AddWave();


			thread.Start(0.01f);
		}

		private void PrimaryFormFormClosing(object sender, FormClosingEventArgs e)
		{
			end = true;
			thread.Join();
			System.Console.WriteLine("Thrad done: " + thread);
			graphics.Dispose();
		}

		private async void ticktock(Object param)
		{
			float dt = (float)param;
			while (!end)
			{
				i.Tick(dt);

				graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, 600, 300);
				i.Draw(graphics,
					Color.DarkGoldenrod,
					Color.DarkRed,
					massRad: 0,
					springWidth: 2,
					offsetX: 5,
					offsetY: 100);
				//await Task.Delay((int)(5));
			}
		}
	}
}
