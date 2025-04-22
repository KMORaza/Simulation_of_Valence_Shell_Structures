using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ValenceShellSimulator
{
    public partial class Form1 : Form
    {
        private ComboBox elementComboBox;
        private Panel shellPanel;
        private Label infoLabel;
        private Button drawButton;
        private Label detailsLabel;

        private readonly Color DarkBackground = Color.Black;
        private readonly Color DarkPanel = Color.Black;
        private readonly Color ElectronColor = Color.Yellow; 
        private readonly Color TextColor = Color.White;
        private readonly Color OrbitColor = Color.White; 
        private readonly Color NucleusColor = Color.OrangeRed; 

        private readonly Dictionary<string, Tuple<string, int, int[], string, string, int>> elements = new Dictionary<string, Tuple<string, int, int[], string, string, int>>()
        {
            {"Hydrogen", Tuple.Create("H", 1, new int[] {1}, "Nonmetal", "1", 1)},
            {"Helium", Tuple.Create("He", 2, new int[] {2}, "Noble Gas", "18", 1)},
            {"Lithium", Tuple.Create("Li", 3, new int[] {2,1}, "Alkali Metal", "1", 2)},
            {"Beryllium", Tuple.Create("Be", 4, new int[] {2,2}, "Alkaline Earth", "2", 2)},
            {"Boron", Tuple.Create("B", 5, new int[] {2,3}, "Metalloid", "13", 2)},
            {"Carbon", Tuple.Create("C", 6, new int[] {2,4}, "Nonmetal", "14", 2)},
            {"Nitrogen", Tuple.Create("N", 7, new int[] {2,5}, "Nonmetal", "15", 2)},
            {"Oxygen", Tuple.Create("O", 8, new int[] {2,6}, "Nonmetal", "16", 2)},
            {"Fluorine", Tuple.Create("F", 9, new int[] {2,7}, "Halogen", "17", 2)},
            {"Neon", Tuple.Create("Ne", 10, new int[] {2,8}, "Noble Gas", "18", 2)},
            {"Sodium", Tuple.Create("Na", 11, new int[] {2,8,1}, "Alkali Metal", "1", 3)},
            {"Magnesium", Tuple.Create("Mg", 12, new int[] {2,8,2}, "Alkaline Earth", "2", 3)},
            {"Aluminum", Tuple.Create("Al", 13, new int[] {2,8,3}, "Post-Transition", "13", 3)},
            {"Silicon", Tuple.Create("Si", 14, new int[] {2,8,4}, "Metalloid", "14", 3)},
            {"Phosphorus", Tuple.Create("P", 15, new int[] {2,8,5}, "Nonmetal", "15", 3)},
            {"Sulfur", Tuple.Create("S", 16, new int[] {2,8,6}, "Nonmetal", "16", 3)},
            {"Chlorine", Tuple.Create("Cl", 17, new int[] {2,8,7}, "Halogen", "17", 3)},
            {"Argon", Tuple.Create("Ar", 18, new int[] {2,8,8}, "Noble Gas", "18", 3)},
            {"Potassium", Tuple.Create("K", 19, new int[] {2,8,8,1}, "Alkali Metal", "1", 4)},
            {"Calcium", Tuple.Create("Ca", 20, new int[] {2,8,8,2}, "Alkaline Earth", "2", 4)},
            {"Scandium", Tuple.Create("Sc", 21, new int[] {2,8,9,2}, "Transition Metal", "3", 4)},
            {"Titanium", Tuple.Create("Ti", 22, new int[] {2,8,10,2}, "Transition Metal", "4", 4)},
            {"Vanadium", Tuple.Create("V", 23, new int[] {2,8,11,2}, "Transition Metal", "5", 4)},
            {"Chromium", Tuple.Create("Cr", 24, new int[] {2,8,13,1}, "Transition Metal", "6", 4)},
            {"Manganese", Tuple.Create("Mn", 25, new int[] {2,8,13,2}, "Transition Metal", "7", 4)},
            {"Iron", Tuple.Create("Fe", 26, new int[] {2,8,14,2}, "Transition Metal", "8", 4)},
            {"Cobalt", Tuple.Create("Co", 27, new int[] {2,8,15,2}, "Transition Metal", "9", 4)},
            {"Nickel", Tuple.Create("Ni", 28, new int[] {2,8,16,2}, "Transition Metal", "10", 4)},
            {"Copper", Tuple.Create("Cu", 29, new int[] {2,8,18,1}, "Transition Metal", "11", 4)},
            {"Zinc", Tuple.Create("Zn", 30, new int[] {2,8,18,2}, "Transition Metal", "12", 4)},
            {"Gallium", Tuple.Create("Ga", 31, new int[] {2,8,18,3}, "Post-Transition", "13", 4)},
            {"Germanium", Tuple.Create("Ge", 32, new int[] {2,8,18,4}, "Metalloid", "14", 4)},
            {"Arsenic", Tuple.Create("As", 33, new int[] {2,8,18,5}, "Metalloid", "15", 4)},
            {"Selenium", Tuple.Create("Se", 34, new int[] {2,8,18,6}, "Nonmetal", "16", 4)},
            {"Bromine", Tuple.Create("Br", 35, new int[] {2,8,18,7}, "Halogen", "17", 4)},
            {"Krypton", Tuple.Create("Kr", 36, new int[] {2,8,18,8}, "Noble Gas", "18", 4)},
            {"Rubidium", Tuple.Create("Rb", 37, new int[] {2,8,18,8,1}, "Alkali Metal", "1", 5)},
            {"Strontium", Tuple.Create("Sr", 38, new int[] {2,8,18,8,2}, "Alkaline Earth", "2", 5)},
            {"Yttrium", Tuple.Create("Y", 39, new int[] {2,8,18,9,2}, "Transition Metal", "3", 5)},
            {"Zirconium", Tuple.Create("Zr", 40, new int[] {2,8,18,10,2}, "Transition Metal", "4", 5)},
            {"Niobium", Tuple.Create("Nb", 41, new int[] {2,8,18,12,1}, "Transition Metal", "5", 5)},
            {"Molybdenum", Tuple.Create("Mo", 42, new int[] {2,8,18,13,1}, "Transition Metal", "6", 5)},
            {"Technetium", Tuple.Create("Tc", 43, new int[] {2,8,18,13,2}, "Transition Metal", "7", 5)},
            {"Ruthenium", Tuple.Create("Ru", 44, new int[] {2,8,18,15,1}, "Transition Metal", "8", 5)},
            {"Rhodium", Tuple.Create("Rh", 45, new int[] {2,8,18,16,1}, "Transition Metal", "9", 5)},
            {"Palladium", Tuple.Create("Pd", 46, new int[] {2,8,18,18,0}, "Transition Metal", "10", 5)},
            {"Silver", Tuple.Create("Ag", 47, new int[] {2,8,18,18,1}, "Transition Metal", "11", 5)},
            {"Cadmium", Tuple.Create("Cd", 48, new int[] {2,8,18,18,2}, "Transition Metal", "12", 5)},
            {"Indium", Tuple.Create("In", 49, new int[] {2,8,18,18,3}, "Post-Transition", "13", 5)},
            {"Tin", Tuple.Create("Sn", 50, new int[] {2,8,18,18,4}, "Post-Transition", "14", 5)},
            {"Antimony", Tuple.Create("Sb", 51, new int[] {2,8,18,18,5}, "Metalloid", "15", 5)},
            {"Tellurium", Tuple.Create("Te", 52, new int[] {2,8,18,18,6}, "Metalloid", "16", 5)},
            {"Iodine", Tuple.Create("I", 53, new int[] {2,8,18,18,7}, "Halogen", "17", 5)},
            {"Xenon", Tuple.Create("Xe", 54, new int[] {2,8,18,18,8}, "Noble Gas", "18", 5)},
            {"Cesium", Tuple.Create("Cs", 55, new int[] {2,8,18,18,8,1}, "Alkali Metal", "1", 6)},
            {"Barium", Tuple.Create("Ba", 56, new int[] {2,8,18,18,8,2}, "Alkaline Earth", "2", 6)},
            {"Lanthanum", Tuple.Create("La", 57, new int[] {2,8,18,18,9,2}, "Lanthanide", "3", 6)},
            {"Cerium", Tuple.Create("Ce", 58, new int[] {2,8,18,20,8,2}, "Lanthanide", "3", 6)},
            {"Praseodymium", Tuple.Create("Pr", 59, new int[] {2,8,18,21,8,2}, "Lanthanide", "3", 6)},
            {"Neodymium", Tuple.Create("Nd", 60, new int[] {2,8,18,22,8,2}, "Lanthanide", "3", 6)},
            {"Promethium", Tuple.Create("Pm", 61, new int[] {2,8,18,23,8,2}, "Lanthanide", "3", 6)},
            {"Samarium", Tuple.Create("Sm", 62, new int[] {2,8,18,24,8,2}, "Lanthanide", "3", 6)},
            {"Europium", Tuple.Create("Eu", 63, new int[] {2,8,18,25,8,2}, "Lanthanide", "3", 6)},
            {"Gadolinium", Tuple.Create("Gd", 64, new int[] {2,8,18,25,9,2}, "Lanthanide", "3", 6)},
            {"Terbium", Tuple.Create("Tb", 65, new int[] {2,8,18,27,8,2}, "Lanthanide", "3", 6)},
            {"Dysprosium", Tuple.Create("Dy", 66, new int[] {2,8,18,28,8,2}, "Lanthanide", "3", 6)},
            {"Holmium", Tuple.Create("Ho", 67, new int[] {2,8,18,29,8,2}, "Lanthanide", "3", 6)},
            {"Erbium", Tuple.Create("Er", 68, new int[] {2,8,18,30,8,2}, "Lanthanide", "3", 6)},
            {"Thulium", Tuple.Create("Tm", 69, new int[] {2,8,18,31,8,2}, "Lanthanide", "3", 6)},
            {"Ytterbium", Tuple.Create("Yb", 70, new int[] {2,8,18,32,8,2}, "Lanthanide", "3", 6)},
            {"Lutetium", Tuple.Create("Lu", 71, new int[] {2,8,18,32,9,2}, "Lanthanide", "3", 6)},
            {"Hafnium", Tuple.Create("Hf", 72, new int[] {2,8,18,32,10,2}, "Transition Metal", "4", 6)},
            {"Tantalum", Tuple.Create("Ta", 73, new int[] {2,8,18,32,11,2}, "Transition Metal", "5", 6)},
            {"Tungsten", Tuple.Create("W", 74, new int[] {2,8,18,32,12,2}, "Transition Metal", "6", 6)},
            {"Rhenium", Tuple.Create("Re", 75, new int[] {2,8,18,32,13,2}, "Transition Metal", "7", 6)},
            {"Osmium", Tuple.Create("Os", 76, new int[] {2,8,18,32,14,2}, "Transition Metal", "8", 6)},
            {"Iridium", Tuple.Create("Ir", 77, new int[] {2,8,18,32,15,2}, "Transition Metal", "9", 6)},
            {"Platinum", Tuple.Create("Pt", 78, new int[] {2,8,18,32,17,1}, "Transition Metal", "10", 6)},
            {"Gold", Tuple.Create("Au", 79, new int[] {2,8,18,32,18,1}, "Transition Metal", "11", 6)},
            {"Mercury", Tuple.Create("Hg", 80, new int[] {2,8,18,32,18,2}, "Transition Metal", "12", 6)},
            {"Thallium", Tuple.Create("Tl", 81, new int[] {2,8,18,32,18,3}, "Post-Transition", "13", 6)},
            {"Lead", Tuple.Create("Pb", 82, new int[] {2,8,18,32,18,4}, "Post-Transition", "14", 6)},
            {"Bismuth", Tuple.Create("Bi", 83, new int[] {2,8,18,32,18,5}, "Post-Transition", "15", 6)},
            {"Polonium", Tuple.Create("Po", 84, new int[] {2,8,18,32,18,6}, "Metalloid", "16", 6)},
            {"Astatine", Tuple.Create("At", 85, new int[] {2,8,18,32,18,7}, "Halogen", "17", 6)},
            {"Radon", Tuple.Create("Rn", 86, new int[] {2,8,18,32,18,8}, "Noble Gas", "18", 6)},
            {"Francium", Tuple.Create("Fr", 87, new int[] {2,8,18,32,18,8,1}, "Alkali Metal", "1", 7)},
            {"Radium", Tuple.Create("Ra", 88, new int[] {2,8,18,32,18,8,2}, "Alkaline Earth", "2", 7)},
            {"Actinium", Tuple.Create("Ac", 89, new int[] {2,8,18,32,18,9,2}, "Actinide", "3", 7)},
            {"Thorium", Tuple.Create("Th", 90, new int[] {2,8,18,32,18,10,2}, "Actinide", "3", 7)},
            {"Protactinium", Tuple.Create("Pa", 91, new int[] {2,8,18,32,20,9,2}, "Actinide", "3", 7)},
            {"Uranium", Tuple.Create("U", 92, new int[] {2,8,18,32,21,9,2}, "Actinide", "3", 7)},
            {"Neptunium", Tuple.Create("Np", 93, new int[] {2,8,18,32,22,9,2}, "Actinide", "3", 7)},
            {"Plutonium", Tuple.Create("Pu", 94, new int[] {2,8,18,32,24,8,2}, "Actinide", "3", 7)},
            {"Americium", Tuple.Create("Am", 95, new int[] {2,8,18,32,25,8,2}, "Actinide", "3", 7)},
            {"Curium", Tuple.Create("Cm", 96, new int[] {2,8,18,32,25,9,2}, "Actinide", "3", 7)},
            {"Berkelium", Tuple.Create("Bk", 97, new int[] {2,8,18,32,26,9,2}, "Actinide", "3", 7)},
            {"Californium", Tuple.Create("Cf", 98, new int[] {2,8,18,32,27,9,2}, "Actinide", "3", 7)},
            {"Einsteinium", Tuple.Create("Es", 99, new int[] {2,8,18,32,28,9,2}, "Actinide", "3", 7)},
            {"Fermium", Tuple.Create("Fm", 100, new int[] {2,8,18,32,29,9,2}, "Actinide", "3", 7)},
            {"Mendelevium", Tuple.Create("Md", 101, new int[] {2,8,18,32,30,9,2}, "Actinide", "3", 7)},
            {"Nobelium", Tuple.Create("No", 102, new int[] {2,8,18,32,31,9,2}, "Actinide", "3", 7)},
            {"Lawrencium", Tuple.Create("Lr", 103, new int[] {2,8,18,32,32,8,3}, "Actinide", "3", 7)},
            {"Rutherfordium", Tuple.Create("Rf", 104, new int[] {2,8,18,32,32,10,2}, "Transition Metal", "4", 7)},
            {"Dubnium", Tuple.Create("Db", 105, new int[] {2,8,18,32,32,11,2}, "Transition Metal", "5", 7)},
            {"Seaborgium", Tuple.Create("Sg", 106, new int[] {2,8,18,32,32,12,2}, "Transition Metal", "6", 7)},
            {"Bohrium", Tuple.Create("Bh", 107, new int[] {2,8,18,32,32,13,2}, "Transition Metal", "7", 7)},
            {"Hassium", Tuple.Create("Hs", 108, new int[] {2,8,18,32,32,14,2}, "Transition Metal", "8", 7)},
            {"Meitnerium", Tuple.Create("Mt", 109, new int[] {2,8,18,32,32,15,2}, "Transition Metal", "9", 7)},
            {"Darmstadtium", Tuple.Create("Ds", 110, new int[] {2,8,18,32,32,16,2}, "Transition Metal", "10", 7)},
            {"Roentgenium", Tuple.Create("Rg", 111, new int[] {2,8,18,32,32,17,2}, "Transition Metal", "11", 7)},
            {"Copernicium", Tuple.Create("Cn", 112, new int[] {2,8,18,32,32,18,2}, "Transition Metal", "12", 7)},
            {"Nihonium", Tuple.Create("Nh", 113, new int[] {2,8,18,32,32,18,3}, "Post-Transition", "13", 7)},
            {"Flerovium", Tuple.Create("Fl", 114, new int[] {2,8,18,32,32,18,4}, "Post-Transition", "14", 7)},
            {"Moscovium", Tuple.Create("Mc", 115, new int[] {2,8,18,32,32,18,5}, "Post-Transition", "15", 7)},
            {"Livermorium", Tuple.Create("Lv", 116, new int[] {2,8,18,32,32,18,6}, "Post-Transition", "16", 7)},
            {"Tennessine", Tuple.Create("Ts", 117, new int[] {2,8,18,32,32,18,7}, "Halogen", "17", 7)},
            {"Oganesson", Tuple.Create("Og", 118, new int[] {2,8,18,32,32,18,8}, "Noble Gas", "18", 7)}
    };

        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            SetupUI();
            ApplyDarkTheme();
        }

        private void SetupUI()
        {
            this.Text = "Valence Shell Structures";
            this.ClientSize = new Size(800, 650);
            this.BackColor = DarkBackground;
            elementComboBox = new ComboBox
            {
                Location = new Point(20, 20),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            elementComboBox.Items.AddRange(elements.Keys.ToArray());
            elementComboBox.SelectedIndex = 0;
            this.Controls.Add(elementComboBox);
            drawButton = new Button
            {
                Text = "Draw Structure",
                Location = new Point(210, 20),
                Size = new Size(140, 28),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10)
            };
            drawButton.Click += DrawButton_Click;
            this.Controls.Add(drawButton);
            infoLabel = new Label
            {
                Location = new Point(20, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = TextColor
            };
            this.Controls.Add(infoLabel);
            detailsLabel = new Label
            {
                Location = new Point(20, 100),
                Size = new Size(200, 120),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.LightGray
            };
            this.Controls.Add(detailsLabel);
            shellPanel = new Panel
            {
                Location = new Point(240, 70),
                Size = new Size(540, 560),
                BackColor = DarkPanel,
                BorderStyle = BorderStyle.FixedSingle
            };
            shellPanel.Paint += ShellPanel_Paint;
            this.Controls.Add(shellPanel);
        }
        private void ApplyDarkTheme()
        {
            elementComboBox.BackColor = Color.FromArgb(50, 50, 60);
            elementComboBox.ForeColor = TextColor;
            drawButton.BackColor = Color.FromArgb(60, 60, 80);
            drawButton.ForeColor = TextColor;
            drawButton.FlatAppearance.BorderColor = Color.FromArgb(80, 80, 100);
            drawButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 100);
            drawButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(40, 40, 60);
        }
        private void DrawButton_Click(object sender, EventArgs e)
        {
            shellPanel.Invalidate(); 
        }
        private void ShellPanel_Paint(object sender, PaintEventArgs e)
        {
            if (elementComboBox.SelectedItem == null) return;
            string selectedElement = elementComboBox.SelectedItem.ToString();
            var elementData = elements[selectedElement];
            string symbol = elementData.Item1;
            int atomicNumber = elementData.Item2;
            int[] electrons = elementData.Item3;
            string category = elementData.Item4;
            string group = elementData.Item5;
            int period = elementData.Item6;
            infoLabel.Text = $"{atomicNumber}: {selectedElement}";
            detailsLabel.Text = $"Category: {category}\n" +
                               $"Group: {group}\n" +
                               $"Period: {period}\n" +
                               $"Electron Configuration: {string.Join(",", electrons)}\n" +
                               $"Valence Electrons: {electrons[electrons.Length - 1]}";
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(DarkPanel);
            int centerX = shellPanel.Width / 2;
            int centerY = shellPanel.Height / 2;
            int nucleusSize = 40;
            g.FillEllipse(new SolidBrush(NucleusColor), centerX - nucleusSize / 2, centerY - nucleusSize / 2, nucleusSize, nucleusSize);
            g.DrawString(symbol, new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Black, centerX - 10, centerY - 5);
            int numShells = electrons.Length;
            const int minRadius = 40; 
            const int maxRadius = 250; 
            for (int shell = 0; shell < numShells; shell++)
            {
                int radius;
                if (numShells == 1)
                {
                    radius = minRadius; 
                }
                else
                {
                    radius = minRadius + (shell * (maxRadius - minRadius) / (numShells - 1));
                }
                int electronsInShell = electrons[shell];
                Pen orbitPen = new Pen(OrbitColor, 2f);
                g.DrawEllipse(orbitPen, centerX - radius, centerY - radius, radius * 2, radius * 2);
                double angleStep = 2 * Math.PI / Math.Max(1, electronsInShell);
                int electronSize = 8; 
                for (int i = 0; i < electronsInShell; i++)
                {
                    double angle = i * angleStep;
                    int x = (int)(centerX + radius * Math.Cos(angle));
                    int y = (int)(centerY + radius * Math.Sin(angle));
                    g.FillEllipse(new SolidBrush(ElectronColor),
                                 x - electronSize / 2, y - electronSize / 2, electronSize, electronSize);
                }
            }
        }

        private Brush GetCategoryBrush(string category)
        {
            switch (category)
            {
                case "Alkali Metal": return Brushes.OrangeRed;
                case "Alkaline Earth": return Brushes.Gold;
                case "Transition Metal": return Brushes.DodgerBlue;
                case "Post-Transition": return Brushes.LightSeaGreen;
                case "Metalloid": return Brushes.MediumSeaGreen;
                case "Nonmetal": return Brushes.LightGreen;
                case "Halogen": return Brushes.PaleGreen;
                case "Noble Gas": return Brushes.Plum;
                default: return Brushes.Crimson;
            }
        }

        private string GetElectronConfiguration(int[] electrons)
        {
            string[] subshells = { "1s", "2s", "2p", "3s", "3p", "4s", "3d", "4p", "5s", "4d", "5p", "6s", "4f" };
            string config = "";
            int totalElectrons = 0;
            foreach (int e in electrons) totalElectrons += e;
            int eIndex = 0;
            int currentElectronCount = 0;
            for (int i = 0; i < subshells.Length && eIndex < electrons.Length; i++)
            {
                int maxE = subshells[i].Contains("s") ? 2 :
                          subshells[i].Contains("p") ? 6 :
                          subshells[i].Contains("d") ? 10 :
                          subshells[i].Contains("f") ? 14 : 2;
                int remainingElectrons = totalElectrons - currentElectronCount;
                if (remainingElectrons <= 0) break;
                int eCount = Math.Min(maxE, remainingElectrons);
                if (eCount > 0)
                {
                    if (currentElectronCount < totalElectrons)
                    {
                        config += $"{subshells[i]}² ".Replace("²", eCount > 2 ? $"^{eCount}" : (eCount == 2 ? "²" : "¹"));
                        currentElectronCount += eCount;
                    }
                    if (electrons[eIndex] <= eCount)
                    {
                        eIndex++;
                    }
                }
            }
            return config.Trim();
        }
    }
}