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

        // trata o clique para abrir um novo pacote
        private async void OnAbrirPacotinhoClicked(object sender, EventArgs e)
        {
            try
            {
                // sorteia 7 figurinhas salvas no sqlite
                _sorteadas = _controller.SortearPacotinho(7);

                if (_sorteadas == null || _sorteadas.Count == 0)
                {
                    await DisplayAlert("Erro", "Não foi possível realizar o sorteio.", "OK");
                    return;
                }

                _currentIndex = 0;

                // abre a revelacao individual
                layoutPacotinhoFechado.IsVisible = false;
                layoutSingleReveal.IsVisible = true;

                // exibe o primeiro jogador sorteado
                await ExibirJogadorAtual();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Falha na abertura: {ex.Message}", "OK");
            }
        }

        // carrega e exibe o jogador atual com animacao
        private async Task ExibirJogadorAtual()
        {
            if (_sorteadas == null || _currentIndex >= _sorteadas.Count)
                return;

            var fig = _sorteadas[_currentIndex];

            // atualiza a figurinha atual no texto do progresso
            lblProgress.Text = $"Figurinha {_currentIndex + 1} de {_sorteadas.Count}";

            // define a foto e o nome do jogador na tela
            imgSinglePhoto.Source = fig.FotoPath;
            lblSingleNome.Text = fig.NomeJogador;

            // faz animacao de revelar o card com zoom e fade
            cardSinglePlayer.Opacity = 0;
            cardSinglePlayer.Scale = 0.85;
            await Task.WhenAll(
                cardSinglePlayer.FadeTo(1, 300, Easing.CubicOut),
                cardSinglePlayer.ScaleTo(1, 300, Easing.CubicOut)
            );
        }

        // trata o clique no card para passar para o proximo jogador
        private async void OnCardTapped(object sender, EventArgs e)
        {
            if (_sorteadas == null || _currentIndex >= _sorteadas.Count)
                return;

            _currentIndex++;

            if (_currentIndex < _sorteadas.Count)
            {
                // faz animacao de saida
                await Task.WhenAll(
                    cardSinglePlayer.FadeTo(0, 150, Easing.CubicIn),
                    cardSinglePlayer.ScaleTo(0.9, 150, Easing.CubicIn)
                );

                // mostra a proxima figurinha
                await ExibirJogadorAtual();
            }
            else
            {
                // mostra o resumo final quando revela todas
                await Task.WhenAll(
                    cardSinglePlayer.FadeTo(0, 180, Easing.CubicIn),
                    cardSinglePlayer.ScaleTo(0.85, 180, Easing.CubicIn)
                );

                ExibirResumoFinal();

                layoutSingleReveal.IsVisible = false;
                layoutFinalSummary.IsVisible = true;
            }
        }

        // monta a grade com todas as figurinhas abertas no final
        private void ExibirResumoFinal()
        {
            flexFigurinhas.Children.Clear();

            foreach (var fig in _sorteadas)
            {
                // cria o card minimalista sem cores fortes
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

                // exibe a foto do jogador
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

                // exibe apenas o nome do jogador
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

        // limpa a tela para abrir outro pacote
        private void OnAbrirOutroClicked(object sender, EventArgs e)
        {
            _sorteadas.Clear();
            flexFigurinhas.Children.Clear();
            
            cardSinglePlayer.Opacity = 1;
            cardSinglePlayer.Scale = 1;

            layoutFinalSummary.IsVisible = false;
            layoutPacotinhoFechado.IsVisible = true;
        }

        // abre a colecao de figurinhas
        private async void OnVerColecaoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListagemView());
        }
    }
}
