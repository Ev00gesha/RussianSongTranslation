function openForm(formId) {

    var forms = document.getElementsByTagName('form');
    for (var i = 0; i < forms.length; i++) {
        forms[i].classList.add('hidden');
    }

    var showForm = document.getElementById(formId);
    showForm.classList.remove('hidden');
    showForm.addEventListener('submit', function (event) {
        alert("Успешно добавлено");
    });
}
