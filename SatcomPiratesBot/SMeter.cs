using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SatcomPiratesBot
{
	public class SMeter : System.Windows.Forms.UserControl
	{

		int ledVal;                             // VU meter value - range 1 to 15
		int peakVal;                            // Peak value
		int ledCount = 15;                      // Number of LEDs
		int peakMsec = 1000;                    // Peak Indicator time is 1 sec.

		protected Timer timer1;                 // Timer to determine how long the peak indicator persists 

		// Array of LED colours			Unlit surround		Lit surround	Lit centre
		//													Unlit centre
		Color[] ledColours =new Color[]{Color.Black,Color.Red,      Color.White,
										Color.Black,Color.Red,      Color.White,
										Color.Black,Color.Red,      Color.White,
										Color.Black,Color.Orange,   Color.White,
										Color.Black,Color.Orange,   Color.White,
										Color.Black,Color.Orange,   Color.White,
										Color.Black,Color.Orange,   Color.White,
										Color.Black,Color.Green,    Color.White,
										Color.Black,Color.Green,    Color.White,
										Color.Black,Color.Green,    Color.White,
										Color.Black,Color.Green,    Color.White,
										Color.Black,Color.Green,    Color.White,
										Color.Black,Color.Green,    Color.White,
										Color.Black,Color.Green,    Color.White,
										Color.Black,Color.Green,    Color.White};

		public SMeter()
		{
			this.Name = "vuMeterLED";
			this.Size = new System.Drawing.Size(20, 100);       // Default size for control
			timer1 = new Timer();
			timer1.Interval = peakMsec;                         // Peak indicator time
			timer1.Enabled = false;
			timer1.Tick += new EventHandler(timer1_Tick);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			DrawLeds(g);
			DrawBorders(g);
		}

		public int Volume
		// Determine how many LEDs to light - valid range 0 - 15
		{
			get
			{
				return ledVal;
			}
			set
			{
				// Do not allow negative value
				if (value < 0)
				{
					ledVal = 0;
				}
				// Max value is 15 - anything over that, allow it but set to 15
				else if (value > 15)
				{
					ledVal = 15;
				}
				else
				{
					ledVal = value;
				}

				// New peak value
				if (ledVal > peakVal)
				{
					peakVal = ledVal;
					timer1.Enabled = true;          // Tell the peak indicator to stay
				}
				this.Invalidate();                  // Re-draw the control
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Enabled = false;                 // Tell the peak indicator to go
			peakVal = 0;                            //
			this.Invalidate();
		}

		private void DrawLeds(Graphics g)
		{
			// Rectangle values for each individual LED - fit them nicely inside the border
			int ledLeft = this.ClientRectangle.Left + 3;
			int ledTop = this.ClientRectangle.Top + 3;
			int ledWidth = this.ClientRectangle.Width - 6;
			int ledHeight = this.ClientRectangle.Height / ledCount - 2;

			// Create the LED rectangle
			Rectangle ledRect = new Rectangle(ledLeft, ledTop, ledWidth, ledHeight);

			GraphicsPath gp = new GraphicsPath();                   // Create Graphics Path
			gp.AddRectangle(ledRect);                               // Add the rectangle
			PathGradientBrush pgb = new PathGradientBrush(gp);      // Nice brush for shiny LEDs

			// Two ints in the FOR LOOP, because the graphics are offset from the top, but the LED
			// values start from the bottom...
			for (int i = 0, j = ledCount; i < ledCount; i++, j--)
			{
				// Light the LED if it's under current value, or if it's the peak value.
				if ((j <= ledVal) | (j == peakVal))
				{
					pgb.CenterColor = ledColours[i * 3 + 2];
					pgb.SurroundColors = new Color[] { ledColours[i * 3 + 1] };
				}
				// Otherwise, don't light it.
				else
				{
					pgb.CenterColor = ledColours[i * 3 + 1];
					pgb.SurroundColors = new Color[] { ledColours[i * 3] };
				}

				// Light LED fom the centre.
				pgb.CenterPoint = new PointF(ledRect.X + ledRect.Width / 2, ledRect.Y + ledRect.Height / 2);

				// Use a matrix to move the LED graphics down according to the value of i
				Matrix mx = new Matrix();
				mx.Translate(0, i * (ledHeight + 2));
				g.Transform = mx;
				g.FillRectangle(pgb, ledRect);
			}

			// Translate back to original position to draw the border
			Matrix mx1 = new Matrix();
			mx1.Translate(0, 0);
			g.Transform = mx1;

			gp.Dispose();
		}

		private void DrawBorders(Graphics g)
		{
			int PenWidth = (int)Pens.White.Width;

			// Draw the outer 3D border round the control
			//
			g.DrawLine(Pens.White,
				new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
				new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top)); //Top
			g.DrawLine(Pens.White,
				new Point(this.ClientRectangle.Left, this.ClientRectangle.Top),
				new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth)); //Left
			g.DrawLine(Pens.DarkGray,
				new Point(this.ClientRectangle.Left, this.ClientRectangle.Height - PenWidth),
				new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth)); //Bottom
			g.DrawLine(Pens.DarkGray,
				new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Top),
				new Point(this.ClientRectangle.Width - PenWidth, this.ClientRectangle.Height - PenWidth)); //Right


			// Draw the inner 3D border round the LEDs
			//

			// Set the size to fit nicely inside the control.
			int ledBorderLeft = this.ClientRectangle.Left + 2;
			int ledBorderTop = this.ClientRectangle.Top + 2;
			int ledBorderWidth = this.ClientRectangle.Width - 3;
			int ledBorderHeight = this.ClientRectangle.Height - 3;

			// Draw the border
			g.DrawLine(Pens.DarkGray, new Point(ledBorderLeft, ledBorderTop), new Point(ledBorderWidth, ledBorderTop)); //Top
			g.DrawLine(Pens.DarkGray, new Point(ledBorderLeft, ledBorderTop), new Point(ledBorderLeft, ledBorderHeight)); //Left
			g.DrawLine(Pens.White, new Point(ledBorderLeft, ledBorderHeight), new Point(ledBorderWidth, ledBorderHeight)); //Bottom
			g.DrawLine(Pens.White, new Point(ledBorderWidth, ledBorderTop), new Point(ledBorderWidth, ledBorderHeight)); //Right
																														 // Extra line to overwrite any LED which shows between the inner and outer border.
			g.DrawLine(Pens.LightGray, new Point(ledBorderLeft, ledBorderHeight + 1), new Point(ledBorderWidth, ledBorderHeight + 1));

		}
	}
}
