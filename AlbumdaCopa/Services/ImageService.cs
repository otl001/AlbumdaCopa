using Microsoft.Maui.Media;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AlbumdaCopa.Services
{
    // classe estatica para selecao e copia de fotos
    public static class ImageService
    {
        // seleciona uma imagem da galeria do dispositivo
        public static async Task<string> SelecionarImagem()
        {
            string diretorio = "";
            var img = await MediaPicker.Default.PickPhotoAsync();
            if (img != null)
            {
                diretorio = img.FullPath;
            }
            return diretorio;
        }

        // copia a imagem selecionada para a pasta local do aplicativo
        public static string CopiarImagem(string dirOriginal)
        {
            string dirDestino = "";
            if (!string.IsNullOrEmpty(dirOriginal))
            {
                var dirNovo = Path.Combine(FileSystem.AppDataDirectory, "Imagens");
                if (!Directory.Exists(dirNovo))
                {
                    Directory.CreateDirectory(dirNovo);
                }

                string nomeOriginal = Path.GetFileName(dirOriginal);
                dirDestino = Path.Combine(dirNovo, nomeOriginal);
                
                if (File.Exists(dirOriginal) && dirOriginal != dirDestino)
                {
                    File.Copy(dirOriginal, dirDestino, overwrite: true);
                }
            }
            return dirDestino;
        }
    }
}
