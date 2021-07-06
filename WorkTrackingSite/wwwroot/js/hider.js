document.getElementById('uploadB').onclick = function () {
    document.getElementById('uploadF').hidden = true;
    document.getElementById('uploadB').hidden = true;
    document.getElementById('netP').hidden = true;
    document.getElementById('descriptionMessage').innerHTML = 'Проверка наличия принтера в БД и загрузка';
}