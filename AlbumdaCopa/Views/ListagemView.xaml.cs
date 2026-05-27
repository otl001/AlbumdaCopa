using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using AlbumdaCopa.Models;
using AlbumdaCopa.Controllers;

namespace AlbumdaCopa.Views
{
    public partial class ListagemView : ContentPage
    {
        private readonly FigurinhaController _controller;

        public ListagemView()
        {
            InitializeComponent();
            _controller = new FigurinhaController();

            // Popula o picker com "Todas" + a lista das 48 seleções oficiais
            var listSelecoes = new List<string> { "Todas" };
            listSelecoes.AddRange(FigurinhaController.ListaSelecoes);
            pickerFiltroSelecao.ItemsSource = listSelecoes;
            pickerFiltroSelecao.SelectedIndex = 0; // "Todas"
        }

        // Executado sempre que a tela fica visível (garante atualização ao voltar de outras telas)
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CarregarFigurinhas();
        }

        private void CarregarFigurinhas()
        {
            string busca = searchBar.Text ?? string.Empty;
            
            // Define filtros opcionais se os switches estiverem ativados
            bool? apenasObtidos = switchFiltroObtido.IsToggled ? true : (bool?)null;
            bool? apenasDesejados = switchFiltroDesejado.IsToggled ? true : (bool?)null;

            // Busca filtrada no banco SQLite
            List<Figurinha> lista = _controller.ListarFigurinhas(busca, apenasObtidos, apenasDesejados);

            // Filtro por Seleção oficial (Copa 2026 - 48 Times)
            string selecaoFiltro = pickerFiltroSelecao.SelectedItem?.ToString() ?? "Todas";
            if (selecaoFiltro != "Todas")
            {
                lista = lista.Where(f => f.Selecao.Equals(selecaoFiltro, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            listViewFigurinhas.ItemsSource = lista;
        }

        private void OnFiltrosChanged(object sender, EventArgs e)
        {
            // Atualiza em tempo real sempre que o usuário digita ou ativa filtros
            CarregarFigurinhas();
        }

        private void OnAlternarObtidoClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var figurinha = (Figurinha)button.CommandParameter;
            
            if (figurinha != null)
            {
                _controller.AlternarStatusObtido(figurinha);
                CarregarFigurinhas(); // Atualiza a lista
            }
        }

        private void OnAlternarDesejadoClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var figurinha = (Figurinha)button.CommandParameter;
            
            if (figurinha != null)
            {
                _controller.AlternarStatusDesejado(figurinha);
                CarregarFigurinhas(); // Atualiza a lista
            }
        }

        private async void OnFigurinhaTapped(object sender, ItemTappedEventArgs e)
        {
            var figurinha = e.Item as Figurinha;
            if (figurinha == null)
                return;

            // Limpa a seleção visual da linha
            listViewFigurinhas.SelectedItem = null;

            // Abre o menu para ver, colar ou voltar
            string acao = await DisplayActionSheet(
                $"Opções para {figurinha.NomeJogador}",
                "Voltar",
                null,
                "Ver Figurinha",
                "Colar no Álbum");

            if (acao == "Ver Figurinha")
            {
                // Abre a tela de visualização da figurinha
                await Navigation.PushAsync(new VisualizacaoView(figurinha));
            }
            else if (acao == "Colar no Álbum")
            {
                // Abre a seleção dos 48 países para colar
                string selecaoEscolhida = await DisplayActionSheet(
                    $"Onde deseja colar {figurinha.NomeJogador}?",
                    "Cancelar",
                    null,
                    FigurinhaController.ListaSelecoes);

                if (!string.IsNullOrEmpty(selecaoEscolhida) && selecaoEscolhida != "Cancelar")
                {
                    // Atualiza a seleção e cola no álbum no banco SQLite
                    figurinha.Selecao = selecaoEscolhida;
                    _controller.ColarNoAlbum(figurinha);

                    await DisplayAlert("Álbum 📖", $"'{figurinha.NomeJogador}' foi colado na seleção '{selecaoEscolhida}' com sucesso!", "Excelente!");
                    
                    CarregarFigurinhas(); // Recarrega a listagem
                }
            }
        }

        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var figurinha = (Figurinha)button.CommandParameter;

            if (figurinha != null)
            {
                // Exibe modal de confirmação de segurança (Requisito obrigatório)
                bool confirmacao = await DisplayAlert(
                    "Confirmar Exclusão ⚠️",
                    $"Tem certeza que deseja remover permanentemente a figurinha de '{figurinha.NomeJogador}'?",
                    "Sim, Excluir",
                    "Cancelar");

                if (confirmacao)
                {
                    var (sucesso, mensagem) = _controller.ExcluirFigurinha(figurinha);
                    if (sucesso)
                    {
                        await DisplayAlert("Sucesso", mensagem, "OK");
                        CarregarFigurinhas(); // Atualiza a lista
                    }
                    else
                    {
                        await DisplayAlert("Erro", mensagem, "OK");
                    }
                }
            }
        }
    }
}
