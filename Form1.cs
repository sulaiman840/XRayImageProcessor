using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace XRayImageProcessor
{
    public partial class Form1 : Form
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;

        private PictureBox pictureBoxInput;
        private PictureBox pictureBoxOutput;

        private Button btnSelectArea;
        private Button btnApplyColor;

        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        private bool isDragging = false;
        private Point startPoint = new Point();
        private Rectangle rect = new Rectangle();

        private ComboBox cmbColormap;

        public Form1()
        {
            InitializeComponent1();

            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();

            pictureBoxInput.MouseDown += new MouseEventHandler(pictureBox_MouseDown);
            pictureBoxInput.MouseMove += new MouseEventHandler(pictureBox_MouseMove);
            pictureBoxInput.MouseUp += new MouseEventHandler(pictureBox_MouseUp);

            pictureBoxInput.Paint += new PaintEventHandler(pictureBoxInput_Paint);
            pictureBoxOutput.Paint += new PaintEventHandler(pictureBoxOutput_Paint);
        }

        private void InitializeComponent1()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxInput = new System.Windows.Forms.PictureBox();
            this.pictureBoxOutput = new System.Windows.Forms.PictureBox();
            this.btnSelectArea = new System.Windows.Forms.Button();
            this.btnApplyColor = new System.Windows.Forms.Button();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOutput)).BeginInit();
            this.SuspendLayout();

            this.cmbColormap = new System.Windows.Forms.ComboBox();
            this.cmbColormap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColormap.Items.AddRange(new object[] {
    "Default",
    "HeatMap",
    "CoolWarm",
    "Viridis",
    "PlasmaColor",
    "SunsetColor",
    "AuroraColor"
            });

            this.cmbColormap.Location = new System.Drawing.Point(252, 440);
            this.cmbColormap.Name = "cmbColormap";
            this.cmbColormap.Size = new System.Drawing.Size(110, 21);
            this.cmbColormap.SelectedIndex = 0;
            this.Controls.Add(this.cmbColormap);

            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem
            });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1600, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem
            });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";

            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadImage);

            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveImage);

            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitApplication);

            this.pictureBoxInput.Location = new System.Drawing.Point(12, 50);
            this.pictureBoxInput.Name = "pictureBoxInput";
            this.pictureBoxInput.Size = new System.Drawing.Size(760, 380);
            this.pictureBoxInput.TabStop = false;

            this.pictureBoxOutput.Location = new System.Drawing.Point(828, 50);
            this.pictureBoxOutput.Name = "pictureBoxOutput";
            this.pictureBoxOutput.Size = new System.Drawing.Size(760, 380);
            this.pictureBoxOutput.TabStop = false;

            this.btnSelectArea.Location = new System.Drawing.Point(12, 440);
            this.btnSelectArea.Name = "btnSelectArea";
            this.btnSelectArea.Size = new System.Drawing.Size(110, 30);
            this.btnSelectArea.TabIndex = 2;
            this.btnSelectArea.Text = "Select Area";
            this.btnSelectArea.UseVisualStyleBackColor = true;

            this.btnApplyColor.Location = new System.Drawing.Point(132, 440);
            this.btnApplyColor.Name = "btnApplyColor";
            this.btnApplyColor.Size = new System.Drawing.Size(110, 30);
            this.btnApplyColor.TabIndex = 3;
            this.btnApplyColor.Text = "Apply Color";
            this.btnApplyColor.UseVisualStyleBackColor = true;
            this.btnApplyColor.Click += new System.EventHandler(this.ApplyColor);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6, 13);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 480);
            this.Controls.Add(this.btnApplyColor);
            this.Controls.Add(this.btnSelectArea);
            this.Controls.Add(this.pictureBoxOutput);
            this.Controls.Add(this.pictureBoxInput);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "X-Ray Image Processor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOutput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void pictureBoxInput_Paint(object sender, PaintEventArgs e)
        {
            if (rect != Rectangle.Empty)
            {
                e.Graphics.DrawRectangle(Pens.Red, rect);
            }
        }

        private void pictureBoxOutput_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBoxOutput.Image != null)
            {
                e.Graphics.DrawImage(pictureBoxOutput.Image, new Point(0, 0));
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = e.Location;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                rect = new Rectangle(
                    Math.Min(startPoint.X, e.X),
                    Math.Min(startPoint.Y, e.Y),
                    Math.Abs(e.X - startPoint.X),
                    Math.Abs(e.Y - startPoint.Y));

                pictureBoxInput.Invalidate();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
            pictureBoxInput.Invalidate();
        }

        private void LoadImage(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.bmp) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.bmp";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBoxInput.Image = Image.FromFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void SaveImage(object sender, EventArgs e)
        {
            if (pictureBoxOutput.Image != null) 
            {
                saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png"; 
                saveFileDialog.Title = "Save an Image File"; 
                saveFileDialog.ShowDialog(); 

                if (saveFileDialog.FileName != "") 
                {
                    using (System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile()) 
                    {
                        switch (saveFileDialog.FilterIndex) 
                        {
                            case 1:
                                pictureBoxOutput.Image.Save(fs, ImageFormat.Jpeg); 
                                break;
                            case 2:
                                pictureBoxOutput.Image.Save(fs, ImageFormat.Bmp); 
                                break;
                            case 3:
                                pictureBoxOutput.Image.Save(fs, ImageFormat.Gif); 
                                break;
                            case 4:
                                pictureBoxOutput.Image.Save(fs, ImageFormat.Png);
                                break;
                        }
                        fs.Close(); 
                    }
                }
            }
            else
            {
                MessageBox.Show("There is no image to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // ⁄—÷ —”«·… Œÿ√ ≈–« ·„  ﬂ‰ Â‰«ﬂ ’Ê—… ·Õ›ŸÂ«
            }
        }






        private void ExitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void ApplyColor(object sender, EventArgs e)
        {
     
            if (pictureBoxInput.Image != null && rect != Rectangle.Empty)
            {
            
                Bitmap inputBitmap = new Bitmap(pictureBoxInput.Image);
               
                Bitmap outputBitmap = new Bitmap(inputBitmap.Width, inputBitmap.Height);

               
                Func<double, Color> colormap = DefaultMap;
               
                switch (cmbColormap.SelectedItem?.ToString())
                {
                    case "HeatMap":
                        colormap = HeatMapColor; 
                        break;
                    case "CoolWarm":
                        colormap = CoolWarmMap; 
                        break;
                    case "Viridis":
                        colormap = ViridisColor;
                        break;
                    case "PlasmaColor":
                        colormap = PlasmaColor; 
                        break;

                    case "SunsetColor":
                        colormap = SunsetColor; 
                        break;

                    case "AuroraColor":
                        colormap = AuroraColor; 
                        break;
                    default:
                        colormap = DefaultMap; 
                        break;
                }

                for (int i = rect.Left; i < rect.Right; i++)
                {
                    for (int j = rect.Top; j < rect.Bottom; j++)
                    {
                        Color originalColor = inputBitmap.GetPixel(i, j);
                        double grayScale = ((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11)) / 255.0;
                        Color mappedColor = colormap(grayScale);
                        outputBitmap.SetPixel(i, j, mappedColor);
                    }
                }

                for (int i = 0; i < inputBitmap.Width; i++)
                {
                    for (int j = 0; j < inputBitmap.Height; j++)
                    {
                        if (i < rect.Left || i >= rect.Right || j < rect.Top || j >= rect.Bottom)
                        {
                            outputBitmap.SetPixel(i, j, inputBitmap.GetPixel(i, j));
                        }
                    }
                }

                pictureBoxOutput.Image = outputBitmap;
                pictureBoxOutput.Invalidate();
            }
        }

        private Color DefaultMap(double value)
        {
            int redIntensity = (int)(255 * value);
            return Color.FromArgb(255, redIntensity, 0, 0);
        }


        private Color HeatMapColor(double value)
        {
            return Color.FromArgb(255, (int)(255 * value), (int)(255 * value * 0.5), 0);
        }

        private Color CoolWarmMap(double value)
        {
            return Color.FromArgb(255, (int)(255 * value), 0, (int)(255 * (1 - value)));
        }


        private Color ViridisColor(double value)
        {


          


            value = Math.Clamp(value, 0.0, 1.0);

     


            byte[][] viridisData = new byte[][]
            {
        new byte[] { 68, 1, 84 },
        new byte[] { 71, 44, 122 },
        new byte[] { 59, 81, 139 },
        new byte[] { 44, 113, 142 },
        new byte[] { 33, 144, 141 },
        new byte[] { 39, 173, 129 },
        new byte[] { 92, 200, 99 },
        new byte[] { 170, 220, 50 },
        new byte[] { 253, 231, 37 }
            };


           
            int index = (int)(value * (viridisData.Length - 1));


           
            double fractional = (value * (viridisData.Length - 1)) - index;


            byte r = (byte)(viridisData[index][0] + fractional * (viridisData[index + 1][0] - viridisData[index][0]));
            byte g = (byte)(viridisData[index][1] + fractional * (viridisData[index + 1][1] - viridisData[index][1]));
            byte b = (byte)(viridisData[index][2] + fractional * (viridisData[index + 1][2] - viridisData[index][2]));

            return Color.FromArgb(255, r, g, b);
        }

        private Color PlasmaColor(double value)
        {
            value = Math.Clamp(value, 0.0, 1.0);

            byte[][] plasmaData = new byte[][]
            {
        new byte[] {12, 7, 134},//blue
        new byte[] {62, 7, 156},//violet
        new byte[] {107, 2, 164},//darker violet.
        new byte[] {152, 0, 157},//purple
        new byte[] {195, 0, 142},//magenta
        new byte[] {232, 3, 124},// deep pink.
        new byte[] {255, 48, 100},//pink
        new byte[] {255, 113, 84},//shade
        new byte[] {241, 175, 74},//bright lemon yellow
        new byte[] {251, 241, 55}//purple
            };


            int index = (int)(value * (plasmaData.Length - 1));
            double fractional = (value * (plasmaData.Length - 1)) - index;


            byte r = (byte)(plasmaData[index][0] + fractional * (plasmaData[index + 1][0] - plasmaData[index][0]));
            byte g = (byte)(plasmaData[index][1] + fractional * (plasmaData[index + 1][1] - plasmaData[index][1]));
            byte b = (byte)(plasmaData[index][2] + fractional * (plasmaData[index + 1][2] - plasmaData[index][2]));

            return Color.FromArgb(255, r, g, b);
        }

        private Color SunsetColor(double value)
        {
            value = Math.Clamp(value, 0.0, 1.0);

            byte[][] sunsetData = new byte[][]
            {
        new byte[] {24, 5, 109},   // Deep blue
        new byte[] {46, 20, 141},  // Royal blue
        new byte[] {75, 35, 161},  // Indigo
        new byte[] {113, 45, 164}, // Purple blue
        new byte[] {158, 63, 148}, // Purple pink
        new byte[] {205, 92, 119}, // Reddish pink
        new byte[] {240, 131, 85}, // Orange
        new byte[] {254, 185, 54}, // Golden yellow
        new byte[] {255, 241, 25}  // Bright yellow
            };

            int index = (int)(value * (sunsetData.Length - 1));
            double fractional = (value * (sunsetData.Length - 1)) - index;

            byte r = (byte)(sunsetData[index][0] + fractional * (sunsetData[index + 1][0] - sunsetData[index][0]));
            byte g = (byte)(sunsetData[index][1] + fractional * (sunsetData[index + 1][1] - sunsetData[index][1]));
            byte b = (byte)(sunsetData[index][2] + fractional * (sunsetData[index + 1][2] - sunsetData[index][2]));

            return Color.FromArgb(255, r, g, b);
        }

        private Color AuroraColor(double value)
        {
            value = Math.Clamp(value, 0.0, 1.0);


            byte[][] auroraData = new byte[][]
            {
        new byte[] {5, 2, 68},     // Dark indigo
        new byte[] {8, 64, 129},   // Deep ocean blue
        new byte[] {14, 127, 168}, // Bright cerulean
        new byte[] {22, 183, 178}, // Soft turquoise
        new byte[] {66, 220, 163}, // Light sea green
        new byte[] {138, 230, 139},// Greenish yellow
        new byte[] {212, 225, 112},// Lime green
        new byte[] {253, 198, 102},// Golden yellow
        new byte[] {255, 128, 130} // Soft pink
            };


            int index = (int)(value * (auroraData.Length - 1));
            double fractional = (value * (auroraData.Length - 1)) - index;


            byte r = (byte)(auroraData[index][0] + fractional * (auroraData[index + 1][0] - auroraData[index][0]));
            byte g = (byte)(auroraData[index][1] + fractional * (auroraData[index + 1][1] - auroraData[index][1]));
            byte b = (byte)(auroraData[index][2] + fractional * (auroraData[index + 1][2] - auroraData[index][2]));

            return Color.FromArgb(255, r, g, b);
        }

    }
}
