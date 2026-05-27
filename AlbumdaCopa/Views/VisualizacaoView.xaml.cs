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
            
            // preenche todos os dados e estiliza o card detalhado
            if (figurinha != null)
            {
                Title = $"Figurinha: {figurinha.NomeJogador}";
                
                imgFoto.Source = figurinha.FotoPath;
                lblNome.Text = figurinha.NomeJogador;
                lblSelecao.Text = string.IsNullOrEmpty(figurinha.Selecao) || figurinha.Selecao == "Não Definida" 
                    ? "Seleção Não Definida" 
                    : figurinha.Selecao;

                // muda as cores se a figurinha for especial
                if (figurinha.Tipo == "Especial")
                {
                    lblTipo.Text = "★ ESPECIAL";
                    frameTipoBadge.BackgroundColor = Color.FromArgb("#EAB308");
                    frameCard.BorderColor = Color.FromArgb("#EAB308");
                    frameCard.BackgroundColor = Color.FromArgb("#FFFDF5");
                }
                else
                {
                    lblTipo.Text = "COMUM";
                    frameTipoBadge.BackgroundColor = Color.FromArgb("#64748B");
                    frameCard.BorderColor = Color.FromArgb("#E2E8F0");
                    frameCard.BackgroundColor = Color.FromArgb("#FFFFFF");
                }

                // define a quantidade e se destaca repetidas
                lblQuantidade.Text = $"{figurinha.Quantidade}x";
                frameQtdBadge.IsVisible = figurinha.Quantidade > 0;
                
                if (figurinha.Quantidade > 1)
                {
                    frameQtdBadge.BackgroundColor = Color.FromArgb("#EF4444");
                }
                else
                {
                    frameQtdBadge.BackgroundColor = Color.FromArgb("#475569");
                }

                // exibe se ja adquiriu a figurinha
                if (figurinha.Obtido)
                {
                    lblIconObtido.Text = "✔";
                    lblIconObtido.TextColor = Color.FromArgb("#10B981");
                    lblStatusObtido.Text = "Adquirida e disponível na coleção";
                    lblStatusObtido.TextColor = Color.FromArgb("#10B981");
                }
                else
                {
                    lblIconObtido.Text = "❌";
                    lblIconObtido.TextColor = Color.FromArgb("#EF4444");
                    lblStatusObtido.Text = "Não adquirida ainda";
                    lblStatusObtido.TextColor = Color.FromArgb("#64748B");
                }

                // exibe se ja esta colada no album
                if (figurinha.NoAlbum)
                {
                    lblIconNoAlbum.Text = "📖";
                    lblIconNoAlbum.TextColor = Color.FromArgb("#2563EB");
                    lblStatusNoAlbum.Text = "Colada no Álbum físico";
                    lblStatusNoAlbum.TextColor = Color.FromArgb("#2563EB");
                }
                else
                {
                    lblIconNoAlbum.Text = "❌";
                    lblIconNoAlbum.TextColor = Color.FromArgb("#64748B");
                    lblStatusNoAlbum.Text = "Não colada no Álbum";
                    lblStatusNoAlbum.TextColor = Color.FromArgb("#64748B");
                }
            }
        }

        // volta para a tela anterior
        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
