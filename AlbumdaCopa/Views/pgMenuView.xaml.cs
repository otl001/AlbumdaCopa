using System;
using Microsoft.Maui.Controls;

namespace AlbumdaCopa.Views
{
    public partial class pgMenuView : ContentPage
    {
        public pgMenuView()
        {
            InitializeComponent();
        }

        // abre a tela de abrir pacotinhos
        private async void OnAbrirPacotinhoClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new pgPacotinhoView());
        }

        // abre a tela do album virtual
        private async void OnAlbumClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new pgAlbumView());
        }

        // abre a tela de cadastrar figurinha
        private async void OnCadastroClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new pgCadFigurinhaView());
        }

        // abre a tela com a colecao de figurinhas
        private async void OnListagemClicked(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new pgColecaoView());
        }
    }
}
