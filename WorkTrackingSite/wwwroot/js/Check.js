const url = 'https://sky-160:5100/GetPrinterInfo/GetDriver/CheckName/';

document.getElementById('uploadB').onclick = function () {
    var printerName = document.getElementById('pn').value;
    var tempUrl = url + printerName;
    var result = fetch(tempUrl)
        .catch(error => error);
    if (result.ok) {
        alert('OK')
    } else {
        alert('BAD');
    }
}