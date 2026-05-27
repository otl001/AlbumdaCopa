using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using AlbumdaCopa.Models;
using AlbumdaCopa.Controllers;

namespace AlbumdaCopa.Views
{
    public partial class pgColecaoView : ContentPage
    {
        FigurinhaController _controle;
        private string _filtroStatusAtivo = "Todas"; // valores: todas, adquiridas, repetidas, noalbum

        public pgColecaoView()
        {
            InitializeComponent();
            _controle = new FigurinhaController();
        }

        // roda sempre que a tela abre
        protected override void OnAppearing()
        {
            base.OnAppearing();
            CarregarFigurinhas();
        }

        // pega e exibe as figurinhas aplicando os filtros ativos
        private void CarregarFigurinhas()
        {
            string busca = searchBar?.Text ?? string.Empty;
            
            // define qual filtro de status usar
            bool? apenasObtidos = null;
            bool? apenasDesejados = null;

            if (_filtroStatusAtivo == "Adquiridas")
                apenasObtidos = true;
            else if (_filtroStatusAtivo == "Desejados")
                apenasDesejados = true;

            // pega as figurinhas filtradas no banco
            List<Figurinha> lista = _controle.ListarFigurinhas(busca, apenasObtidos, apenasDesejados);

            // filtra as repetidas e coladas em memoria para ficar mais rapido
            if (_filtroStatusAtivo == "Repetidas")
            {
                lista = lista.Where(f => f.Quantidade > 1).ToList();
            }
            else if (_filtroStatusAtivo == "NoAlbum")
            {
                lista = lista.Where(f => f.NoAlbum).ToList();
            }

            // joga as figurinhas na grade da tela
            collectionViewFigurinhas.ItemsSource = lista;

            // atualiza os numeros do painel de cima
            AtualizarPainelEstatisticas();
        }

        // calcula e atualiza os totais e a barra de progresso no topo da tela
        private void AtualizarPainelEstatisticas()
        {
            // pega todas as figurinhas do banco para calcular os totais reais
            var todas = _controle.ListarFigurinhas(string.Empty, null, null);

            int totalObtidas = todas.Count(f => f.Obtido);
            int totalRepetidas = todas.Sum(f => f.Quantidade > 1 ? f.Quantidade - 1 : 0);
            int totalColadas = todas.Count(f => f.NoAlbum);

            // joga os numeros obtidos nos textos da tela
            lblStatObtidas.Text = $"{totalObtidas} / 757";
            lblStatRepetidas.Text = totalRepetidas.ToString();
            lblStatColadas.Text = $"{totalColadas} / 757";

            // calcula a porcentagem de figurinhas coladas no album
            double percent = 0;
            if (todas.Count > 0)
            {
                percent = (double)totalColadas / 757.0;
            }
            
            lblStatPercent.Text = $"{Math.Round(percent * 100, 1)}%";
            progressAlbum.Progress = percent;
        }

        // recarrega a lista quando o usuario digita no campo de busca
        private void OnFiltrosChanged(object sender, EventArgs e)
        {
            CarregarFigurinhas();
        }

        // trata o clique nos chips de filtro por status
        private void OnChipFilterClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            string filterType = button.CommandParameter?.ToString() ?? "Todas";

            // define qual chip foi clicado
            _filtroStatusAtivo = filterType;

            // muda as cores dos chips para destacar o selecionado
            AtualizarCoresDosChips();

            // atualiza a lista de figurinhas
            CarregarFigurinhas();
        }

        // muda as cores de fundo dos chips ativo e inativos
        private void AtualizarCoresDosChips()
        {
            // deixa todos os chips com a cor cinza padrao
            btnChipTodas.BackgroundColor = Color.FromArgb("#F1F5F9");
            btnChipTodas.TextColor = Color.FromArgb("#475569");

            btnChipAdquiridas.BackgroundColor = Color.FromArgb("#F1F5F9");
            btnChipAdquiridas.TextColor = Color.FromArgb("#475569");

            btnChipDesejados.BackgroundColor = Color.FromArgb("#F1F5F9");
            btnChipDesejados.TextColor = Color.FromArgb("#475569");

            btnChipRepetidas.BackgroundColor = Color.FromArgb("#F1F5F9");
            btnChipRepetidas.TextColor = Color.FromArgb("#475569");

            btnChipNoAlbum.BackgroundColor = Color.FromArgb("#F1F5F9");
            btnChipNoAlbum.TextColor = Color.FromArgb("#475569");

            // deixa o chip selecionado com cor escura
            if (_filtroStatusAtivo == "Todas")
            {
                btnChipTodas.BackgroundColor = Color.FromArgb("#0F172A");
                btnChipTodas.TextColor = Color.FromArgb("#FFFFFF");
            }
            else if (_filtroStatusAtivo == "Adquiridas")
            {
                btnChipAdquiridas.BackgroundColor = Color.FromArgb("#0F172A");
                btnChipAdquiridas.TextColor = Color.FromArgb("#FFFFFF");
            }
            else if (_filtroStatusAtivo == "Desejados")
            {
                btnChipDesejados.BackgroundColor = Color.FromArgb("#0F172A");
                btnChipDesejados.TextColor = Color.FromArgb("#FFFFFF");
            }
            else if (_filtroStatusAtivo == "Repetidas")
            {
                btnChipRepetidas.BackgroundColor = Color.FromArgb("#0F172A");
                btnChipRepetidas.TextColor = Color.FromArgb("#FFFFFF");
            }
            else if (_filtroStatusAtivo == "NoAlbum")
            {
                btnChipNoAlbum.BackgroundColor = Color.FromArgb("#0F172A");
                btnChipNoAlbum.TextColor = Color.FromArgb("#FFFFFF");
            }
        }

        // muda o status de obtido/nao obtido ao clicar no botao do cartao
        private void OnAlternarObtidoClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var figurinha = (Figurinha)button.CommandParameter;
            
            if (figurinha != null)
            {
                _controle.AlternarStatusObtido(figurinha);
                CarregarFigurinhas();
            }
        }

        // muda o status de desejado ao clicar no botao de coraçao
        private void OnAlternarDesejadoClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var figurinha = (Figurinha)button.CommandParameter;
            
            if (figurinha != null)
            {
                _controle.AlternarStatusDesejado(figurinha);
                CarregarFigurinhas();
            }
        }

        // trata o clique para colar a figurinha ou remove-la do album
        private async void OnColarNoAlbumCardClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var figurinha = (Figurinha)button.CommandParameter;
            
            if (figurinha == null)
                return;

            // pergunta se quer descolar a figurinha se ela ja estiver colada
            if (figurinha.NoAlbum)
            {
                bool descolar = await DisplayAlert(
                    "Descolar Figurinha?",
                    $"Deseja remover '{figurinha.NomeJogador}' do seu álbum?",
                    "Sim, Descolar",
                    "Voltar");

                if (descolar)
                {
                    figurinha.NoAlbum = false;
                    _controle.Salvar(figurinha);
                    CarregarFigurinhas();
                    await DisplayAlert("Álbum", $"'{figurinha.NomeJogador}' foi retirado do álbum.", "OK");
                }
                return;
            }

            // abre a lista de paises para o usuario escolher onde colar
            string selecaoEscolhida = await DisplayActionSheet(
                $"Onde deseja colar {figurinha.NomeJogador}?",
                "Cancelar",
                null,
                FigurinhaController.ListaSelecoes);

            if (!string.IsNullOrEmpty(selecaoEscolhida) && selecaoEscolhida != "Cancelar")
            {
                // salva no banco que a figurinha foi colada na selecao escolhida
                figurinha.Selecao = selecaoEscolhida;
                _controle.ColarNoAlbum(figurinha);

                await DisplayAlert("Álbum", $"'{figurinha.NomeJogador}' foi colado na seleção '{selecaoEscolhida}' com sucesso!", "OK");
                
                CarregarFigurinhas();
            }
        }

        // exclui a figurinha da colecao apos confirmacao
        private async void OnExcluirClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var figurinha = (Figurinha)button.CommandParameter;

            if (figurinha != null)
            {
                // mostra o aviso para confirmar a exclusao definitiva da figurinha
                bool confirmacao = await DisplayAlert(
                    "Confirmar Exclusão",
                    $"Tem certeza que deseja remover permanentemente a figurinha de '{figurinha.NomeJogador}'?",
                    "Sim, Excluir",
                    "Cancelar");

                if (confirmacao)
                {
                    if (_controle.Delete(figurinha))
                    {
                        await DisplayAlert("Informaçao", "Registro excluído com sucesso.", "OK");
                        CarregarFigurinhas();
                    }
                    else
                    {
                        await DisplayAlert("Atençao", "Falha ao excluir o registro.", "OK");
                    }
                }
            }
        }

        // abre os detalhes da figurinha clicada
        private async void OnCardTapped(object sender, EventArgs e)
        {
            TappedEventArgs tapped = (TappedEventArgs)e;
            if (tapped.Parameter is Figurinha item)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new pgVisualizarFigurinhaView(item));
            }
        }
    }
}
