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
		private Brush backgroundBrush;
		private BufferedGraphicsContext myContext;
		private BufferedGraphics myBuffer;

		public PrimaryForm()
		{
			InitializeComponent();
			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
			graphics = CreateGraphics();
			end = false;

			backgroundBrush = new SolidBrush(Color.LightGray);

			myContext = BufferedGraphicsManager.Current;
		}

		private void button1Click(object sender, EventArgs e)
		{
			if (thread != null)
			{
				end = true;
				thread.Join();
				end = false;
			}

			thread = new Thread(new ParameterizedThreadStart(Ticktock));


			i = new Interface();
			i.CreateUniformRope(80, 5, 1000, 100f);
			i.SetHardEnds(true, false);
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

		private async void Ticktock(Object param)
		{
			float dt = (float)param;
			while (!end)
			{
				i.Tick(dt);
				myBuffer = myContext.Allocate(this.CreateGraphics(),this.DisplayRectangle);

				myBuffer.Graphics.FillRectangle(backgroundBrush, 0, 0, 1000, 1000);
				i.Draw(myBuffer.Graphics,
					Color.DarkGoldenrod,
					Color.DarkRed,
					massRad: 2,
					springWidth: 2,
					offsetX: 5,
					offsetY: 100);
				myBuffer.Render();
				await Task.Delay((int)(dt*900)).ConfigureAwait(false);
			}
		}
	}
}
