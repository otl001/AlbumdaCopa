using System;
using Microsoft.Maui.Controls;
using AlbumdaCopa.Models;

namespace AlbumdaCopa.Views
{
    public partial class VisualizacaoView : ContentPage
    {
        public VisualizacaoView(Figurinha figurinha)
        {
            InitializeComponent();
            
            if (figurinha != null)
            {
                Title = $"Figurinha: {figurinha.NomeJogador}";
                imgFoto.Source = figurinha.FotoPath;
                lblNome.Text = figurinha.NomeJogador;
            }
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
