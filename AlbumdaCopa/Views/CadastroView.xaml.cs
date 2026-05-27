using System;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using AlbumdaCopa.Models;
using AlbumdaCopa.Controllers;

namespace AlbumdaCopa.Views
{
    public partial class CadastroView : ContentPage
    {
        private readonly FigurinhaController _controller;
        private string _selectedFotoPath = string.Empty;

        public CadastroView()
        {
            InitializeComponent();
            _controller = new FigurinhaController();
            pickerTipo.SelectedIndex = 0;
            pickerSelecao.ItemsSource = FigurinhaController.ListaSelecoes;
        }

        // abre a galeria do celular para escolher uma foto
        private async void OnSelecionarFotoClicked(object sender, EventArgs e)
        {
            try
            {
                var foto = await MediaPicker.Default.PickPhotoAsync();
                
                if (foto != null)
                {
                    _selectedFotoPath = foto.FullPath;
                    lblFotoPath.Text = Path.GetFileName(_selectedFotoPath);
                    
                    // atualiza o preview da imagem na tela
                    imgPreview.Source = ImageSource.FromFile(_selectedFotoPath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Não foi possível abrir a galeria: {ex.Message}", "OK");
            }
        }

        // valida os dados e salva a figurinha no banco
        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            string nomeJogador = txtNomeJogador.Text?.Trim().ToUpper() ?? string.Empty;
            string selecao = pickerSelecao.SelectedItem?.ToString() ?? string.Empty;
            string tipo = pickerTipo.SelectedItem?.ToString() ?? "Comum";
            bool obtido = switchObtido.IsToggled;

            // confere se todos os campos obrigatorios foram preenchidos
            if (string.IsNullOrWhiteSpace(nomeJogador) || 
                string.IsNullOrWhiteSpace(selecao) || 
                string.IsNullOrWhiteSpace(tipo))
            {
                await DisplayAlert("Aviso", "Por favor, preencha todos os campos obrigatórios (*).", "OK");
                return;
            }

            // se nao escolheu foto na galeria, tenta buscar a foto oficial pelo nome do jogador
            if (string.IsNullOrEmpty(_selectedFotoPath))
            {
                string normalizedInput = nomeJogador.Replace(" ", "_");
                var matchJogador = FigurinhaController.PoolJogadores
                    .FirstOrDefault(p => p.Name.Equals(normalizedInput, StringComparison.OrdinalIgnoreCase));

                if (matchJogador != null)
                {
                    _selectedFotoPath = matchJogador.Path;
                }
                else
                {
                    await DisplayAlert("Aviso", "Selecione uma imagem na galeria ou use o nome completo de um jogador válido da Copa 2026.", "OK");
                    return;
                }
            }

            // cria o objeto da nova figurinha
            var novaFigurinha = new Figurinha
            {
                NomeJogador = nomeJogador,
                Selecao = selecao,
                Tipo = tipo,
                Obtido = obtido,
                Desejado = false,
                FotoPath = _selectedFotoPath
            };

            // salva a figurinha no banco de dados SQLite
            var (sucesso, mensagem) = _controller.SalvarFigurinha(novaFigurinha);

            if (sucesso)
            {
                await DisplayAlert("Sucesso 🎉", mensagem, "OK");
                // volta para a tela anterior
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Erro ❌", mensagem, "OK");
            }
        }
    }
}
