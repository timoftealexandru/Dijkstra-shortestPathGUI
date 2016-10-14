using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace ApmDijkstra
{
    public partial class FormApmDijkstra : Form
    {
        private Dictionary<string, Node> _dictNodes;     // Dictionary cu key si Value
        private List<Edge> _edges;                       // Lista tip matrice fara rezervare de spatiu
        private List<Node> _nodes;
        private Graph dijkstra;                              //clasa Dijkstra
        TextBox[] txtBxCosturi;        //array cu costurile din cele 24 textBox.uri
        private Dictionary<String, Panel> _panel;
        private Dictionary<String, TextBox> _textBox;
        private XmlTextWriter xmlDijkstraWriter;
        private XmlTextReader xmlDijkstraReader;

        public FormApmDijkstra()
        {
            InitializeComponent();

            txtBxCosturi = new TextBox[24];
            txtBxCosturi[0] = textBoxA_B;                //trec valorile fiecarui texBox in Array-ul txtBxCosturi
            txtBxCosturi[1] = textBoxA_F;
            txtBxCosturi[2] = textBoxB_E;
            txtBxCosturi[3] = textBoxB_C;
            txtBxCosturi[4] = textBoxC_D;
            txtBxCosturi[5] = textBoxC_G;
            txtBxCosturi[6] = textBoxD_E;
            txtBxCosturi[7] = textBoxD_H;
            txtBxCosturi[8] = textBoxD_P;
            txtBxCosturi[9] = textBoxE_F;
            txtBxCosturi[10] = textBoxE_O;
            txtBxCosturi[11] = textBoxF_N;
            txtBxCosturi[12] = textBoxN_O;
            txtBxCosturi[13] = textBoxN_M;
            txtBxCosturi[14] = textBoxM_L;
            txtBxCosturi[15] = textBoxO_L;
            txtBxCosturi[16] = textBoxL_K;
            txtBxCosturi[17] = textBoxP_K;
            txtBxCosturi[18] = textBoxK_J;
            txtBxCosturi[19] = textBoxI_J;
            txtBxCosturi[20] = textBoxP_I;
            txtBxCosturi[21] = textBoxH_I;
            txtBxCosturi[22] = textBoxG_H;
            txtBxCosturi[23] = textBoxO_P;

            comboBoxNodeEnd.SelectedIndex = 0;          // afiseaza primul item din combobox
            comboBoxNodeStart.SelectedIndex = 0;
            // init ... todo
            _dictNodes = new Dictionary<string, Node>(); //initializeaza Dictionary cu Nodes (key tip string, value tip Node)

            _dictNodes.Add("A", new Node("A"));          // am introdus nodurile in Dictionary 
            _dictNodes.Add("B", new Node("B"));
            _dictNodes.Add("C", new Node("C"));
            _dictNodes.Add("D", new Node("D"));
            _dictNodes.Add("E", new Node("E"));
            _dictNodes.Add("F", new Node("F"));
            _dictNodes.Add("G", new Node("G"));
            _dictNodes.Add("H", new Node("H"));
            _dictNodes.Add("I", new Node("I"));
            _dictNodes.Add("J", new Node("J"));
            _dictNodes.Add("K", new Node("K"));
            _dictNodes.Add("L", new Node("L"));
            _dictNodes.Add("M", new Node("M"));
            _dictNodes.Add("N", new Node("N"));
            _dictNodes.Add("O", new Node("O"));
            _dictNodes.Add("P", new Node("P"));

            // todo
            _nodes = new List<Node>();                  //initializeaza lista Nodes
            foreach (Node n in _dictNodes.Values)       //adauga fiecare nod n din Dictionary in List
            {
                _nodes.Add(n);
            }

            // init muchii ...

            _textBox = new Dictionary<string, TextBox>();
            _textBox.Add("A_B", textBoxA_B);
            _textBox.Add("A_F", textBoxA_F);
            _textBox.Add("B_E", textBoxB_E);
            _textBox.Add("B_C", textBoxB_C);
            _textBox.Add("C_D", textBoxC_D);
            _textBox.Add("C_G", textBoxC_G);
            _textBox.Add("D_E", textBoxD_E);
            _textBox.Add("D_H", textBoxD_H);
            _textBox.Add("D_P", textBoxD_P);
            _textBox.Add("E_F", textBoxE_F);
            _textBox.Add("E_O", textBoxE_O);
            _textBox.Add("F_N", textBoxF_N);
            _textBox.Add("N_O", textBoxN_O);
            _textBox.Add("N_M", textBoxN_M);
            _textBox.Add("M_L", textBoxM_L);
            _textBox.Add("O_L", textBoxO_L);
            _textBox.Add("L_K", textBoxL_K);
            _textBox.Add("P_K", textBoxP_K);
            _textBox.Add("K_J", textBoxK_J);
            _textBox.Add("I_J", textBoxI_J);
            _textBox.Add("P_I", textBoxP_I);
            _textBox.Add("H_I", textBoxH_I);
            _textBox.Add("G_H", textBoxG_H);
            _textBox.Add("O_P", textBoxO_P);

            _panel = new Dictionary<String, Panel>();
            _panel.Add("A_B", panelA_B);
            _panel.Add("B_C", panelB_C);
            _panel.Add("B_E", panelB_E);
            _panel.Add("C_D", panelC_D);
            _panel.Add("C_G", panelC_G);
            _panel.Add("D_E", panelD_E);
            _panel.Add("D_H", panelD_H);
            _panel.Add("D_P", panelD_P);
            _panel.Add("E_F", panelE_F);
            _panel.Add("E_O", panelE_O);
            _panel.Add("A_F", panelA_F);
            _panel.Add("F_N", panelF_N);
            _panel.Add("N_O", panelN_O);
            _panel.Add("M_N", panelM_N);
            _panel.Add("O_P", panelO_P);
            _panel.Add("I_P", panelI_P);
            _panel.Add("K_P", panelK_P);
            _panel.Add("L_M", panelL_M);
            _panel.Add("L_O", panelL_O);
            _panel.Add("K_L", panelK_L);
            _panel.Add("J_K", panelJ_K);
            _panel.Add("I_J", panelI_J);
            _panel.Add("H_I", panelH_I);
            _panel.Add("G_H", panelG_H);
            _panel.Add("B_A", panelA_B);
            _panel.Add("C_B", panelB_C);
            _panel.Add("E_B", panelB_E);
            _panel.Add("D_C", panelC_D);
            _panel.Add("G_C", panelC_G);
            _panel.Add("E_D", panelD_E);
            _panel.Add("H_D", panelD_H);
            _panel.Add("P_D", panelD_P);
            _panel.Add("F_E", panelE_F);
            _panel.Add("O_E", panelE_O);
            _panel.Add("F_A", panelA_F);
            _panel.Add("N_F", panelF_N);
            _panel.Add("O_N", panelN_O);
            _panel.Add("N_M", panelM_N);
            _panel.Add("P_O", panelO_P);
            _panel.Add("P_I", panelI_P);
            _panel.Add("P_K", panelK_P);
            _panel.Add("M_L", panelL_M);
            _panel.Add("O_L", panelL_O);
            _panel.Add("L_K", panelK_L);
            _panel.Add("K_J", panelJ_K);
            _panel.Add("J_I", panelI_J);
            _panel.Add("I_H", panelH_I);
            _panel.Add("H_G", panelG_H);

        }

        private double converCost(TextBox textBox)
        {
            double result;
            try
            {
                result = Convert.ToDouble(textBox.Text);
            }
            catch
            {
                result = 99999;
                textBox.Text = result.ToString();
            }

            return result;
        }

        private void init()
        {
            _edges = new List<Edge>();                  //initializez Lista cu muchii apoi introduc in ea intre fiecare perechi
            //de noduri valoarea convertita in double din textBox-ul respectiv
            _edges.Add(new Edge(_dictNodes["A"], _dictNodes["B"], converCost(textBoxA_B)));
            _edges.Add(new Edge(_dictNodes["A"], _dictNodes["F"], converCost(textBoxA_F)));

            _edges.Add(new Edge(_dictNodes["B"], _dictNodes["A"], converCost(textBoxA_B)));
            _edges.Add(new Edge(_dictNodes["B"], _dictNodes["C"], converCost(textBoxB_C)));
            _edges.Add(new Edge(_dictNodes["B"], _dictNodes["E"], converCost(textBoxB_E)));

            _edges.Add(new Edge(_dictNodes["C"], _dictNodes["B"], converCost(textBoxB_C)));
            _edges.Add(new Edge(_dictNodes["C"], _dictNodes["D"], converCost(textBoxC_D)));
            _edges.Add(new Edge(_dictNodes["C"], _dictNodes["G"], converCost(textBoxC_G)));

            _edges.Add(new Edge(_dictNodes["D"], _dictNodes["C"], converCost(textBoxC_D)));
            _edges.Add(new Edge(_dictNodes["D"], _dictNodes["E"], converCost(textBoxD_E)));
            _edges.Add(new Edge(_dictNodes["D"], _dictNodes["H"], converCost(textBoxD_H)));
            _edges.Add(new Edge(_dictNodes["D"], _dictNodes["P"], converCost(textBoxD_P)));

            _edges.Add(new Edge(_dictNodes["E"], _dictNodes["F"], converCost(textBoxE_F)));
            _edges.Add(new Edge(_dictNodes["E"], _dictNodes["B"], converCost(textBoxB_E)));
            _edges.Add(new Edge(_dictNodes["E"], _dictNodes["D"], converCost(textBoxD_E)));
            _edges.Add(new Edge(_dictNodes["E"], _dictNodes["O"], converCost(textBoxE_O)));

            _edges.Add(new Edge(_dictNodes["F"], _dictNodes["A"], converCost(textBoxA_F)));
            _edges.Add(new Edge(_dictNodes["F"], _dictNodes["E"], converCost(textBoxE_F)));
            _edges.Add(new Edge(_dictNodes["F"], _dictNodes["N"], converCost(textBoxF_N)));

            _edges.Add(new Edge(_dictNodes["N"], _dictNodes["F"], converCost(textBoxF_N)));
            _edges.Add(new Edge(_dictNodes["N"], _dictNodes["O"], converCost(textBoxN_O)));
            _edges.Add(new Edge(_dictNodes["N"], _dictNodes["M"], converCost(textBoxN_M)));

            _edges.Add(new Edge(_dictNodes["O"], _dictNodes["N"], converCost(textBoxN_O)));
            _edges.Add(new Edge(_dictNodes["O"], _dictNodes["E"], converCost(textBoxE_O)));
            _edges.Add(new Edge(_dictNodes["O"], _dictNodes["P"], converCost(textBoxO_P)));
            _edges.Add(new Edge(_dictNodes["O"], _dictNodes["L"], converCost(textBoxO_L)));

            _edges.Add(new Edge(_dictNodes["P"], _dictNodes["O"], converCost(textBoxO_P)));
            _edges.Add(new Edge(_dictNodes["P"], _dictNodes["D"], converCost(textBoxD_P)));
            _edges.Add(new Edge(_dictNodes["P"], _dictNodes["I"], converCost(textBoxP_I)));
            _edges.Add(new Edge(_dictNodes["P"], _dictNodes["K"], converCost(textBoxP_K)));

            _edges.Add(new Edge(_dictNodes["M"], _dictNodes["N"], converCost(textBoxN_M)));
            _edges.Add(new Edge(_dictNodes["M"], _dictNodes["L"], converCost(textBoxM_L)));

            _edges.Add(new Edge(_dictNodes["L"], _dictNodes["M"], converCost(textBoxM_L)));
            _edges.Add(new Edge(_dictNodes["L"], _dictNodes["O"], converCost(textBoxO_L)));
            _edges.Add(new Edge(_dictNodes["L"], _dictNodes["K"], converCost(textBoxL_K)));

            _edges.Add(new Edge(_dictNodes["K"], _dictNodes["L"], converCost(textBoxL_K)));
            _edges.Add(new Edge(_dictNodes["K"], _dictNodes["P"], converCost(textBoxP_K)));
            _edges.Add(new Edge(_dictNodes["K"], _dictNodes["J"], converCost(textBoxK_J)));

            _edges.Add(new Edge(_dictNodes["J"], _dictNodes["K"], converCost(textBoxK_J)));
            _edges.Add(new Edge(_dictNodes["J"], _dictNodes["I"], converCost(textBoxI_J)));

            _edges.Add(new Edge(_dictNodes["I"], _dictNodes["J"], converCost(textBoxI_J)));
            _edges.Add(new Edge(_dictNodes["I"], _dictNodes["P"], converCost(textBoxP_I)));
            _edges.Add(new Edge(_dictNodes["I"], _dictNodes["H"], converCost(textBoxH_I)));

            _edges.Add(new Edge(_dictNodes["H"], _dictNodes["I"], converCost(textBoxH_I)));
            _edges.Add(new Edge(_dictNodes["H"], _dictNodes["D"], converCost(textBoxD_H)));
            _edges.Add(new Edge(_dictNodes["H"], _dictNodes["G"], converCost(textBoxG_H)));

            _edges.Add(new Edge(_dictNodes["G"], _dictNodes["H"], converCost(textBoxG_H)));
            _edges.Add(new Edge(_dictNodes["G"], _dictNodes["C"], converCost(textBoxC_G)));
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {

            string keystart = (string)comboBoxNodeStart.SelectedItem;      //ii dau lui keystart valoarea selectata din comboBox
            string keyend = (string)comboBoxNodeEnd.SelectedItem;          //la fel si pentru keyend din comboboxNodeEnd



            init();                                                        // run init()


            // Neues Objekt der Klasse Dijkstra erstellen
            dijkstra = new Graph(_edges, _nodes);                       //initializez noul obiect din clasa Dijkstra 

            // Startknoten festlegen und Distanzen berechnen
            dijkstra.calculateDistance(_dictNodes[keystart]);              // setarea nodului de start si calcularea distantei


            // Pfad zu einem bestimmten Knoten ausgeben                    //scoate distanta pana la nodul final ales
            List<Node> path = dijkstra.getPathTo(_dictNodes[keyend]);

            if (path.Count > 0)                                            //daca gaseste o cale o trece in textBox 
            {
                textBox.AppendText("Drumul cautat este: ");

                string panelNume;
                string nodeStart = "";
                string nodeEnd;


                foreach (Node n in path)
                {
                    textBox.AppendText(n.Name + " ");
                    if (nodeStart == "")
                    {
                        nodeStart = n.Name;
                    }
                    else
                    {
                        nodeEnd = n.Name;

                        panelNume = nodeStart + "_" + nodeEnd;
                        _panel[panelNume].BackColor = Color.Red;
                        // textBox.AppendText(panelNume + "\r\n");

                        nodeStart = nodeEnd;
                    }
                }
                textBox.AppendText("\r\n");
            }
           
        }

        private void comboBoxNodeStart_SelectedIndexChanged(object sender, EventArgs e) //butonul calculeaza este activat daca sunt 
        {                                                                               //alese cele 2 capmuri
            buttonCalc.Enabled = ((comboBoxNodeStart.SelectedIndex > 0) && (comboBoxNodeEnd.SelectedIndex > 0));
           
        }

        private void comboBoxNodeEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonCalc.Enabled = ((comboBoxNodeStart.SelectedIndex > 0) && (comboBoxNodeEnd.SelectedIndex > 0));
    
        }

        private void buttonInit_Click(object sender, EventArgs e) //butonul de initializare
        {
            init();
            MessageBox.Show("Initializarea a fost facuta");
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            panelA_B.BackColor = Color.Black;
            panelB_C.BackColor = Color.Black;
            panelB_E.BackColor = Color.Black;
            panelC_D.BackColor = Color.Black;
            panelC_G.BackColor = Color.Black;
            panelD_E.BackColor = Color.Black;
            panelD_H.BackColor = Color.Black;
            panelD_P.BackColor = Color.Black;
            panelE_F.BackColor = Color.Black;
            panelE_O.BackColor = Color.Black;
            panelA_F.BackColor = Color.Black;
            panelF_N.BackColor = Color.Black;
            panelN_O.BackColor = Color.Black;
            panelM_N.BackColor = Color.Black;
            panelO_P.BackColor = Color.Black;
            panelI_P.BackColor = Color.Black;
            panelK_P.BackColor = Color.Black;
            panelL_M.BackColor = Color.Black;
            panelL_O.BackColor = Color.Black;
            panelK_L.BackColor = Color.Black;
            panelJ_K.BackColor = Color.Black;
            panelI_J.BackColor = Color.Black;
            panelH_I.BackColor = Color.Black;
            panelG_H.BackColor = Color.Black;
            MessageBox.Show("Distantele au fost resetate");
        }

        private void buttonSalveaza_Click(object sender, EventArgs e)
        {
            if (saveFileDialogSalveaza.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                xmlDijkstraWriter = new XmlTextWriter(saveFileDialogSalveaza.FileName, new UnicodeEncoding());
                xmlDijkstraWriter.Formatting = Formatting.Indented;
                xmlDijkstraWriter.WriteStartDocument();
                xmlDijkstraWriter.WriteStartElement("ApmDijkstra");
                xmlDijkstraWriter.WriteStartElement("Distante");

                for (int i = 0; i < 24; i++)
                {
                    xmlDijkstraWriter.WriteStartElement("Distanta");
                    string nume;
                    nume = txtBxCosturi[i].Name;
                    nume = nume.Replace("textBox", "");
                    xmlDijkstraWriter.WriteAttributeString("nume", nume);
                    double valoare;
                    valoare = Convert.ToDouble(txtBxCosturi[i].Text);
                    xmlDijkstraWriter.WriteAttributeString("valoare", valoare.ToString());
                    xmlDijkstraWriter.WriteEndElement(); // Distanta
                }

                xmlDijkstraWriter.WriteEndElement(); // Distante
                xmlDijkstraWriter.WriteEndElement(); // ApmDijkstra
                xmlDijkstraWriter.Close();
            }
            MessageBox.Show("Datele au fost salvate");
        }

        private void buttonIncarca_Click(object sender, EventArgs e)
        {
            if (openFileDialogIncarca.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                xmlDijkstraReader = new XmlTextReader(openFileDialogIncarca.FileName);
                XmlNodeType type;
                while (xmlDijkstraReader.Read())
                {
                    type = xmlDijkstraReader.NodeType;
                    if (type == XmlNodeType.Element)
                    {
                        if (xmlDijkstraReader.Name == "Distanta")
                        {
                            if (xmlDijkstraReader.AttributeCount > 0)
                            {
                                bool numeCitit = false;
                                bool valoareaCitita = false;

                                string numeTextBoxKey = "";
                                string valoareTextBox = "";
                                while (xmlDijkstraReader.MoveToNextAttribute())
                                {
                                    string attributeName = xmlDijkstraReader.Name;

                                    if (attributeName == "nume")
                                    {
                                        numeTextBoxKey = xmlDijkstraReader.Value;
                                        numeCitit = true;
                                    }
                                    else if (attributeName == "valoare")
                                    {
                                        valoareTextBox = xmlDijkstraReader.Value;
                                        valoareaCitita = true;
                                    }

                                    if (numeCitit && valoareaCitita)
                                    {
                                        numeCitit = false;
                                        valoareaCitita = false;

                                        if (_textBox.ContainsKey(numeTextBoxKey))
                                            _textBox[numeTextBoxKey].Text = valoareTextBox;
                                    }
                                }
                            }
                        }
                    }
                } // while
                xmlDijkstraReader.Close();
            }
            MessageBox.Show("Datele au fost Incarcate");
        }
    }
}

