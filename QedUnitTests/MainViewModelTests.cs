using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using Moq;
using QedFrontend.Models;
using QedFrontend.Services;
using QedFrontend.ViewModels;
using QedFrontend.Views;
using System.Reactive.Linq;

namespace QedUnitTests
{

    public class MainViewModelTests
    {
        [Fact]
        public void ViewModel_ShouldInitializeWithDefaultValues()
        {
            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            var viewModel = new MainViewModel(mockApiService.Object);

            // Assert
            Assert.Null(viewModel.NumberA);
            Assert.Null(viewModel.NumberB);
            Assert.Null(viewModel.Sum);
            Assert.Equal("Hello!", viewModel.StatusMessage);
        }

        [Fact]
        public async Task AddCommand_ShouldCallApiWithValidInputs()
        {
            double A = 2;
            double B = 3;
            string inputA = A.ToString();
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ReturnsAsync(new SumResponse { Sum = A + B});

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.Equal((A + B).ToString(), viewModel.Sum);
        }

        [Fact]
        public async Task AddCommand_ShouldCallApi_WhenInputContainsNegativeNumbers()
        {
            double A = -2;
            double B = 3;
            string inputA = A.ToString();
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ReturnsAsync(new SumResponse { Sum = A + B });

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.Equal((A + B).ToString(), viewModel.Sum);
        }

        [Fact]
        public async Task AddCommand_ShouldCallApi_WhenInputContainsLargeNumbers()
        {
            double A = double.MaxValue;
            double B = 3;
            string inputA = A.ToString();
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ReturnsAsync(new SumResponse { Sum = A + B });

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.Equal((A + B).ToString(), viewModel.Sum);
        }

        [Fact]
        public async Task AddCommand_ShouldCallApi_WhenInputContainsFloatingPointNumbers()
        {
            double A = 2.7;
            double B = 3;
            string inputA = A.ToString();
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ReturnsAsync(new SumResponse { Sum = A + B });

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.Equal((A + B).ToString(), viewModel.Sum);
        }

        [Fact]
        public async Task AddCommand_ShouldCallApi_WhenInputContainsNegativeFloatingPointNumbers()
        {
            double A = -2.7;
            double B = 3;
            string inputA = A.ToString();
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ReturnsAsync(new SumResponse { Sum = A + B });

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.Equal((A + B).ToString(), viewModel.Sum);
        }

        [Fact]
        public async Task AddCommand_ShouldNotCallApi_WhenInputContainsTwoCommas()
        {
            double A = 2.76;
            double B = 3;
            string inputA = "2,7,6";
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ReturnsAsync(new SumResponse { Sum = A + B });

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.False(viewModel.InputValidationSuccess);
            Assert.Null(viewModel.Sum);
        }

        [Fact]
        public async Task AddCommand_ShouldNotCallApi_WhenInputContainsNumbersAndCharacters()
        {
            double A = 2.7;
            double B = 3;
            string inputA = "2,a";
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ReturnsAsync(new SumResponse { Sum = A + B });

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.False(viewModel.InputValidationSuccess);
            Assert.Null(viewModel.Sum);
        }

        [Fact]
        public async Task InputValidationSuccess_ShouldBeFalse_WhenInputContainsDotFloatingPointSeperator()
        {
            double A = 2.7;
            double B = 3;
            string inputA = "2.7";
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ReturnsAsync(new SumResponse { Sum = A + B });

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.False(viewModel.InputValidationSuccess);
            Assert.Null(viewModel.Sum);
        }

        [Fact]
        public async Task AddCommand_ShouldNotCallApi_IfInputIsInvalid()
        {
            double A = -2;
            double B = 3;
            string inputA = "abcd";
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(0, B))
                .ReturnsAsync(new SumResponse { Sum = 0 + B });

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.False(viewModel.InputValidationSuccess);
            Assert.Null(viewModel.Sum);
        }

        [Fact]
        public async Task AddCommand_ShouldHandleApiErrors_IfHttpExceptionOccurs()
        {
            double A = 2;
            double B = 3;
            string inputA = A.ToString();
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ThrowsAsync(new HttpRequestException("Backend Error"));

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.Null(viewModel.Sum);
            Assert.Equal("Request Error: Backend Error", viewModel.StatusMessage);
        }

        [Fact]
        public async Task AddCommand_ShouldHandleApiErrors_IfAnyExceptionOccurs()
        {
            double A = 2;
            double B = 3;
            string inputA = A.ToString();
            string inputB = B.ToString();

            // Arrange
            var mockApiService = new Mock<IQedApiService>();
            mockApiService
                .Setup(service => service.GetAsync<SumResponse>(A, B))
                .ThrowsAsync(new Exception("Backend Error"));

            var viewModel = new MainViewModel(mockApiService.Object);
            viewModel.NumberA = inputA;
            viewModel.NumberB = inputB;

            // Act
            await viewModel.AddCommand.Execute();

            // Assert
            Assert.Null(viewModel.Sum);
            Assert.Equal("Error: Backend Error", viewModel.StatusMessage);
        }
    }
}