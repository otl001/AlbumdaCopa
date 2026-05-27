using System;
using Microsoft.Maui.Controls;

namespace AlbumdaCopa.Views
{
    public partial class MenuView : ContentPage
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private async void OnAbrirPacotinhoClicked(object sender, EventArgs e)
        {
            // Navega para a tela de abertura de pacotes
            await Navigation.PushAsync(new PacotinhoView());
        }

        private async void OnAlbumClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlbumView());
        }

        private async void OnCadastroClicked(object sender, EventArgs e)
        {
            // Navega para a tela de cadastro
            await Navigation.PushAsync(new CadastroView());
        }

        private async void OnListagemClicked(object sender, EventArgs e)
        {
            // Navega para a tela de listagem
            await Navigation.PushAsync(new ListagemView());
        }
    }
}
