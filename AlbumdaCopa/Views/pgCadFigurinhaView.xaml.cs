using System;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;
using AlbumdaCopa.Models;
using AlbumdaCopa.Controllers;
using AlbumdaCopa.Services;

namespace AlbumdaCopa.Views
{
    public partial class pgCadFigurinhaView : ContentPage
    {
        FigurinhaController _controle;
        string _imgSelecionada = "";

        public pgCadFigurinhaView()
        {
            InitializeComponent();
            _controle = new FigurinhaController();
            pickerTipo.SelectedIndex = 0;
            pickerSelecao.ItemsSource = FigurinhaController.ListaSelecoes;
        }

        // seleciona uma imagem da galeria do celular
        private async void OnSelecionarFotoClicked(object sender, EventArgs e)
        {
            try
            {
                _imgSelecionada = await ImageService.SelecionarImagem();
                
                if (!string.IsNullOrEmpty(_imgSelecionada))
                {
                    lblFotoPath.Text = Path.GetFileName(_imgSelecionada);
                    imgPreview.Source = ImageSource.FromFile(_imgSelecionada);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Não foi possível abrir a galeria: {ex.Message}", "OK");
            }
        }

        // valida e salva a figurinha no banco de dados
        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            string nomeJogador = txtNomeJogador.Text?.Trim().ToUpper() ?? string.Empty;
            string selecao = pickerSelecao.SelectedItem?.ToString() ?? string.Empty;
            string tipo = pickerTipo.SelectedItem?.ToString() ?? "Comum";
            bool obtido = switchObtido.IsToggled;
            bool desejado = switchDesejado.IsToggled;

            // valida se os campos obrigatorios foram preenchidos
            if (string.IsNullOrWhiteSpace(nomeJogador) || 
                string.IsNullOrWhiteSpace(selecao) || 
                string.IsNullOrWhiteSpace(tipo))
            {
                await DisplayAlert("Atençao", "Preencha todos os campos obrigatórios.", "OK");
                return;
            }

            // tenta buscar a foto oficial se o usuario nao escolheu nenhuma imagem
            if (string.IsNullOrEmpty(_imgSelecionada))
            {
                string normalizedInput = nomeJogador.Replace(" ", "_");
                var matchJogador = FigurinhaController.PoolJogadores
                    .FirstOrDefault(p => p.Name.Equals(normalizedInput, StringComparison.OrdinalIgnoreCase));

                if (matchJogador != null)
                {
                    _imgSelecionada = matchJogador.Path;
                }
                else
                {
                    await DisplayAlert("Atençao", "Selecione uma imagem na galeria ou use o nome de um jogador valido.", "OK");
                    return;
                }
            }

            // cria o objeto com os dados preenchidos
            var novaFigurinha = new Figurinha
            {
                NomeJogador = nomeJogador,
                Selecao = selecao,
                Tipo = tipo,
                Obtido = obtido,
                Desejado = desejado,
                FotoPath = ImageService.CopiarImagem(_imgSelecionada)
            };

            // tenta salvar no banco e limpa a tela
            if (_controle.Salvar(novaFigurinha))
            {
                await DisplayAlert("Informaçao", "Registro salvo com sucesso!", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Atençao", "Falha ao salvar o cadastro.", "OK");
            }
        }
    }
}
