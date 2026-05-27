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

        private async void OnSelecionarFotoClicked(object sender, EventArgs e)
        {
            try
            {
                // Abre o seletor de fotos nativo do dispositivo
                var foto = await MediaPicker.Default.PickPhotoAsync();
                
                if (foto != null)
                {
                    _selectedFotoPath = foto.FullPath;
                    lblFotoPath.Text = Path.GetFileName(_selectedFotoPath);
                    
                    // Atualiza o preview da imagem
                    imgPreview.Source = ImageSource.FromFile(_selectedFotoPath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Não foi possível abrir a galeria: {ex.Message}", "OK");
            }
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            string nomeJogador = txtNomeJogador.Text?.Trim().ToUpper() ?? string.Empty;
            string selecao = pickerSelecao.SelectedItem?.ToString() ?? string.Empty;
            string tipo = pickerTipo.SelectedItem?.ToString() ?? "Comum";
            bool obtido = switchObtido.IsToggled;
            bool desejado = switchDesejado.IsToggled;

            // Se o usuário não selecionou explicitamente a seleção no picker, mas informou o nome,
            // tentamos inferir a seleção automaticamente para economizar cliques
            if (string.IsNullOrEmpty(selecao) && !string.IsNullOrEmpty(nomeJogador))
            {
                string inferred = FigurinhaController.InferirSelecao(nomeJogador);
                if (!string.IsNullOrEmpty(inferred))
                {
                    pickerSelecao.SelectedItem = inferred;
                    selecao = inferred;
                }
            }

            // Validação visual de campos vazios
            if (string.IsNullOrWhiteSpace(nomeJogador) || 
                string.IsNullOrWhiteSpace(selecao) || 
                string.IsNullOrWhiteSpace(tipo))
            {
                await DisplayAlert("Aviso", "Por favor, preencha todos os campos obrigatórios (*).", "OK");
                return;
            }

            // Se o usuário não selecionou uma foto da galeria, mas o nome do jogador existe no nosso
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

            // Criar o objeto Figurinha
            var novaFigurinha = new Figurinha
            {
                NomeJogador = nomeJogador,
                Selecao = selecao,
                Tipo = tipo,
                Obtido = obtido,
                Desejado = desejado,
                FotoPath = _selectedFotoPath
            };

            // Salvar no SQLite via Controller
            var (sucesso, mensagem) = _controller.SalvarFigurinha(novaFigurinha);

            if (sucesso)
            {
                await DisplayAlert("Sucesso 🎉", mensagem, "OK");
                // Retorna à tela anterior
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Erro ❌", mensagem, "OK");
            }
        }
    }
}
