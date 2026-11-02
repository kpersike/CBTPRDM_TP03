/*
Nome: Kaik Persike Maiorquino
Prontuário: CB3029689

Nome: Luiz Gustavo Verissimo Monteiro
Prontuário: CB3030326
*/
using CBTPRDM_TP03.Mvvm.Models;

namespace CBTPRDM_TP03.Mvvm.Views;

[QueryProperty(nameof(Package), "PackageData")]
public partial class ResultPage : ContentPage
{
    private PackageModel _package;
    public PackageModel Package
    {
        get => _package;
        set
        {
            _package = value;
            OnPropertyChanged();
        }
    }

    public ResultPage()
	{
		InitializeComponent();
        BindingContext = this;
    }
}