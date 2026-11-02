/*
Nome: Kaik Persike Maiorquino
Prontuário: CB3029689

Nome: Luiz Gustavo Verissimo Monteiro
Prontuário: CB3030326
*/
using CBTPRDM_TP03.Mvvm.Models;
using CBTPRDM_TP03.Mvvm.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace CBTPRDM_TP03.Mvvm.ViewModels
{
    public class TrackingViewModel : INotifyPropertyChanged
    {
        private bool _hasResult;
        public bool HasResult
        {
            get => _hasResult;
            set { _hasResult = value; OnPropertyChanged(); }
        }

        private PackageModel _package;
        public PackageModel Package
        {
            get => _package;
            set { _package = value; OnPropertyChanged(); }
        }

        private string _inputCode;
        private readonly List<PackageModel> _pacotesFicticios;

        public string InputCode
        {
            get => _inputCode;
            set { _inputCode = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }

        public TrackingViewModel()
        {
            SearchCommand = new Command(OnSearchAsync);

            _pacotesFicticios = new List<PackageModel>
            {
                new PackageModel
                {
                    TrackingCode = "BR123456789",
                    Status = "Em Trânsito",
                    ShippingDate = DateTime.Now.AddDays(-3),
                    EstimatedDeliveryDate = DateTime.Now.AddDays(2),
                    CurrentLocation = "Centro de Distribuição - São Paulo/SP",
                    History = new List<TrackingEvent>
                    {
                        new TrackingEvent { Date = DateTime.Now.AddDays(-3), Description = "Objeto postado", Location = "Agência - Cubatão/SP" },
                        new TrackingEvent { Date = DateTime.Now.AddDays(-2), Description = "Encaminhado para triagem", Location = "Unidade de Tratamento - SP" },
                        new TrackingEvent { Date = DateTime.Now.AddDays(-1), Description = "Chegou ao centro de distribuição", Location = "São Paulo/SP" }
                    }
                },
                new PackageModel
                {
                    TrackingCode = "SP987654321",
                    Status = "Entregue",
                    ShippingDate = DateTime.Now.AddDays(-5),
                    EstimatedDeliveryDate = DateTime.Now.AddDays(-1),
                    CurrentLocation = "Endereço do Destinatário - Santos/SP",
                    History = new List<TrackingEvent>
                    {
                        new TrackingEvent { Date = DateTime.Now.AddDays(-5), Description = "Objeto postado", Location = "Agência - São Vicente/SP" },
                        new TrackingEvent { Date = DateTime.Now.AddDays(-2), Description = "Saiu para entrega", Location = "CDD Santos - Santos/SP" },
                        new TrackingEvent { Date = DateTime.Now.AddDays(-1), Description = "Objeto entregue ao destinatário", Location = "Santos/SP" }
                    }
                },
                new PackageModel
                {
                    TrackingCode = "EX999999999",
                    Status = "Aguardando Retirada",
                    ShippingDate = DateTime.Now.AddDays(-2),
                    EstimatedDeliveryDate = DateTime.Now.AddDays(1),
                    CurrentLocation = "Agência dos Correios - Cubatão/SP",
                    History = new List<TrackingEvent>
                    {
                        new TrackingEvent { Date = DateTime.Now.AddDays(-2), Description = "Objeto postado", Location = "São Paulo/SP" },
                        new TrackingEvent { Date = DateTime.Now, Description = "Aguardando retirada pelo destinatário", Location = "Cubatão/SP" }
                    }
                }
            };
        }

        private async void OnSearchAsync()
        {
            if (string.IsNullOrWhiteSpace(InputCode))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Por favor, introduza um código de rastreamento.", "OK");
                return;
            }

            string codigoProcurado = InputCode.ToUpper().Trim();

            var package = _pacotesFicticios.FirstOrDefault(p => p.TrackingCode == codigoProcurado);

            if (package != null)
            {
                Package = package;
                HasResult = true;
                // Navega para a página de resultados passando o objeto encontrado
                /*
                await Shell.Current.GoToAsync(nameof(ResultPage), true, new Dictionary<string, object>
                {
                    { "PackageData", package }
                });
                */
            }
            else
            {
                HasResult = false;
                await Application.Current.MainPage.DisplayAlert("Não encontrado", "Código não localizado no sistema.\n\nExperimente testar:\n• BR123456789\n• SP987654321\n• EX999999999", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}