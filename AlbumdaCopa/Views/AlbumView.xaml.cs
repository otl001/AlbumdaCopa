using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using AlbumdaCopa.Models;
using AlbumdaCopa.Controllers;

namespace AlbumdaCopa.Views
{
    public partial class AlbumView : ContentPage
    {
        private readonly FigurinhaController _controller;

        public AlbumView()
        {
            InitializeComponent();
            _controller = new FigurinhaController();

            // joga as selecoes oficiais no picker
            pickerSelecao.ItemsSource = FigurinhaController.ListaSelecoes;
            pickerSelecao.SelectedIndex = 9; // deixa o brasil selecionado por padrao
        }

        // roda sempre que a tela abre
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CarregarAlbum();
        }

        // recarrega o album quando muda o pais no picker
        private void OnSelecaoChanged(object sender, EventArgs e)
        {
            CarregarAlbum();
        }

        // carrega e monta a grade de figurinhas coladas e vazias da selecao
        private void CarregarAlbum()
        {
            string selecaoSelecionada = pickerSelecao.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(selecaoSelecionada))
                return;

            // pega as figurinhas coladas desta selecao no banco
            var figurinhasDoBanco = _controller.ListarTodos();
            var coladasDestaSelecao = figurinhasDoBanco
                .Where(f => f.Selecao.Equals(selecaoSelecionada, StringComparison.OrdinalIgnoreCase) && f.NoAlbum)
                .OrderBy(f => f.NomeJogador)
                .ToList();

            int coladasCount = coladasDestaSelecao.Count;
            int totalSlotsValidos = 26; // total de 26 slots por selecao na copa

            var slotsDoAlbum = new List<Figurinha>();
            
            // adiciona as figurinhas ja coladas
            slotsDoAlbum.AddRange(coladasDestaSelecao);

            // preenche o restante ate 26 com slots vazios (silhuetas)
            int slotsVaziosRestantes = Math.Max(0, totalSlotsValidos - coladasCount);
            for (int i = 0; i < slotsVaziosRestantes; i++)
            {
                slotsDoAlbum.Add(new Figurinha
                {
                    NomeJogador = string.Empty, // sem nome para exibir como silhueta de jogador
                    Selecao = selecaoSelecionada,
                    Obtido = false,
                    NoAlbum = false,
                    Quantidade = 0,
                    FotoPath = string.Empty
                });
            }

            collectionViewAlbum.ItemsSource = slotsDoAlbum;
        }

        // trata o clique em um slot da grade
        private async void OnSlotTapped(object sender, TappedEventArgs e)
        {
            var layout = (BindableObject)sender;
            var figurinha = (Figurinha)layout.BindingContext;

            if (figurinha != null)
            {
                if (figurinha.NoAlbum)
                {
                    // se ja esta colada, abre a tela de detalhes grande
                    await Navigation.PushAsync(new VisualizacaoView(figurinha));
                }
                else
                {
                    // se esta vazio, avisa que precisa colar uma figurinha
                    await DisplayAlert("Slot vazio",
                        "Espaço vazio, aguardando colocar novo jogador",
                        "Ok");
                }
            }
        }
    }

    // conversor para inverter valores booleanos
    public class InverseBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool b)
                return !b;
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // conversor para verificar quantidade de repetidas
    public class RepeatedCountConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int qty)
                return qty > 1; // exibe o circulo vermelho se tiver repetidas
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
