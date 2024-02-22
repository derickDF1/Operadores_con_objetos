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

namespace Operadores_con_objetos
{
    public partial class Form1 : Form
    {
        List<Persona> personas = new List<Persona>();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            //Instanciar la clase persona
            Persona persona = new Persona();

            //Guardar los datos de una persona
            persona.Dpi = textBoxDPI.Text;
            persona.Nombre = textBoxNombre.Text;
            persona.Apellido = textBoxApellido.Text;
            persona.FechaNacimiento = monthCalendarFechaNacimiento.SelectionStart;

            //Mando a guardar a la persona a mi lista de personass
            personas.Add(persona);

            textBoxDPI.Text = "";
            textBoxNombre.Text = "";
            textBoxApellido.Text = "";
            monthCalendarFechaNacimiento.SetDate(DateTime.Now);
            textBoxDPI.Select();
        }

        private void Mostrar ()
        {
            dataGridViewPersonas.DataSource = null;
            dataGridViewPersonas.DataSource = personas;
            dataGridViewPersonas.Refresh();
        }

        private void buttonMostrar_Click(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void buttonOrdenarApellido_Click(object sender, EventArgs e)
        {
            personas = personas.OrderBy(p => p.Apellido).ToList(); //p es solamente una variable que nos inventamos
            Mostrar();
        }

        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            string dpi = textBoxDPI.Text;
            personas.RemoveAll(p => p.Dpi == dpi);
            Mostrar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            personas = personas.OrderByDescending(p => p.Apellido).ToList();
            Mostrar();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            FileStream stream = new FileStream("Personas.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            for(int i = 0; i<personas.Count; i++)
            {
                writer.WriteLine(personas[i].Dpi);
                writer.WriteLine(personas[i].Nombre);
                writer.WriteLine(personas[i].Apellido);
                writer.WriteLine(personas[i].FechaNacimiento.ToShortDateString());
            }

            writer.Close();
        }

        private void buttonCargarDatos_Click(object sender, EventArgs e)
        {
            string fileName = "Personas.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while(reader.Peek() > -1)
            {
                Persona persona = new Persona();

                persona.Dpi = reader.ReadLine();
                persona.Nombre = reader.ReadLine();
                persona.Apellido = reader.ReadLine();
                persona.FechaNacimiento = Convert.ToDateTime(reader.ReadLine());
            }
        }
    }
}
