document.getElementById('uploadedFile').addEventListener('change', function () {
    var fileInput = this;
    var submitButton = document.querySelector('input[type="submit"]');

    if (fileInput.files && fileInput.files.length > 0) {
        submitButton.style.backgroundColor = '#ff0000'; // Измените цвет на ваш выбор
    } else {
        submitButton.style.backgroundColor = '#007bff'; // Измените цвет на ваш выбор
    }
});