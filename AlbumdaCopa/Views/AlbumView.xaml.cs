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

            // Popula o Picker com as 48 seleções oficiais
            pickerSelecao.ItemsSource = FigurinhaController.ListaSelecoes;
            pickerSelecao.SelectedIndex = 9; // Default: Seleciona Brasil (índice 9 na lista alfabética)
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CarregarAlbum();
        }

        private void OnSelecaoChanged(object sender, EventArgs e)
        {
            CarregarAlbum();
        }

        private void CarregarAlbum()
        {
            string selecaoSelecionada = pickerSelecao.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(selecaoSelecionada))
                return;

            // 1. Obtém todas as figurinhas coladas no banco para esta seleção
            var figurinhasDoBanco = _controller.ListarTodos();
            var coladasDestaSelecao = figurinhasDoBanco
                .Where(f => f.Selecao.Equals(selecaoSelecionada, StringComparison.OrdinalIgnoreCase) && f.NoAlbum)
                .OrderBy(f => f.NomeJogador)
                .ToList();

            int coladasCount = coladasDestaSelecao.Count;
            int totalSlotsValidos = 26; // 26 bonecos/slots por seleção na Copa

            var slotsDoAlbum = new List<Figurinha>();
            
            // Adiciona as figurinhas já coladas (ordenadas alfabeticamente)
            slotsDoAlbum.AddRange(coladasDestaSelecao);

            // Preenche o restante até atingir 26 slots com bonecos (silhuetas) sem nome
            int slotsVaziosRestantes = Math.Max(0, totalSlotsValidos - coladasCount);
            for (int i = 0; i < slotsVaziosRestantes; i++)
            {
                slotsDoAlbum.Add(new Figurinha
                {
                    NomeJogador = string.Empty, // Sem o nome para ser exibido como boneco anônimo
                    Selecao = selecaoSelecionada,
                    Obtido = false,
                    NoAlbum = false,
                    Quantidade = 0,
                    FotoPath = string.Empty
                });
            }

            collectionViewAlbum.ItemsSource = slotsDoAlbum;
        }

        private async void OnSlotTapped(object sender, TappedEventArgs e)
        {
            var layout = (BindableObject)sender;
            var figurinha = (Figurinha)layout.BindingContext;

            if (figurinha != null)
            {
                if (figurinha.NoAlbum)
                {
                    // Se já foi colada, abre a visualização da figurinha grande
                    await Navigation.PushAsync(new VisualizacaoView(figurinha));
                }
                else
                {
                    // Slot vazio/anônimo: orienta o usuário a ir para Coleção colar
                    await DisplayAlert("Slot vazio",
                        "Espaço vazio, aguardando colocar novo jogador",
                        "Ok");
                }
            }
        }
    }

    // ====================================================================
    // CONVERTER DE INVERSÃO BOOLEANA
    // ====================================================================
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

    // ====================================================================
    // CONVERTER DE CONTROLE DE REPETIDAS 
    // ====================================================================
    public class RepeatedCountConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int qty)
                return qty > 1; // Exibe o badge vermelho se possuir mais de 1 cópia total
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
