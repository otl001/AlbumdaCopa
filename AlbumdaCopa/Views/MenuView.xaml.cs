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

        // abre a tela de abrir pacotinhos
        private async void OnAbrirPacotinhoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PacotinhoView());
        }

        // abre a tela do album virtual
        private async void OnAlbumClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlbumView());
        }

        // abre a tela de cadastrar figurinha
        private async void OnCadastroClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CadastroView());
        }

        // abre a tela com a colecao de figurinhas
        private async void OnListagemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListagemView());
        }
    }
}
