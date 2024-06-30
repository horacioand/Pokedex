using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokedex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Pokemon> listaPokemon = new List<Pokemon>();   
            PokemonDatabase conexionPokemon = new PokemonDatabase();
            listaPokemon = conexionPokemon.listar();
            dgvPokemons.DataSource = listaPokemon;  //cargar la lista en dgv
            dgvPokemons.Columns["Imagen"].Visible = false;  //ocultar la columna
            cargarImagen(listaPokemon[0].Imagen); 
        }


        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon pokemonSeleccionado = dgvPokemons.CurrentRow.DataBoundItem as Pokemon;
            cargarImagen(pokemonSeleccionado.Imagen);
        }

        private void cargarImagen(string imagen)    //funcion para cargar la imagen en pbx
        {
            try
            {
                pbxPokemon.Load(imagen);
            }
            catch (Exception)
            {
                pbxPokemon.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQaw00Z7-vJPlGR5DVgW-XvQRUfHXqafJUNYw&s");
            }
        }
    }
}
