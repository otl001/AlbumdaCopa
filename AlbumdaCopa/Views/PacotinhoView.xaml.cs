using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using AlbumdaCopa.Models;
using AlbumdaCopa.Controllers;

namespace AlbumdaCopa.Views
{
    public partial class PacotinhoView : ContentPage
    {
        private readonly FigurinhaController _controller;
        private List<Figurinha> _sorteadas = new List<Figurinha>();
        private int _currentIndex = 0;

        public PacotinhoView()
        {
            InitializeComponent();
            _controller = new FigurinhaController();
        }

        private async void OnAbrirPacotinhoClicked(object sender, EventArgs e)
        {
            try
            {
                // 1. Sorteia 7 figurinhas (salvando no SQLite)
                _sorteadas = _controller.SortearPacotinho(7);

                if (_sorteadas == null || _sorteadas.Count == 0)
                {
                    await DisplayAlert("Erro", "Não foi possível realizar o sorteio.", "OK");
                    return;
                }

                _currentIndex = 0;

                // 2. Transiciona para a revelação individual
                layoutPacotinhoFechado.IsVisible = false;
                layoutSingleReveal.IsVisible = true;

                // 3. Exibe o primeiro jogador
                await ExibirJogadorAtual();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Falha na abertura: {ex.Message}", "OK");
            }
        }

        private async Task ExibirJogadorAtual()
        {
            if (_sorteadas == null || _currentIndex >= _sorteadas.Count)
                return;

            var fig = _sorteadas[_currentIndex];

            // Atualiza indicadores de progresso
            lblProgress.Text = $"Figurinha {_currentIndex + 1} de {_sorteadas.Count}";

            // Configura os dados da figurinha (apenas foto e nome)
            imgSinglePhoto.Source = fig.FotoPath;
            lblSingleNome.Text = fig.NomeJogador;

            // Micro-Animação Premium de Revelação (zoom + fade)
            cardSinglePlayer.Opacity = 0;
            cardSinglePlayer.Scale = 0.85;
            await Task.WhenAll(
                cardSinglePlayer.FadeTo(1, 300, Easing.CubicOut),
                cardSinglePlayer.ScaleTo(1, 300, Easing.CubicOut)
            );
        }

        private async void OnCardTapped(object sender, EventArgs e)
        {
            if (_sorteadas == null || _currentIndex >= _sorteadas.Count)
                return;

            _currentIndex++;

            if (_currentIndex < _sorteadas.Count)
            {
                // Animação de Saída
                await Task.WhenAll(
                    cardSinglePlayer.FadeTo(0, 150, Easing.CubicIn),
                    cardSinglePlayer.ScaleTo(0.9, 150, Easing.CubicIn)
                );

                // Exibe a próxima
                await ExibirJogadorAtual();
            }
            else
            {
                // Revelou todas! Transiciona para o resumo
                await Task.WhenAll(
                    cardSinglePlayer.FadeTo(0, 180, Easing.CubicIn),
                    cardSinglePlayer.ScaleTo(0.85, 180, Easing.CubicIn)
                );

                ExibirResumoFinal();

                layoutSingleReveal.IsVisible = false;
                layoutFinalSummary.IsVisible = true;
            }
        }

        private void ExibirResumoFinal()
        {
            flexFigurinhas.Children.Clear();

            foreach (var fig in _sorteadas)
            {
                // Card no estilo super limpo e minimalista (sem cores fortes, apenas borda cinza clara)
                var cardFrame = new Frame
                {
                    WidthRequest = 115,
                    HeightRequest = 150,
                    Margin = new Thickness(4),
                    Padding = new Thickness(4),
                    CornerRadius = 8,
                    BorderColor = Color.FromArgb("#E2E8F0"),
                    BackgroundColor = Color.FromArgb("#FFFFFF"),
                    HasShadow = true
                };

                var stack = new VerticalStackLayout
                {
                    Spacing = 4,
                    HorizontalOptions = LayoutOptions.Center
                };

                // Foto do Jogador
                var imgFrame = new Frame
                {
                    HeightRequest = 100,
                    WidthRequest = 95,
                    Padding = 0,
                    CornerRadius = 4,
                    BorderColor = Color.FromArgb("#E2E8F0"),
                    BackgroundColor = Color.FromArgb("#F8FAFC"),
                    HasShadow = false,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 2, 0, 2)
                };

                var img = new Image
                {
                    Source = fig.FotoPath,
                    Aspect = Aspect.AspectFill
                };
                imgFrame.Content = img;
                stack.Children.Add(imgFrame);

                // Apenas o Nome do Jogador
                var lblNome = new Label
                {
                    Text = fig.NomeJogador,
                    FontSize = 9.0,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#0F172A"),
                    HorizontalTextAlignment = TextAlignment.Center,
                    LineBreakMode = LineBreakMode.TailTruncation
                };
                stack.Children.Add(lblNome);

                cardFrame.Content = stack;
                flexFigurinhas.Children.Add(cardFrame);
            }
        }

        private void OnAbrirOutroClicked(object sender, EventArgs e)
        {
            _sorteadas.Clear();
            flexFigurinhas.Children.Clear();
            
            cardSinglePlayer.Opacity = 1;
            cardSinglePlayer.Scale = 1;

            layoutFinalSummary.IsVisible = false;
            layoutPacotinhoFechado.IsVisible = true;
        }

        private async void OnVerColecaoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListagemView());
        }
    }
}
