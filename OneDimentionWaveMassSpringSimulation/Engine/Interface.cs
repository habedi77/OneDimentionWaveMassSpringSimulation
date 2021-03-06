﻿namespace OneDimentionWaveMassSpringSimulation.Engine.Engine
{
	using System;
	using System.Drawing;
	using System.Numerics;
	using MassSpringLibrary;

	public class Interface
	{
		private Core core;

		private int size;

		private Mass[] masses;
		private Spring[] springs;

		public int Size { get; }

		public void CreateUniformRope(int size, float gap, float constant, float strechModifier = 1)
		{
			this.size = size;
			masses = new Mass[size];
			springs = new Spring[size - 1];

			masses[0] = new Mass(1f, new Vector3(0, 0, 0), true, false, false);
			for (int i = 1; i < size; i++)
			{
				masses[i] = new Mass(1f, new Vector3(i * gap, 0, 0), true, false, true);
				springs[i - 1] = new Spring(masses[i - 1], masses[i], constant, 1 / strechModifier);
			}

			core = new Core(masses, springs);
		}

		public void Draw(Graphics graphics,
			Color massColor,
			Color springColor,
			float massRad = 15,
			float springWidth = 5,
			float offsetX = 0,
			float offsetY = 0)
		{

			using Pen springPen = new Pen(springColor, springWidth);

			for (int i = 0; i < size - 1; i++)
			{
				graphics.DrawLine(springPen,
					x1: masses[i].Position.X + offsetX,
					y1: masses[i].Position.Y + offsetY,
					x2: masses[i + 1].Position.X + offsetX,
					y2: masses[i + 1].Position.Y + offsetY);
			}

			using Brush massBrush = new SolidBrush(massColor);
			for (int i = 0; i < size; i++)
			{
				graphics.FillEllipse(
					massBrush,
					x: masses[i].Position.X - massRad + offsetX,
					y: masses[i].Position.Y - massRad + offsetY,
					width: massRad * 2,
					height: massRad * 2);
			}
		}

		// todo CreateUniformRope(int size)
		// todo CreateRope(float[] massArray)
		// todo CreateRope(string expression)?
		public void SetHardEnds(bool isLefHard, bool isRightHard)
		{
			masses[0].AxisYLock = isLefHard;
			masses[size - 1].AxisYLock = isRightHard;
		}

		// todo AddWave() ????
		[Obsolete("UI TetsOnly")]
		public void AddWave()
		{
			int len = (int)(size * 0.1f);

			for (int i = 0; i < len; i++)
			{
				//var pos = masses[i].Position;
				//pos.Y = (float)Math.Sin(i * Math.PI / len)*20;
				masses[i].Position = new Vector3(
					masses[i].Position.X,
					(float)Math.Sin(i * Math.PI / len) * 60,
				masses[i].Position.Z);
			}
		}
		// todo return Masses for external drawing methods
		public Mass[] GetMasses()
		{
			return masses;
		}

		public void Tick(float dt)
		{
			core.Tick(dt);
		}
	}
}
