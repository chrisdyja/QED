using QedFrontend.Models;
using QedFrontend.Services;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace QedFrontend.ViewModels;

public class MainViewModel : ReactiveObject
{
    private readonly IQedApiService _apiService;
    private readonly CultureInfo? _cultureInfo;

    public ReactiveCommand<Unit, Unit> AddCommand { get; }

    private string? _statusMessage = "Hello!";
    public string? StatusMessage
    {
        get => _statusMessage;
        set => this.RaiseAndSetIfChanged(ref _statusMessage, value);
    }

    private bool _inputValidationSuccess = false;
    public bool InputValidationSuccess
    {
        get => _inputValidationSuccess;
        set => this.RaiseAndSetIfChanged(ref _inputValidationSuccess, value);
    }

    private string? _numberA;
    public string? NumberA
    {
        get => _numberA;
        set 
        {
            this.RaiseAndSetIfChanged(ref _numberA, value);
            ValidateInput();
        } 
    }

    private string? _numberB;
    public string? NumberB
    {
        get => _numberB;
        set
        {
            this.RaiseAndSetIfChanged(ref _numberB, value);
            ValidateInput();
        }

    }

    private string? _sum;
    public string? Sum
    {
        get => _sum;
        set => this.RaiseAndSetIfChanged(ref _sum, value);
    }


    public MainViewModel()
    {
        // Parameterless constructor for Avalonia XAML loader
    }
    public MainViewModel(IQedApiService apiService)
    {
        _apiService = apiService;

        _cultureInfo = new CultureInfo("pl-PL");

        AddCommand = ReactiveCommand.CreateFromTask(AddAsync);
    }
    private async Task AddAsync()
    {
        try
        {
            StatusMessage = "Calculating...";
            double a, b;

            if (double.TryParse(_numberA, NumberStyles.Float, _cultureInfo, out a) && double.TryParse(_numberB, NumberStyles.Float, _cultureInfo, out b) && InputValidationSuccess)
            {
                var items = await _apiService.GetAsync<SumResponse>(a, b);
                StatusMessage = "Finished calculating.";
                Sum = Convert.ToDouble(items.Sum.ToString(), _cultureInfo).ToString();
            }
            else
            {
                StatusMessage = "Input is invalid!";
            }          
        }
        catch (HttpRequestException ex)
        {
            StatusMessage = $"Request Error: {ex.Message}";
        }
        catch (Exception e)
        {
            StatusMessage = $"Error: {e.Message}";
        }
    }

    private void ValidateInput()
    {
        if (!double.TryParse(NumberA, NumberStyles.Float, _cultureInfo, out _))
        {
            StatusMessage = "First field must contain a valid number.";
            InputValidationSuccess = false;
        }
        else if (!double.TryParse(NumberB, NumberStyles.Float, _cultureInfo, out _))
        {
            StatusMessage = "Second field must contain a valid number.";
            InputValidationSuccess = false;
        }
        else
        {
            StatusMessage = null; 
            InputValidationSuccess = true;
        }
    }
}
